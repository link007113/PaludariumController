
#include "RGBdriver.h"
#include <OneWire.h>
#include <DallasTemperature.h>
#define CIN 3 
#define DIN 2
#define ONE_WIRE_BUS 4
RGBdriver Driver(CIN, DIN);
OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);

String serialData;
int red = 0;
int blue = 0;
int green = 0;
int commandBit;
int LedPin = 13;
int RelayPin = 12;
int ledState = 0;
int buzzer = 8;

// ComData Example (cCommandbitrREDgGREENbBLUE)

//c 3  r 255  g 064  b 128
//c3r255g000b000
//c3r000g255b000
//c3r000g000b255

void setup()
{
  pinMode(LedPin, OUTPUT);
  pinMode(RelayPin, OUTPUT);
  Serial.begin(9600);
  Serial.setTimeout(10);
  sensors.begin();
}

void serialEvent()
{
  serialData = Serial.readString();
  //  Serial.println(serialData);
  red = parseDataRed(serialData);
  blue = parseDataBlue(serialData);
  green = parseDataGreen(serialData);
  commandBit = parseDataCommand(serialData);
  //  Serial.println("Following command recieved " + String(commandBit) + ", " +String(red)+", "+String(green)+", "+String(blue)+"");
  execute(commandBit, red, green, blue);
}

int parseDataRed(String data)
{
   data.remove(0, data.indexOf("r") + 1);
  data.remove(data.indexOf("g"));
  data.remove(data.indexOf("r"), 1);
  return data.toInt();
}

int parseDataGreen(String data)
{
  data.remove(0, data.indexOf("g") + 1);
  data.remove(data.indexOf("b"));
  data.remove(data.indexOf("g"), 1);
  return data.toInt();
}

int parseDataBlue(String data)
{
  data.remove(0, data.indexOf("b") + 1);
  return data.toInt();
}

int parseDataCommand(String data)
{
  data.remove(data.indexOf("r"));
  data.remove(data.indexOf("c"), 1);
  return data.toInt();
}


void execute(int commandbitInt, int redInt, int greenInt, int blueInt )
{  
  switch (commandbitInt)
  {
    case 0: // Testing Led
      {

        break;
      }
    case 1: // Testing Led
      {
        Serial.println("Recieved 1");
        ledState = 1;
        digitalWrite(LedPin, ledState);

        break;
      }
    case 2: // Testing Led longer code
      {
        Serial.println("Recieved 2");
        ledState = 1;
        digitalWrite(LedPin, ledState);
        delay(600);
        ledState = 0;
        digitalWrite(LedPin, ledState);
        delay(600);
        ledState = 1;
        digitalWrite(LedPin, ledState);
        delay(600);
        ledState = 0;
        digitalWrite(LedPin, ledState);

        break;
      }
    case 3: // Command mode
      {
        ledState = 0;
        digitalWrite(LedPin, ledState);
        delay(1000);
        //  Serial.println("Following command recieved " + String(commandbitInt) + ", " +String(redInt)+", "+String(greenInt)+", "+String(blueInt)+"");
        Driver.begin();
        Driver.SetColor(redInt, greenInt, blueInt);
        Driver.end();
        break;
      }
    case 4:
      {
        sensors.requestTemperatures();
        Serial.print("Temperature: ");
        Serial.println(sensors.getTempCByIndex(0));
        delay(50);
        break;
      }

    case 5:
      {
        ledState = 0;
        digitalWrite(LedPin, ledState);
        delay(50);
        digitalWrite(RelayPin, 0);
        break;
      }
    case 6:
      {
        ledState = 0;
        digitalWrite(LedPin, ledState);
        delay(50);
        digitalWrite(RelayPin, 1);
        break;
      }     
    default:
      {
        // Serial.println("Recieved Unknown");
        // ledState = 0;
        // digitalWrite(LedPin, ledState);
        tone(buzzer, 400);
        delay(1000);
        noTone(buzzer);
        break;
      }
  }
}

void loop()
{
  // lol
}
