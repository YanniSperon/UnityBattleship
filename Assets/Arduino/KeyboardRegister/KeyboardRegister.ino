#include <Wire.h>
#include "Adafruit_MPR121.h"

Adafruit_MPR121 cap = Adafruit_MPR121();

uint16_t temp = 0;

byte data[12];

void setup() {
  Serial.begin(115200);

  while (!Serial) {
    delay(10);
  }
  
  // Default address is 0x5A, if tied to 3.3V its 0x5B
  // If tied to SDA its 0x5C and if SCL then 0x5D
  if (!cap.begin(0x5A)) {
    Serial.println("FAIL: MPR121 not found");
    while (1);
  }

  Serial.println("");
  Serial.println("x");
}

void loop() {
  uint16_t touched = cap.touched(); // Get the touched data as a 16-bit number

  // Convert the touched data into a 12-byte array
  for (int i = 0; i < 12; i++) {
    data[i] = (touched & (1 << i)) ? 1 : 0; // Check each bit, set data[i] to 1 if touched, otherwise 0
  }
  Serial.write(data, 12);
  
  // Print the 12-byte array in Serial Monitor
  
  //Serial.print("Touched: ");
  //for (int i = 0; i < 12; i++) {
  //  Serial.print(data[i] == 1 ? 1:0);
  //  Serial.print(" ");
  //}
  //Serial.println();

  //Serial.println("Touched:" + String(cap.touched()));
  delay(500);
}