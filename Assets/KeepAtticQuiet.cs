using UnityEngine;
using System.Collections;

public class KeepAtticQuiet : MonoBehaviour {
	//when controller's y is bigger than 1.3, start to decrese sound vol
	//completely mute them when controller's y is higher than 13.3

	public float startFakingYpos = 1.3f;
	public float mutedYpos = 13.3f;

	public GameObject controller;
	public GameObject generatorSound;
	public GameObject showerSound;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float currentYpos = controller.transform.position.y;
		if(controller.transform.position.y > 1.3f){
			//because the volumn of Generator is constantly chaning so we need to maxVol instead of volumn.
			generatorSound.GetComponent<FakeMachineSound>().maxVol = map(currentYpos,startFakingYpos,mutedYpos,1,0);
			showerSound.GetComponent<AudioSource>().volume = map(currentYpos,startFakingYpos,mutedYpos,1,0);
		}
	}

	//Map function is back!!
	float map(float value, 
		float istart, 
		float istop, 
		float ostart, 
		float ostop) {
		return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
	}

}
