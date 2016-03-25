import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import oscP5.*; 
import netP5.*; 
import processing.serial.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class oscP5sendReceive extends PApplet {

/**
 * oscP5sendreceive by andreas schlegel
 * example shows how to send and receive osc messages.
 * oscP5 website at http://www.sojamo.de/oscP5
 */






int beatCount = 0;
Serial port; 

OscP5 oscP5;
NetAddress myRemoteLocation;
boolean beat;
int broadcastingPort = 22345;

String breathData;

public void setup() {
  size(400, 400);
  /* start oscP5, listening for incoming messages at port 12345 */
  oscP5 = new OscP5(this, 12345);

  /* myRemoteLocation is a NetAddress. a NetAddress takes 2 parameters,
   * an ip address and a port number. myRemoteLocation is used as parameter in
   * oscP5.send() when sending osc packets to another computer, device, 
   * application. usage see below. for testing purposes the listening port
   * and the port of the remote location address are the same, hence you will
   * send messages back to this sketch.
   */

  myRemoteLocation = new NetAddress("127.0.0.1", broadcastingPort);

  
  //serial
  port = new Serial(this, Serial.list()[7], 115200);  // make sure Arduino is talking serial at this baud rate
  port.clear();            // flush buffer
  port.bufferUntil('\n');
}


public void draw() {
  background(0);  
  /* in the following different ways of creating osc messages are shown by example */
  OscMessage myMessage1 = new OscMessage("/pulse");
  OscMessage myMessage2 = new OscMessage("/breath");

  myMessage1.add(beat); /* add an int to the osc message */
  text("sending osc data to " + broadcastingPort, 20, 20);
  text("beat counter: "+ beatCount, 20, 40);
  text("breath raw value: " + Float.parseFloat(breathData), 20, 60);

  //  println(Float.parseFloat(breathData));
  myMessage2.add(Float.parseFloat(breathData));
  myMessage1.add(beatCount);
  /* send the message */
  oscP5.send(myMessage1, myRemoteLocation);
  oscP5.send(myMessage2, myRemoteLocation);

  //  if (beat) {
  //    beat = false;
  //  }
}


public void serialEvent(Serial port) { 
  String inData = port.readStringUntil('\n');

  if (inData == null) {                 // bail if we didn't get anything
    return;
  }   
  if (inData.isEmpty()) {                // bail if we got an empty line
    return;
  }
  inData = trim(inData);                 // cut off white space (carriage return)   
  if (inData.length() <= 0) {             // bail if there's nothing there
    return;
  }

  if (inData.charAt(0) == 'B') {        
    beatCount++;
  }
  if (inData.charAt(0) =='I') {
    //    println("breath data: " + inData.substring(1));
    breathData = inData.substring(1);
  }
}

  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "oscP5sendReceive" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
