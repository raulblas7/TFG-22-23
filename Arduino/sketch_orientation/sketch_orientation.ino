#include "Arduino_BHY2.h"

  Sensor device_orientation(SENSOR_ID_DEVICE_ORI);
  SensorOrientation orientation(SENSOR_ID_ORI);  
  
  void setup(){
    Serial.begin(115200);
    BHY2.begin();
    orientation.begin();
  }

  void loop(){
    BHY2.update();
    
    //Serial.print("orientation pitch :");
    //Serial.println(orientation.pitch());
    Serial.print("orientation heading :");
    Serial.println(orientation.heading());
    Serial.print("orientation roll :");
    Serial.println(orientation.roll());
    delay(500);
  }
