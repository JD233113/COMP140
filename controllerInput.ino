/*
    MPU6050 Triple Axis Gyroscope & Accelerometer. Simple Accelerometer Example.
    Read more: http://www.jarzebski.pl/arduino/czujniki-i-sensory/3-osiowy-zyroskop-i-akcelerometr-mpu6050.html
    GIT: https://github.com/jarzebski/Arduino-MPU6050
    Web: http://www.jarzebski.pl
    (c) 2014 by Korneliusz Jarzebski
*/  

// Joystick code based on Henry's Bench Module KY023

#include <Wire.h>
#include <MPU6050.h>

MPU6050 mpu;
int Xin= A0;
int Yin = A1;

void setup() 
{
  Serial.begin(9600);

  /*Serial.println("Initialize MPU6050");

  while(!mpu.begin(MPU6050_SCALE_2000DPS, MPU6050_RANGE_2G))
  {
    Serial.println("Could not find a valid MPU6050 sensor, check wiring!");
    delay(500);
  }

  //checkSettings();*/
}

/*void checkSettings()
{
  Serial.println();
  
  Serial.print(" * Sleep Mode:            ");
  Serial.println(mpu.getSleepEnabled() ? "Enabled" : "Disabled");
  
  Serial.print(" * Clock Source:          ");
  switch(mpu.getClockSource())
  {
    case MPU6050_CLOCK_KEEP_RESET:     Serial.println("Stops the clock and keeps the timing generator in reset"); break;
    case MPU6050_CLOCK_EXTERNAL_19MHZ: Serial.println("PLL with external 19.2MHz reference"); break;
    case MPU6050_CLOCK_EXTERNAL_32KHZ: Serial.println("PLL with external 32.768kHz reference"); break;
    case MPU6050_CLOCK_PLL_ZGYRO:      Serial.println("PLL with Z axis gyroscope reference"); break;
    case MPU6050_CLOCK_PLL_YGYRO:      Serial.println("PLL with Y axis gyroscope reference"); break;
    case MPU6050_CLOCK_PLL_XGYRO:      Serial.println("PLL with X axis gyroscope reference"); break;
    case MPU6050_CLOCK_INTERNAL_8MHZ:  Serial.println("Internal 8MHz oscillator"); break;
  }
  
  Serial.print(" * Accelerometer:         ");
  switch(mpu.getRange())
  {
    case MPU6050_RANGE_16G:            Serial.println("+/- 16 g"); break;
    case MPU6050_RANGE_8G:             Serial.println("+/- 8 g"); break;
    case MPU6050_RANGE_4G:             Serial.println("+/- 4 g"); break;
    case MPU6050_RANGE_2G:             Serial.println("+/- 2 g"); break;
  }  

  Serial.print(" * Accelerometer offsets: ");
  Serial.print(mpu.getAccelOffsetX());
  Serial.print(" / ");
  Serial.print(mpu.getAccelOffsetY());
  Serial.print(" / ");
  Serial.println(mpu.getAccelOffsetZ());
  
  Serial.println();
}*/

void loop()
{
  int xVal, yVal;

  xVal = analogRead (Xin);
  yVal = analogRead (Yin);

  //Vector rawAccel = mpu.readRawAccel();
  //Vector normAccel = mpu.readNormalizeAccel();

  //Used for testing components in Arduino IDE
  /*Serial.print(" Xraw = ");
  Serial.print(rawAccel.XAxis);
  Serial.print(" Yraw = ");
  Serial.print(rawAccel.YAxis);
  Serial.print(" Zraw = ");

  Serial.println(rawAccel.ZAxis);
  Serial.print(" Xnorm = ");
  Serial.print(normAccel.XAxis);
  Serial.print(" Ynorm = ");
  Serial.print(normAccel.YAxis);
  Serial.print(" Znorm = ");
  Serial.println(normAccel.ZAxis);

  Serial.print("X = ");
  Serial.println (xVal, DEC);
  
  Serial.print ("Y = ");
  Serial.println (yVal, DEC);*/

  /*//Communicates with Unity via the serial port
  //Sends "1" if the controller's accelleration is greater than the threshold
  //Used to register when the controller is swung
  if (normAccel.ZAxis > 250 || normAccel.XAxis > 250 || normAccel.YAxis > 250){
    Serial.flush(); 
    Serial.println(1);
 }
 else{
    Serial.flush();
    Serial.println(0);
  }*/

  //Sends data to Unity via the serial port
  //Strings based on the state of the joystick are used by Unity to register movement inputs

  //Forward movement input
  if (xVal < 250) {
    Serial.flush();
    Serial.println(2);
  }
  //Backward movement input
  else if (xVal > 750){
    Serial.flush();
    Serial.println(3);
  }
  //Stops movement in Unity
  else{
    Serial.flush();
    Serial.println(4);
  }

  //Jump input
  if (yVal > 750){
    Serial.flush();
    Serial.println(5);
  }
  //Block input
  else if (yVal < 250){
    Serial.flush();
    Serial.println(6);
  }
  
  
  delay(20);
}
