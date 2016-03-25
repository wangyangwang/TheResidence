using UnityEngine;
using System.Collections;

public class addCollidersToChildren : MonoBehaviour {

	Transform[] allChildren;
	// Use this for initialization
	void Start () {
		allChildren = gameObject.GetComponentsInChildren<Transform>();
		foreach(Transform t in allChildren) {
			t.gameObject.AddComponent<MeshCollider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
