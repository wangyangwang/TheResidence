using UnityEngine;
using System.Collections;

public class BloomControl : MonoBehaviour {

	GameObject centerEyeAnchor;
	GameObject FPS;

	// Use this for initialization
	void Start () {
		centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
		FPS = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float bloomMax = 0.8f;
		if(FPS.transform.position.y > 13.0f){
			centerEyeAnchor.GetComponent<UltimateBloom>().m_BloomIntensity = Map(FPS.transform.position.y,5.24f,8.5f,0,bloomMax);
			if(centerEyeAnchor.GetComponent<UltimateBloom>().m_BloomIntensity > bloomMax){
				centerEyeAnchor.GetComponent<UltimateBloom>().m_BloomIntensity = bloomMax;
			}
		}else if(FPS.transform.position.y <= 10.0f){
			centerEyeAnchor.GetComponent<UltimateBloom>().m_BloomIntensity = 0;
		}
}



	float Map(float value, float istart, float istop, float ostart, float ostop) {
		return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
	}

}