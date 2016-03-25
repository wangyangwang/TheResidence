using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class oscReceiver : MonoBehaviour {

	private string UDPHost = "127.0.0.1";
	private int listenerPort = 22345;
	private int broadcastPort = 57131;
	private Osc oscHandler;
		
	private string eventName = "";
	private string eventData = "";
	public int inputData = 0;


	public float rawBreathData = 0;
	public int rawPulseData = 0;

	// Use this for initialization
	void Start () {
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(UDPHost,broadcastPort,listenerPort);
		oscHandler = GetComponent<Osc>();
		oscHandler.init(udp);
		oscHandler.SetAllMessageHandler(getInput);	
	}

	public void getInput(OscMessage oscMessage) {
		if(oscMessage.Address=="/pulse"){
			try {
				if(oscMessage.Values.Count != 0){
					rawPulseData = int.Parse(oscMessage.Values[0].ToString());
				}
			}catch(Exception e){
				if(e.Source!=null){
					Debug.LogError(e);
				}
			}

		}else if(oscMessage.Address=="/breath"){
			try {
				if(oscMessage.Values.Count != 0){
					rawBreathData = float.Parse(oscMessage.Values[0].ToString());
				}
			}catch(Exception e){
				if(e.Source!=null){
					Debug.LogError(e);
				}
			}
		}

//		Debug.Log("getInput function");
//		inputData =  Convert.ToInt32(oscMessage.Values[0]); // Int32.Parse(oscMessage.Values[0]);
//		Debug.Log(inputData);
	}



	public int getData(){
		return inputData;
	}

}
