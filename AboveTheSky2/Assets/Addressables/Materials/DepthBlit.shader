Shader "DepthBlit"
{
        SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "DepthBlitPass"

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // The Blit.hlsl file provides the vertex shader (Vert),
            // input structure (Attributes) and output strucutre (Varyings)
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/PostProcessing/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            #pragma vertex Vert
            #pragma fragment frag

            TEXTURE2D_X(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);

            float _Intensity;

            half4 frag (Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                //float depth = SampleSceneDepth(input.texcoord);
                real2 UV = input.texcoord;
#if UNITY_REVERSED_Z
                real depth = SampleSceneDepth(UV);
#else
                // Adjust z to match NDC for OpenGL
                real depth = lerp(UNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(UV));
#endif
//                float3 worldPos = ComputeWorldSpacePosition(UV, depth, UNITY_MATRIX_I_VP);
//                uint scale = 20;
//                uint3 worldIntPos = uint3(abs(worldPos.xyz * scale));
//                bool white = (worldIntPos.x & 1) ^ (worldIntPos.y & 1) ^ (worldIntPos.z & 1);

//                #if UNITY_REVERSED_Z
//                    // Case for platforms with REVERSED_Z, such as D3D.
//                    if(depth < 0.0001)
//                        return half4(0,0,0,1);
//                #else
//                    // Case for platforms without REVERSED_Z, such as OpenGL.
//                    if(depth > 0.9999)
//                        return half4(0,0,0,1);
//                #endif

//                return white ? half4(1,1,1,1) : half4(0,0,0,1);


                return float4(depth, depth, depth, 1);

                //float4 _color = SAMPLE_TEXTURE2D_X(_CameraOpaqueTexture, sampler_CameraOpaqueTexture, input.texcoord);

                //return _color * float4(1, _Intensity, 1, 1);
            }
            ENDHLSL
        }
    }
}