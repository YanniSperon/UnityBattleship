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
  Serial.println("Touched:" + String(cap.touched()));
  delay(50);
}