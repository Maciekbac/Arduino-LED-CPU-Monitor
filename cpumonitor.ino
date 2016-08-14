//github.com/Maciekbac
int led[]={2,3,4,5,6,7,8,9,10,11}; 

void setup() {
  Serial.begin(19200);
  
  for (int i=0;i<10;i++)
   {//init and test leds
     pinMode(led[i],OUTPUT);
     digitalWrite(led[i],1);
     delay(100);
     digitalWrite(led[i],0);
   }
  
  
}
int cpu;
int activeLeds;
void loop() {
   if (Serial.available()) cpu=Serial.parseInt();
   
        if (cpu<5)  activeLeds=0;
   else if (cpu<20) activeLeds=B1;
   else if (cpu<30) activeLeds=B11;
   else if (cpu<40) activeLeds=B111;
   else if (cpu<50) activeLeds=B1111;
   else if (cpu<60) activeLeds=B11111;
   else if (cpu<70) activeLeds=B111111;
   else if (cpu<80) activeLeds=B1111111;
   else if (cpu<90) activeLeds=B11111111;
   else if (cpu<95) activeLeds=511; //B111111111
   else             activeLeds=1023;//B1111111111

   for (int i=0;i<10;i++)
    {
      digitalWrite(led[i],bitRead(activeLeds,i));
    }
  

}
