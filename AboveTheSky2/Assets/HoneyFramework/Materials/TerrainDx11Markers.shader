Shader "TerrainDx11Markers"
{
    Properties
    {
            _Tess ("Tessellation", Range(1,32)) = 4
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _HeightTex ("Height Texture", 2D) = "gray" {}
            _NormalMap ("Normalmap", 2D) = "bump" {}
            _Displacement ("Displacement", Range(0, 3.0)) = 1.5            
            _SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
            
            _MarkersGraphic ("Markers Graphic", 2D) = "black" {}
            _MarkersPositionData ("Markers Position Data", 2D) = "black" {}
			//marker settings: (marker graphic count width,
			//                  marker graphic count height, 
			//					marker data width hex count, <- expected to be square for height
			//					marker hex data size, <- number of following pixels of data for each hex
			_MarkerSettings("Marker Settings", vector) = (8, 8, 64, 2) 
            _Color("Base Color", Color) = (0.5, 0.5, 0.5, 1)
            _Cutoff("AlphaCutout", Range(0.0, 1.0)) = 0.5
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline"  "RenderType"="Opaque" }
        LOD 300
        ZWrite On
        Pass
        {
            HLSLPROGRAM
            // Required to compile gles 2.0 with standard srp library

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma vertex vert
            #pragma fragment frag
            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            TEXTURE2D(_HeightTex);
            TEXTURE2D(_MarkersPositionData);
            TEXTURE2D(_MarkersGraphic);

            SAMPLER(sampler_MainTex);
            SAMPLER(sampler_HeightTex);
            SAMPLER(sampler_MarkersPositionData);
            SAMPLER(sampler_MarkersGraphic);
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float4 _Color;
            float _Cutoff;
            float4 _MarkerSettings;		
            float _Displacement;

            CBUFFER_END
            struct Attributes
            {
                float4 positionOS       : POSITION;
                float2 uv               : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct Varyings
            {
                float2 uv        : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes input)
            {
                Varyings output = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.vertex = vertexInput.positionCS;
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.color = input.vertexColor;

                float d = (SAMPLE_TEXTURE2D_LOD(_HeightTex,sampler_HeightTex, output.uv, 0).a - 0.5) * _Displacement;
                //if its underground we will scaledown maximum depth
                if (d < 0)
                {
                    d *= 0.6;
                }
                output.vertex.xyz += output.normal * d;



                return output;
            }

             //------- UTILS --------
            struct Vector3i 
            {
                int x;
                int y;
                int z;

                //this value tells us uv of the hex. which is 0,0 in one corner and 1,1 in the oposite. 
                //Perfect to draw texture for teh hex. Note that only single hex will have influence in each pixel! no blenddrawings here!
                float2 uv;
            };

                        //converts integer hex coordinates into flat world position used for uv and other 2d scapce calculations
            float2 ConvertToPosition(Vector3i v)
            {
                float cos30 = sqrt(3.0) * 0.5;

                float2 X = float2(1, 0);
                float2 Y = float2(-0.5, cos30);
                float2 Z = float2(-0.5, -cos30);
                return X * v.x + Y * v.y + Z * v.z;                
            }

            //this code expects hex radius to be 1 for simplification. 
            Vector3i GetHexCoord(float2 pos)
            {
                //Convert world flat coordinates into hex FLOAT position
                float TWO_THIRD = 2.0 / 3.0;
                float ONE_THIRD = 1.0 / 3.0;
                float COMPONENT = ONE_THIRD * sqrt(3.0);

                float x = TWO_THIRD * pos.x;
                float y = (COMPONENT * pos.y - ONE_THIRD * pos.x);
                float z = -x - y;

                //we cant use floating hex position, so before return we need to convert it to integer.
                //also its important to understand that floating point position contains some artifacts if converted separately into integers
                //we have to do some post-calculation cleanup to be able to recover from them. 
                Vector3i v;
                v.x = round(x);
                v.y = round(y);
                v.z = round(z);

                //find delta between rounded and original value
                float dx = abs(v.x - x);
                float dy = abs(v.y - y);
                float dz = abs(v.z - z);

                //value which after rounding get most offset contains biggest artifacts, we want to discard it and recover form {a + b + c = 0} equation
                if (dz > dy && dz > dx) 
                { 
                    v.z = -v.x - v.y; 
                }
                else if (dy > dx) 
                { 
                    v.y = -v.x - v.z; 
                }
                else 
                { 
                    v.x = -v.y - v.z; 
                }

                //recover delta between testpoint and hex center as UV

                float2 center = ConvertToPosition(v);
                float2 offset = pos - center;
                v.uv = offset*0.5 + float2(0.5, 0.5);

                return v;
            }
            #define TWO_PI 6.28
            half4 frag(Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
                half2 uv = IN.uv;
                float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                float dataResolution = _MarkerSettings.z;
				int dataSize = round( _MarkerSettings.w);

                float2 pos = float2(IN.vertex.x, IN.vertex.z);
                Vector3i v = GetHexCoord(pos);
				float trueDataResolution = dataSize*dataResolution;
				
                float2 dataUV = float2( (v.x*dataSize+0.5) /trueDataResolution, (v.y*dataSize+0.5)/trueDataResolution);
                float2 data2UV = dataUV + float2(1.0/trueDataResolution, 0);	

                float4 data = SAMPLE_TEXTURE2D (_MarkersPositionData, sampler_MarkersPositionData, dataUV); 
				float4 data2 = SAMPLE_TEXTURE2D (_MarkersPositionData, sampler_MarkersPositionData, data2UV);
				
                float xCoord;
				float yCoord;
				

				float2 markerUV;
				half4 marker;
				float2 markerCorner;


                // 1st marker layer
                int type = round(data.r * _MarkerSettings.x * _MarkerSettings.y);
                if (type != 0)
				{
					float2 markerPoint = float2(v.uv.x, v.uv.y) - float2(0.5, 0.5);
					
					//Short way of 2d rotation matrix
					float angle =  TWO_PI * data2.r;
					float cosRot = cos(angle);
					float sinRot = sin(angle);
					float2 singleMarkerUV = float2(markerPoint.x * cosRot - markerPoint.y * sinRot,
												   markerPoint.y * cosRot + markerPoint.x * sinRot);

					//Index of the type in atlas column and row
					int typeX = fmod(type, _MarkerSettings.x);  
					int typeY = floor(_MarkerSettings.y - (type + 0.01) / _MarkerSettings.x);

					float2 atlasMarkerUV = singleMarkerUV + float2(0.5, 0.5) + float2(typeX, typeY);

					//make atlas UV to be within 0-1
					atlasMarkerUV.x = atlasMarkerUV.x / _MarkerSettings.x;
					atlasMarkerUV.y = atlasMarkerUV.y / _MarkerSettings.y;
					
					marker = SAMPLE_TEXTURE2D (_MarkersGraphic, sampler_MarkersGraphic, atlasMarkerUV); 
					c.rgb = c.rgb * (1 - marker.a) + marker.rgb * (marker.a);						
				}

                // 2nd marker layer
                type = round(data.g * _MarkerSettings.x * _MarkerSettings.y);
				if (type != 0)
				{
					float2 markerPoint = float2(v.uv.x, v.uv.y) - float2(0.5, 0.5);
					
					//Short way of 2d rotation matrix
					float angle =  TWO_PI * data2.g;
					float cosRot = cos(angle);
					float sinRot = sin(angle);
					float2 singleMarkerUV = float2(markerPoint.x * cosRot - markerPoint.y * sinRot,
												   markerPoint.y * cosRot + markerPoint.x * sinRot);

					//Index of the type in atlas column and row
					int typeX = fmod(type, _MarkerSettings.x);  
					int typeY = floor(_MarkerSettings.y - (type + 0.01) / _MarkerSettings.x);

					float2 atlasMarkerUV = singleMarkerUV + float2(0.5, 0.5) + float2(typeX, typeY);

					//make atlas UV to be within 0-1
					atlasMarkerUV.x = atlasMarkerUV.x / _MarkerSettings.x;
					atlasMarkerUV.y = atlasMarkerUV.y / _MarkerSettings.y;
					
					marker = SAMPLE_TEXTURE2D (_MarkersGraphic, sampler_MarkersGraphic, atlasMarkerUV); 
					c.rgb = c.rgb * (1 - marker.a) + marker.rgb * (marker.a);						
				}

                // 3rd marker layer
                type = round(data.b * _MarkerSettings.x * _MarkerSettings.y);
				if (type != 0)
				{
					float2 markerPoint = float2(v.uv.x, v.uv.y) - float2(0.5, 0.5);
					
					//Short way of 2d rotation matrix
					float angle =  TWO_PI * data2.b;
					float cosRot = cos(angle);
					float sinRot = sin(angle);
					float2 singleMarkerUV = float2(markerPoint.x * cosRot - markerPoint.y * sinRot,
												   markerPoint.y * cosRot + markerPoint.x * sinRot);

					//Index of the type in atlas column and row
					int typeX = fmod(type, _MarkerSettings.x);  
					int typeY = floor(_MarkerSettings.y - (type + 0.01) / _MarkerSettings.x);

					float2 atlasMarkerUV = singleMarkerUV + float2(0.5, 0.5) + float2(typeX, typeY);

					//make atlas UV to be within 0-1
					atlasMarkerUV.x = atlasMarkerUV.x / _MarkerSettings.x;
					atlasMarkerUV.y = atlasMarkerUV.y / _MarkerSettings.y;
					
					marker = SAMPLE_TEXTURE2D (_MarkersGraphic, sampler_MarkersGraphic, atlasMarkerUV); 
					c.rgb = c.rgb * (1 - marker.a) + marker.rgb * (marker.a);						
				}
                // 4th marker layer
                type = round(data.a * _MarkerSettings.x * _MarkerSettings.y);
				if (type != 0)
				{
					float2 markerPoint = float2(v.uv.x, v.uv.y) - float2(0.5, 0.5);
					
					//Short way of 2d rotation matrix
					float angle =  TWO_PI * data2.a;
					float cosRot = cos(angle);
					float sinRot = sin(angle);
					float2 singleMarkerUV = float2(markerPoint.x * cosRot - markerPoint.y * sinRot,
												   markerPoint.y * cosRot + markerPoint.x * sinRot);

					//Index of the type in atlas column and row
					int typeX = fmod(type, _MarkerSettings.x);  
					int typeY = floor(_MarkerSettings.y - (type + 0.01) / _MarkerSettings.x);

					float2 atlasMarkerUV = singleMarkerUV + float2(0.5, 0.5) + float2(typeX, typeY);

					//make atlas UV to be within 0-1
					atlasMarkerUV.x = atlasMarkerUV.x / _MarkerSettings.x;
					atlasMarkerUV.y = atlasMarkerUV.y / _MarkerSettings.y;
					
					marker = SAMPLE_TEXTURE2D (_MarkersGraphic, sampler_MarkersGraphic, atlasMarkerUV); 
					c.rgb = c.rgb * (1 - marker.a) + marker.rgb * (marker.a);						
				}   

                // half3 color = texColor.rgb * _Color.rgb;
                // half alpha = texColor.a * _Color.a;
                // AlphaDiscard(alpha, _Cutoff);

                return c;
            }
            ENDHLSL
        }
    }
}
