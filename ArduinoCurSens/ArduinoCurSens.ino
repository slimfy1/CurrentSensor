//
//   SDL_Arduino_INA3221 Library Test Code
//   SDL_Arduino_INA3221.cpp Arduino code - runs in continuous mode
//   Version 1.1
//   SwitchDoc Labs   January 31, 2015
//
//
// This was designed for SunAirPlus - Solar Power Controller - www.switchdoc.com
//



#include <Wire.h>
#include <SDL_Arduino_INA3221.h>

SDL_Arduino_INA3221 ina3221;

// the three channels of the INA3221 named for SunAirPlus Solar Power Controller channels (www.switchdoc.com)
#define LIPO_BATTERY_CHANNEL 1
#define SOLAR_CELL_CHANNEL 2
#define OUTPUT_CHANNEL 3

void setup(void) 
{
    
  Serial.begin(57600);
  ina3221.begin();
}

void loop(void) 
{
  
  float shuntvoltage3 = 0;
  float busvoltage3 = 0;
  float current_mA3 = 0;
  float loadvoltage3 = 0;

  busvoltage3 = ina3221.getBusVoltage_V(OUTPUT_CHANNEL);
  shuntvoltage3 = ina3221.getShuntVoltage_mV(OUTPUT_CHANNEL);
  current_mA3 = ina3221.getCurrent_mA(OUTPUT_CHANNEL);
  loadvoltage3 = busvoltage3 + (shuntvoltage3 / 1000);
  Serial.print(loadvoltage3);
  Serial.print(current_mA3);

  delay(100);
}
