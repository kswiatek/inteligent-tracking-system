#include <Servo.h>

Servo servo1;
Servo servo2;
String command;

void setup()
{
  Serial.begin(9600);
  servo1.attach(10);
  servo2.attach(11);
}
//////////////////////////////////////////////////////////
void loop()
{      
  if(Serial.available())
  {
   char c = Serial.read();
   if(c == '\n')
   {
      parseCommand(command);
      command = "";
   } 
   else
   {
     command += c;
   }

  }
}
////////////////////////////////////////////////////////////////////////
void parseCommand(String com)
{
  String part1;
  String part2;
 
  part1 = com.substring(0,com.indexOf(" "));
  part2 = com.substring(com.indexOf(" ") + 1);
  
  if(part1.equalsIgnoreCase("servo1"))
  {
   int val = part2.toInt();
   if(val !=0)
   {
     servo1.write(val);
   }
  }
  else if(part1.equalsIgnoreCase("servo2"))
  {
    int val = part2.toInt();
    if(val !=0)
    {servo2.write(val);}
  }
  else
  {
   Serial.println("command not recog"); 
  }
}
