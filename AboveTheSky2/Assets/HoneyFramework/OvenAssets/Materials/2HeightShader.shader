// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Oven/HeightShader" 
{

Properties 
{
	_MainTex ("Base (RGB) Trans (A)", 2D) = "black" {}
    _Mixer ("Mixer (RGB) Trans (A)", 2D) = "black" {}
    _GlobalMixer ("_GlobalMixer (RGB) Trans (A)", 2D) = "black" {}
    _Centralization ("Centralization Strength", Float) = 1.0
}

SubShader 
{
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
    	
	Pass 
    {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;                
				half2 texcoord : TEXCOORD0;
                half2 scrPos : TEXCOORD1;
			};

			sampler2D _MainTex;
            sampler2D _Mixer;
            sampler2D _GlobalMixer;
			float4 _MainTex_ST;
            float _Centralization;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.scrPos = ComputeScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.texcoord);
                fixed4 mixer = tex2D(_Mixer, i.texcoord);
                fixed4 globalMixer = tex2D(_GlobalMixer, i.scrPos);
				
                //Offset from texture center
                float2 offset = i.texcoord - float2(0.5, 0.5);

                //lower strength based on distance from center but just a little bit                                      
                float sqOffset = max( length(offset.x), length(offset.y));

                //decreasing defines how big part will get strenght lowered below 1. Eg decreasing 3 means about 1/3 of the texture radius will be decreasing
                //centralization paramter allows to define strength of this modifier.   Rivers would mix themselves along one axis only
                float decreasing = 3;
                float strength = min(1, decreasing*(1 - sqOffset*2))* _Centralization + 
                                 min(1, decreasing*(1 - length(offset.x)*2))* (1 - _Centralization); 
                float mixerStr = mixer.x * strength;                                
                                
                //missing strenght is a parameter which tells us what is the "1 - strongles influence" in this pixel. 
                //This way we will fill areas which would not get enought love from any other hex or asset
                float missingStrength = 1 - globalMixer.x;

                col.a = mixerStr + missingStrength*strength;
                
				return col;
			}
		ENDCG
	}
}

}
