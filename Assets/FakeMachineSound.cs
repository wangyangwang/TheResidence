using UnityEngine;
using System.Collections;

public class FakeMachineSound : MonoBehaviour {

	public float maxVol = 1.0f;

	void OnEnable(){
		PulseDataProcessor.heartJustBeat += RoarUp;
	}

	void OnDisable(){
		PulseDataProcessor.heartJustBeat -= RoarUp;
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<AudioSource>().volume >= 0){
			GetComponent<AudioSource>().volume -= 0.01f;
		}
	}

	void RoarUp(){
		GetComponent<AudioSource>().volume = maxVol;
	}

}
