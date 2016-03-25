using UnityEngine;
using System.Collections;

public class blinkMe : MonoBehaviour {


	void OnEnable(){
		PulseDataProcessor.heartJustBeat += BrightUp;
	}

	void OnDisable(){
		PulseDataProcessor.heartJustBeat -= BrightUp;
	}
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Light>().intensity >= 0){
			GetComponent<Light>().intensity-=0.16f;
		}
	}

	void BrightUp(){
		GetComponent<Light>().intensity = 8.0f;
	}


}
