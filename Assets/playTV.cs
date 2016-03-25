using UnityEngine;
using System.Collections;

public class playTV : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// this line of code will make the Movie Texture begin playing
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
