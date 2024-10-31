import processing.serial.*;
import java.awt.AWTException;
import java.awt.Robot;
import java.awt.event.KeyEvent;

Serial myPort;
boolean startedReading = false;
IntDict sensorData;
boolean lastKeyStates[];
boolean keyStates[];
int keyCodes[];
int validKeys = 12;

void setup() {
  println(Serial.list());
  
  String whichPort = Serial.list()[0];
  myPort = new Serial(this, whichPort, 115200);
  sensorData = new IntDict();
  myPort.buffer(12);
  size(1000, 900);
  //surface.setLocation(-200, -200);
  //surface.setVisible(false);
  
  keyStates = new boolean[12];
  lastKeyStates = new boolean[12];
  for (int i = 0; i < 12; ++i) {
    keyStates[i] = false;
    lastKeyStates[i] = false;
  }
  keyCodes =  new int[12];
  keyCodes[0] = KeyEvent.VK_W;
  keyCodes[1] = KeyEvent.VK_A;
  keyCodes[2] = KeyEvent.VK_S;
  keyCodes[3] = KeyEvent.VK_D;
  keyCodes[4] = KeyEvent.VK_ENTER;      
  keyCodes[5] = KeyEvent.VK_ESCAPE; 
  keyCodes[6] = KeyEvent.VK_R;          // Rotate
  keyCodes[7] = KeyEvent.VK_V;          // Switch view
  keyCodes[8] = KeyEvent.VK_PERIOD;          // . volume up
  keyCodes[9] = KeyEvent.VK_COMMA;           // , volume down
  keyCodes[10] = KeyEvent.VK_O;         // Account for other input
  keyCodes[11] = KeyEvent.VK_P;         // Account for other input 
  validKeys = 12;
}

void serialEvent(Serial port) {
  
  //String input = port.readString().trim();
  byte[] input = new byte[12];
  myPort.readBytes(input);
  
  //println(input);
  println("Received data:");
  for (int i=0; i < validKeys; i++) {
    print(input[i] + " ");
  }
  println();
  
  
  //if (input.equals("x")) {
  //  if (!startedReading) {
  //    startedReading = true;
  //    println("*******started reading sensor data*****");
  //  }
  //} else {
  //  if (startedReading) {
  //    int delimiterIdx = input.indexOf(":");
  //    if (delimiterIdx < 0) {
  //      return;
  //    }
  //    String property = input.substring(0, delimiterIdx-1);
  //    int value = int(input.substring(delimiterIdx+1, input.length() - 1));
  //    sensorData.set(property, value);
  //    updateKeys();
  //  }
  //}
  if (myPort.available() >= 12) {
    
    for (int i =0; i<validKeys; i++) {
      lastKeyStates[i] = keyStates[i];
      keyStates[i] = (input[i] == 49); //Checks for ASCII 1, rather than "1"
    }
    updateKeys();
  }
}

void updateKeys() {
  for (int i = 0; i < validKeys; i++) {
    if (keyStates[i] != lastKeyStates[i]) {
      if (keyStates[i]) {
        // Key pressed
        keyPress(keyCodes[i]);
      } else {
        // Key released
        keyRelease(keyCodes[i]);
      }
    }
  }
  //if (sensorData.hasKey("Touched")) {
  //  int val = sensorData.get("Touched");
  //  for (int i = 0; i < validKeys; ++i) {
  //    lastKeyStates[i] = keyStates[i];
  //    keyStates[i] = ((val >> i) & 1) == 1;
  //    if (keyStates[i] != lastKeyStates[i]) {
  //      if (keyStates[i]) {
  //        // Key pressed
  //      } else {
  //        // Key released
  //      }
  //    }
  //  }
  //} 
}

// Function to simulate key press
void keyPress(int keyCode) {
  try {
    Robot robot = new Robot();
    robot.keyPress(keyCode);
  } catch (AWTException e) {
    e.printStackTrace();
  }
}

// Function to simulate key release
void keyRelease(int keyCode) {
  try {
    Robot robot = new Robot();
    robot.keyRelease(keyCode);
  } catch (AWTException e) {
    e.printStackTrace();
  }
}

void draw() {
  background(255); // Clear screen each frame
  
  
  for (int i = 0; i < validKeys; i++) {
    // Draw a rectangle for each button's state
    fill(keyStates[i] ? color(0, 255, 0) : color(255, 0, 0)); // Green if pressed, red if not
    rect(50 + i * 60, height / 2, 50, 50); // Spacing out the rectangles
  }
}
