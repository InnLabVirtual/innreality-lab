Shader "GTS/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_OutLineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutLineSize("Outline Width", Range(1.0, 5.0)) = 1.01
    }

	CGINCLUDE	
	#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION,
		float3 normal: NORMAL
	}

	struct v2f 
	{
		float4 pos : POSITION,
		float4 color: COLOR,
		float3 normal : NORMAL
	}

	float _OutLineColor;
	float _OutLineSize;
	
		_OutLineColor("Outline Color", Color) = (0, 0, 0, 1)
		_OutLineSize("Outline Width", Range(1.0, 5.0)) = 1.01

	v2f vert(appdata v){
		v.vertex.xyz *= _Outline;

		v2f o;

	}

	ENDCG

    SubShader
    {
       Pass
	   {
		#pragma vertex vert 
	   }
    }
}
