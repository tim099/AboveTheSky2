Shader "HoneyFramework/Terrain" 
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}        
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 300
            
        CGPROGRAM
        #pragma surface surf BlinnPhong addshadow fullforwardshadows nolightmap
        #pragma target 3.0
        #pragma glsl

        struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
        };        

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;
        sampler2D _NormalMap;

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Specular = 0.2;
            o.Gloss = 1.0;
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
        }
        ENDCG
    }
    FallBack "Diffuse"
}