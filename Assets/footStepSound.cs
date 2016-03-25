using UnityEngine;
using System.Collections;

public class footStepSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.W))
		{
			Debug.Log("on W");
			GetComponent<AudioSource>().Play();
		}else{
			GetComponent<AudioSource>().Stop();
		}

	}
}
