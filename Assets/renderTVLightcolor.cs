using UnityEngine;
using System.Collections;

public class renderTVLightcolor : MonoBehaviour {
	public Light tvLightEmiter;

	RenderTexture tvContent;
	Camera tvCamera;

	// Use this for initialization
	void Start () {
		tvContent = RenderTexture.GetTemporary(16,16,24);
		tvCamera = GetComponent<Camera>();
		tvCamera.targetTexture = tvContent;
		RenderTexture.active = tvContent;
	}
	
	// Update is called once per frame
	void Update () {
		tvCamera.Render();
	}

	void OnPostRender() {

			Texture2D tex = new Texture2D(16,16,TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0,0,16,16),0,0);
			tex.Apply();
			float r = 0;
			float g = 0;
			float b = 0;
			int counter = 0;
			for (int y = 0; y < tex.height; y++) {
				for (int x = 0; x < tex.width; x++) {
					r += tex.GetPixel(x,y).r;
					g += tex.GetPixel(x,y).g;
					b += tex.GetPixel(x,y).b;
					counter++;
				}
			}

			Color averageColor = new Color(r/counter,g/counter,b/counter);
			
			tvLightEmiter.color = averageColor;

	}


}
