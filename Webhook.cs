#include <Adafruit_DHT.h>

#include <ThingSpeak.h>

#define DHTPIN D5    

#define DHTTYPE DHT11        

DHT dht(DHTPIN, DHTTYPE);



TCPClient client;

//Identifies what ThingSpeak channel and specific channel set to send the data to and write to
unsigned long ThingSpeakChannelNum = 1352070;
const char * APIWriteKey = "9DSXW4UP5BTQLHJI";


void setup() 
{
    ThingSpeak.begin(client);
    
 pinMode(D5, OUTPUT);
 pinMode(D7, OUTPUT);
  Serial.begin(8000); 
    dht.begin();

 Particle.subscribe("temperature", myHandler, MY_DEVICES);
}


void loop() 
{
    delay(5000);

       
       
 
        float temperature = dht.getTempFarenheit();
       

    
   
   Particle.publish("temperature", String (temperature), PRIVATE);

    
    
    ThingSpeak.setField(1,temperature);

  Serial.print(dht.getTempFarenheit());
    Serial.println("temperature");

  
  ThingSpeak.writeFields(ThingSpeakChannelNum, APIWriteKey);  
}


 void myHandler(const char *event, const char *data)
{
        digitalWrite(D7, HIGH);
             delay(2000);
        digitalWrite(D7, LOW);
             delay(500);
}