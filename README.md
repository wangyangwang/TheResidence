TODO for Tuesday:

//* important

* 1. add TV and let it play some implicit/suggestive clips  
* 2. re-arrange furnatures 
* 3. put a few pile of books on the attic
* 4. add lights around the mansion, adding “BreathingLight” script to let it controlled by breath
5. add footstep sound
6. adjust ( walking speed, lightings, ambient colors, etc….)


Note:

1. Make sure you are running this program to get serial data from Arduino and sending data to Unity throuhg OSC

meresidency/serial_read_and_OSC_send_Processing_Sketch/oscP5sendReceive/application.macosx/oscP5sendReceive.app

if any error occurs, try run the Processing source file (oscP5sendReceive.pde) and debug. 

port = new Serial(this, Serial.list()[7], 115200);  

When running double check this line to make sure the 7th port is Arduino, it may vary from computers to computers.



2. Since the pulse sensor isn't working perfectly, I havn't implemented the Pulsen Sensor to Generator Control part. So far the generator's light & sound are controlled simply by an endless Enumerator. 



