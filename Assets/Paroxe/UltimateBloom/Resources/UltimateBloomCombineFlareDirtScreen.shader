Shader "Hidden/Ultimate/BloomCombineFlareDirtScreen" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_FlareTexture ("Flare (RGB)", 2D) = "black" {}
	}


	Subshader 
	{
		Pass 
 		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }      

			CGPROGRAM

			#define ULTIMATE_USE_FLARE
			#define ULTIMATE_USE_DIRT
			#define ULTIMATE_SCREEN
			 
			#include "./UltimateBloomCombineCore.cginc"
			 
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag

			ENDCG
		}

		Pass 
 		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }      

			CGPROGRAM

			#define ULTIMATE_USE_FLARE
			#define ULTIMATE_USE_DIRT
			#define ULTIMATE_SCREEN
			 
			#include "./UltimateBloomCombineCore.cginc"

			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment fragINV

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
