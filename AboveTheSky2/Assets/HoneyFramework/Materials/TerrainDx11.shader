Shader "HoneyFramework/TerrainDx11" 
{
        Properties {
            _Tess ("Tessellation", Range(1,32)) = 4
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _HeightTex ("Height Texture", 2D) = "gray" {}
            _NormalMap ("Normalmap", 2D) = "bump" {}
            _Displacement ("Displacement", Range(0, 3.0)) = 1.5            
            _SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
            //_CustomShading ("Custom Shading", Float) = 0

        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 300
            ZWrite On
            
            CGPROGRAM
            #pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp tessellate:tessFixed nolightmap
            #pragma target 5.0

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            float _Tess;

            float4 tessFixed()
            {
                return _Tess;
            }

            sampler2D _HeightTex;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = (tex2Dlod(_HeightTex, float4(v.texcoord.xy,0,0)).a - 0.5) * _Displacement;
                //if its underground we will scaledown maximum depth
                if (d<0)
                {
                    d *= 0.6;
                }
                v.vertex.xyz += v.normal * d;                
            }

            struct Input {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
            //float _CustomShading;            

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex);
                //float4 addons = tex2D (_HeightTex, IN.uv_MainTex);                
                //float lightAndShadow = (((_CustomShading * addons.g - 0.45) * 3.4) + 1.7);
                                
                o.Albedo = c.rgb;
                //o.Specular = 0.2;
                //o.Gloss = 1.0;
                //o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            }
            ENDCG
        }
        FallBack "Diffuse"
    }