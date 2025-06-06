// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Oven/DiffuseShader" {
Properties {
	_MainTex ("Base", 2D) = "black" {}
    _Mixer ("Mixer", 2D) = "black" {}
    _Height ("Height", 2D) = "black" {}
    _GlobalMixer ("GlobalMixer", 2D) = "black" {}            
    _ShadowsAndHeight ("ShadowsAndHeight", 2D) = "black" {}
    _Sea ("Sea", Float) = 0.0
    _Centralization ("Centralization Strength", Float) = 1.0
}


SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
    	
	Pass {  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;

			};

			struct v2f {
				float4 vertex : SV_POSITION;                
				half2 texcoord : TEXCOORD0;
                half2 scrPos : TEXCOORD1;
                fixed4 color : COLOR;
			};

			sampler2D _MainTex;
            sampler2D _Mixer;
            sampler2D _Height;
            sampler2D _GlobalMixer;  
            sampler2D _ShadowsAndHeight;            
			float4 _MainTex_ST;
            float _Sea;
            float _Centralization;
            
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.scrPos = ComputeScreenPos(o.vertex);
                o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col          = tex2D(_MainTex, i.texcoord);
                fixed4 mixer        = tex2D(_Mixer, i.texcoord);
                fixed4 height       = tex2D(_Height, i.texcoord);
                fixed4 globalMixer  = tex2D(_GlobalMixer, i.scrPos);
                fixed4 shadowsAndHeight  = tex2D(_ShadowsAndHeight, i.scrPos);

                /**
                        BLENDING (similar section in mixer and height shader!)
                **/				
                float2 offset = i.texcoord - float2(0.5, 0.5);                                      
                float sqOffset = max( length(offset.x), length(offset.y));

                //lower strength based on distance from center but just a little bit               

                //decreasing defines how big part will get strenght lowered below 1. Eg decreasing 3 means about 1/3 of the texture radius will be decreasing
                //centralization paramter allows to define strength of this modifier from square to horizontal only.  Rivers would mix themselves along one axis only
                float decreasing = 3;
                float strength = min(1, decreasing*(1 - sqOffset*2))* _Centralization + 
                                 min(1, decreasing*(1 - length(offset.x)*2))* (1 - _Centralization); 

                float mixerStr = mixer.x * strength;                                
                                
                //missing strenght is a parameter which tells us what is the "1 - strongles influence" in this pixel. 
                //This way we will fill areas which would not get enought love from any other hex or asset
                float missingStrength = 1 - globalMixer.x;

                float colFill = 0;
                col.a = mixerStr + missingStrength*strength;

                /**
                        ADD WATER BORDERS
                **/
                //we will use height to define water borders

                fixed borderRadius = 0.04;                                                
                fixed borderHeight = 0.55;
                fixed borderValue = borderHeight - shadowsAndHeight.a;
                fixed borderStrength = saturate ((borderValue) / borderRadius);

                //we would expect that this place qualify for depth only if there is hex influence of value way lower than border height
                fixed expectedDepth = step(0.1, borderHeight - height.r);

                // we will zero this influence in places where we do not expect depth
                borderStrength *= expectedDepth; 

                // and last but not least we will ignore border using mixer mask to ensure its not looking like a square
                borderStrength *= mixer.r;

                //define texture overdraw for to custom for sea areas
                if (_Sea > 0.5)
                {
                    col.a = borderStrength;
                }                

                /**
                        BURN SHADOWS AND LIGHT ON TEXTURE
                **/
                float lightAndShadow = ((( shadowsAndHeight.r - 0.5) * 1.3) + 1.1);                
                col.rgb *= lightAndShadow;                                                   

                /**
                        ADD VERTEX BLENDING
                **/
                //add vertex blending if provided. 
                //this way we may add extra  blending based on mesh. 
                //Eg at the beginning and end of the river where we do not want to fight sea depth texture etc
                //default vertex color is (1, 1, 1, 1) so lack of the data will not have effect on this part
                col.a *= i.color.a;    

                /**
                        RETURN
                **/

				return col;
			}
		ENDCG
	}
}

}
