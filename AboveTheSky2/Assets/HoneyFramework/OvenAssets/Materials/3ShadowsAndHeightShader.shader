// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Oven/ShadowsAndHeightShader" {
Properties {	
    _AOHeight ("_AOHeight", 2D) = "black" {}    
    _AOHeightBlur ("_AOHeightBlur", 2D) = "black" {}
    _Height ("_Height", 2D)     = "black" {}    
    _Height1 ("_Height1", 2D)   = "black" {}
    _Height2 ("_Height2", 2D)   = "black" {}    
    
    _ShadowStrenght ("Shadow strenght", Float) = 0.6    
}

SubShader {
	Tags {"Queue"="Geometry" "RenderType"="Opaque"}
	LOD 100
	
	ZWrite Off
		
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
			
            sampler2D _AOHeight;
            sampler2D _AOHeightBlur;
            sampler2D _Height;
            sampler2D _Height1;
            sampler2D _Height2;
            
            float _ShadowStrenght;
            float _AOStrenght;
            float _AOScanDistance;
            float _AOBorderSize;
            float4 _MainTex_ST;           
    		
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
                float4 color = 1;

                /**
                        HEIGHT
                **/
                fixed4 trueHeight = tex2D(_Height, i.scrPos);
                color.a = trueHeight.r;                                

                /**
                        SHADOWS
                **/                
				fixed4 offHeight1 = tex2D(_Height1, i.scrPos);
                fixed4 offHeight2 = tex2D(_Height2, i.scrPos);
				              
                fixed baseH = (trueHeight.x ) ;
                fixed helperHeight1 = (offHeight1.x) ;
                fixed helperHeight2 = (offHeight2.x) ;
               
                fixed light1 = saturate(baseH - helperHeight1 ) * _ShadowStrenght * 0.5;                
                fixed shadow1 = saturate(helperHeight1 - baseH) * _ShadowStrenght;
                light1 *= saturate( baseH -0.5) * 3; //lights stronget above the gound only

                fixed light2 = saturate(baseH - helperHeight2 ) * _ShadowStrenght * 0.5;                
                fixed shadow2 = saturate(helperHeight2 - baseH) * _ShadowStrenght;
                light2 *= saturate( baseH -0.5) * 3; //lights stronget above the gound only

                //in one pixel there is always only either light or shadow from one layer, 
                //but it is possible that on turnarount shapes that you will get a bit of shadow and light which will neutralize each other
                color.r = max(light1, light2) - ((shadow1 + shadow2) * 0.5) + 0.5;
                      				
				return color;				
			}
		ENDCG
	}
}

}
