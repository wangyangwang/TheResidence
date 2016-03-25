Shader "Hidden/Ultimate/FlareSingle" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}


	Subshader 
	{
		Pass 
 		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }      

			CGPROGRAM

			#include "./UltimateFlareCore.cginc"
			 
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
