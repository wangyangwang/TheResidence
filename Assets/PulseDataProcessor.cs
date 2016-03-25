using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PulseDataProcessor : MonoBehaviour {
	
	public delegate void del();
	public static event del heartJustBeat;


	public GameObject oscReceiverGaOb;
	int initialBeatCounter;
	private int beatCounter;


	void Start () {
		StartCoroutine("WaitAndBeat");
		initialBeatCounter = oscReceiverGaOb.GetComponent<oscReceiver>().rawPulseData;
		beatCounter = 0;
	}

	
	void Update () {
		beatCounter = oscReceiverGaOb.GetComponent<oscReceiver>().rawPulseData;

	}


	IEnumerator WaitAndBeat() {
		while(true)
		{
			if(heartJustBeat!=null){
				heartJustBeat();
			}
			yield return new WaitForSeconds(2.0f);
		}
	}
		


}
