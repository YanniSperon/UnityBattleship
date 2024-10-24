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
  myPort.bufferUntil('\n');
  size(1000, 900);
  surface.setLocation(-200, -200);
  surface.setVisible(false);
  
  keyStates = new boolean[12];
  lastKeyStates = new boolean[12];
  for (int i = 0; i < 12; ++i) {
    keyStates[i] = false;
    lastKeyStates[i] = false;
  }
  keyCodes =  new int[12];
  keyCodes[0] = KeyEvent.VK_DOWN;
  keyCodes[1] = KeyEvent.VK_LEFT;
  keyCodes[2] = KeyEvent.VK_UP;
  keyCodes[3] = KeyEvent.VK_RIGHT;
  validKeys = 4;
}

void serialEvent(Serial port) {
  String input = port.readString().trim();
  println(input);

  if (input.equals("x")) {
    if (!startedReading) {
      startedReading = true;
      println("*******started reading sensor data*****");
    }
  } else {
    if (startedReading) {
      int delimiterIdx = input.indexOf(":");
      if (delimiterIdx < 0) {
        return;
      }
      String property = input.substring(0, delimiterIdx-1);
      int value = int(input.substring(delimiterIdx+1, input.length() - 1));
      sensorData.set(property, value);
      updateKeys();
    }
  }
}

void updateKeys() {
  if (sensorData.hasKey("Touched")) {
    int val = sensorData.get("Touched");
    for (int i = 0; i < validKeys; ++i) {
      lastKeyStates[i] = keyStates[i];
      keyStates[i] = ((val >> i) & 1) == 1;
      if (keyStates[i] != lastKeyStates[i]) {
        if (keyStates[i]) {
          // Key pressed
        } else {
          // Key released
        }
      }
    }
  }
}
