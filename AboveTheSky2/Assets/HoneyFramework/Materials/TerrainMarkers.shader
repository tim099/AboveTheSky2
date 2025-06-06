Shader "HoneyFramework/TerrainMarkers" 
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}        
		_MarkersGraphic ("Markers Graphic", 2D) = "black" {}
            _MarkersPositionData ("Markers Position Data", 2D) = "black" {}
			//marker settings: (marker graphic count width,
			//                  marker graphic count height, 
			//					marker data width hex count, <- expected to be square for height
			//					marker hex data size, <- number of following pixels of data for each hex
			_MarkerSettings("Marker Settings", vector) = (8, 8, 64, 2) 
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 300
            
        CGPROGRAM
        #pragma surface surf BlinnPhong addshadow fullforwardshadows nolightmap
        #pragma target 3.0
        #pragma glsl
		
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

        struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
        };

        struct Input {
            float2 uv_MainTex;
			float3 worldPos;
        };

        sampler2D _MainTex;
        sampler2D _NormalMap;
		sampler2D _MarkersGraphic;
        sampler2D _MarkersPositionData;
		float4 _MarkerSettings;	

        void surf (Input IN, inout SurfaceOutput o)
		 {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);                
               
            float dataResolution = _MarkerSettings.z;
			int dataSize = round( _MarkerSettings.w);
                
            float2 pos = float2(IN.worldPos.x, IN.worldPos.z);
            Vector3i v = GetHexCoord(pos);
			float trueDataResolution = dataSize*dataResolution;
				
            float2 dataUV = float2( (v.x*dataSize+0.5) /trueDataResolution, (v.y*dataSize+0.5)/trueDataResolution);
			float2 data2UV = dataUV + float2(1.0/trueDataResolution, 0);								

            float4 data = tex2D (_MarkersPositionData, dataUV); 
			float4 data2 = tex2D (_MarkersPositionData, data2UV);

			float xCoord;
			float yCoord;
				

			float2 markerUV;
			half4 marker;
			float2 markerCorner;
			const float TWO_PI = 6.28;	
								

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
					
				marker = tex2D (_MarkersGraphic, atlasMarkerUV); 
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
					
				marker = tex2D (_MarkersGraphic, atlasMarkerUV); 
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
					
				marker = tex2D (_MarkersGraphic, atlasMarkerUV); 
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
					
				marker = tex2D (_MarkersGraphic, atlasMarkerUV); 
				c.rgb = c.rgb * (1 - marker.a) + marker.rgb * (marker.a);						
			}           
				
            o.Albedo = c.rgb;              
        }
        ENDCG
    }
    FallBack "Diffuse"
}