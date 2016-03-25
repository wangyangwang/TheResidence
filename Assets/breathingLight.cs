using UnityEngine;
using System.Collections;

public class breathingLight : MonoBehaviour {


	public GameObject sensorAnalyzer;
	Light light;
	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sensorAnalyzer.GetComponent<BreathDataProcesser>().isInhaling){
			if(light.intensity <= 6.5f) {
				light.intensity += 0.09f;
			}
		}else{
			if(light.intensity > 0){
				light.intensity  -= 0.09f;
			}
		}
	}
}
