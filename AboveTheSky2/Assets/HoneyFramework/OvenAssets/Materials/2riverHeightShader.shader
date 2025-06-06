// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Oven/RiverHeightShaderShader" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "black" {}
    _Mixer ("Mixer (RGB) Trans (A)", 2D) = "black" {}
    _GlobalMixer ("_GlobalMixer (RGB) Trans (A)", 2D) = "black" {}
    _Centralization ("Centralization Strength", Float) = 1.0
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha    
    BlendOp Min

	Pass {  
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
				        
                col.rgb = (1 - mixer.x) * 0.5 + 0.46;
                col.a = 1;
                
				return col;
			}
		ENDCG
	}
}

}
