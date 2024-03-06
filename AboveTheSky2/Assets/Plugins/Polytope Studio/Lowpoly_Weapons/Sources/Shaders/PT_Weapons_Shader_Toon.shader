// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Polytope Studio/ PT_Medieval Weapons Shader Toon"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][HDR]_COATOFARMSCOLOR("COAT OF ARMS COLOR", Color) = (1,0,0,0)
		[HDR]_METAL1COLOR("METAL 1 COLOR", Color) = (0.7261481,0.7735849,0.7528313,0)
		[NoScaleOffset]_COATOFARMSMASK("COAT OF ARMS MASK", 2D) = "black" {}
		_CEL1SIZE("CEL 1 SIZE", Range( 0 , 1)) = 0.1
		_CEL2SIZE("CEL 2 SIZE", Range( 0 , 1)) = 0.4
		[HDR]_METAL2COLOR("METAL 2 COLOR", Color) = (1.678431,1.003922,0.1176471,0)
		_CEL3SIZE("CEL 3 SIZE", Range( 0 , 1)) = 0.8
		_CEL1POWER("CEL 1 POWER", Range( 0 , 1)) = 0.15
		_CEL2POWER("CEL 2 POWER", Range( 0 , 1)) = 0.15
		[HDR]_METAL3COLOR("METAL 3 COLOR", Color) = (0.597023,0.6237553,0.7395956,0)
		_CEL3POWER("CEL 3 POWER", Range( 0 , 1)) = 0.15
		[HDR]_CEL1COLOR("CEL 1 COLOR", Color) = (1,1,1,0)
		[HDR]_CEL2COLOR("CEL 2 COLOR", Color) = (1,1,1,0)
		[HDR]_METAL4COLOR("METAL 4 COLOR", Color) = (0.8791043,0.8044721,0.9422547,0)
		[HDR]_CEL3COLOR("CEL 3 COLOR", Color) = (1,1,1,0)
		[HDR]_WOOD1COLOR("WOOD 1 COLOR", Color) = (0.1981132,0.08345769,0.06261124,0)
		[HDR]_WOOD2COLOR("WOOD 2 COLOR", Color) = (0.1320755,0.06452555,0.05420079,0)
		[HDR]_WOOD3COLOR("WOOD 3 COLOR", Color) = (0.1037736,0.07509367,0.04650232,0)
		[HDR]_LEATHER1COLOR("LEATHER 1 COLOR", Color) = (0.2924528,0.1296404,0.09242612,1)
		[HDR]_LEATHER2COLOR("LEATHER 2 COLOR", Color) = (0.06603771,0.03523636,0.03146137,1)
		[HDR]_LEATHER3COLOR("LEATHER 3 COLOR", Color) = (0.1320755,0.03139969,0.02180491,1)
		[HDR]_PAINT1COLOR("PAINT 1 COLOR", Color) = (0.5450981,0.6936808,0.6980392,0)
		[HDR]_PAINT2COLOR("PAINT 2 COLOR", Color) = (0.3649431,0.5566038,0.4386422,0)
		[HDR]_PAINT3COLOR("PAINT 3 COLOR", Color) = (0.5849056,0.5418971,0.4331613,0)
		[HDR]_GEMS1COLOR("GEMS 1 COLOR", Color) = (1,0,0,0)
		[HDR]_GEMS2COLOR("GEMS 2 COLOR", Color) = (0,0.3218706,0.5754717,0)
		[HDR]_GEMS3COLOR("GEMS 3 COLOR", Color) = (0,0.4716981,0.1359325,0)
		[HDR]_FEATHERS1COLOR("FEATHERS 1 COLOR", Color) = (0.3301887,0.1241556,0.04516733,0)
		[HDR]_FEATHERS2COLOR("FEATHERS 2 COLOR", Color) = (0.509434,0.4260285,0.1802243,0)
		[HDR]_FEATHERS3COLOR("FEATHERS 3 COLOR", Color) = (0.509434,0.25712,0.25712,0)
		[HDR]_FEATHERS4COLOR("FEATHERS 4 COLOR", Color) = (0.8113208,0.2104842,0.2104842,0)
		[HDR]_FEATHERS5COLOR("FEATHERS 5 COLOR", Color) = (0.4150943,0.2615769,0.1468494,0)
		[ASEEnd][HDR]_FEATHERS6COLOR("FEATHERS 6 COLOR", Color) = (0.7924528,0.7444169,0.6391954,0)
		[HideInInspector]_TextureSample2("Texture Sample 2", 2D) = "white" {}
		[HideInInspector]_TextureSample9("Texture Sample 9", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}


		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25

		[HideInInspector][ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[HideInInspector][ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0
		[HideInInspector][ToggleOff] _ReceiveShadows("Receive Shadows", Float) = 1.0
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" "Queue"="Geometry" }

		Cull Back
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		AlphaToMask Off

		

		HLSLINCLUDE
		#pragma target 3.5
		#pragma prefer_hlslcc gles
		// ensure rendering platforms toggle list is visible

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}

		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma shader_feature_local _RECEIVE_SHADOWS_OFF
			#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF

			
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
		

			

			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile_fragment _ _SHADOWS_SOFT
			#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK

			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_FORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
				#define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
			#define ASE_NEEDS_FRAG_WORLD_NORMAL


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 lightmapUVOrVertexSH : TEXCOORD0;
				half4 fogFactorAndVertexLight : TEXCOORD1;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					float4 shadowCoord : TEXCOORD2;
				#endif
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					float4 screenPos : TEXCOORD6;
				#endif
				float4 ase_texcoord7 : TEXCOORD7;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _COATOFARMSMASK;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord7.xy = v.texcoord.xy;
				o.ase_texcoord7.zw = v.texcoord1.xyzw.xy;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 positionVS = TransformWorldToView( positionWS );
				float4 positionCS = TransformWorldToHClip( positionWS );

				VertexNormalInputs normalInput = GetVertexNormalInputs( v.ase_normal, v.ase_tangent );

				o.tSpace0 = float4( normalInput.normalWS, positionWS.x);
				o.tSpace1 = float4( normalInput.tangentWS, positionWS.y);
				o.tSpace2 = float4( normalInput.bitangentWS, positionWS.z);

				#if defined(LIGHTMAP_ON)
					OUTPUT_LIGHTMAP_UV( v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy );
				#endif

				#if !defined(LIGHTMAP_ON)
					OUTPUT_SH( normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					o.lightmapUVOrVertexSH.zw = v.texcoord;
					o.lightmapUVOrVertexSH.xy = v.texcoord * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				half3 vertexLight = VertexLighting( positionWS, normalInput.normalWS );

				#ifdef ASE_FOG
					half fogFactor = ComputeFogFactor( positionCS.z );
				#else
					half fogFactor = 0;
				#endif

				o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;

				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					o.screenPos = ComputeScreenPos(positionCS);
				#endif

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord1;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE)
				#define ASE_SV_DEPTH SV_DepthLessEqual
			#else
				#define ASE_SV_DEPTH SV_Depth
			#endif

			half4 frag ( VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (IN.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( IN.tSpace0.xyz );
					float3 WorldTangent = IN.tSpace1.xyz;
					float3 WorldBiTangent = IN.tSpace2.xyz;
				#endif

				float3 WorldPosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					float4 ScreenPos = IN.screenPos;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#endif

				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_TextureSample2 = IN.ase_texcoord7.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 tex2DNode717 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color719 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = IN.ase_texcoord7.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode718 = tex2D( _TextureSample9, uv_TextureSample9 );
				float4 lerpResult729 = lerp( float4( 0,0,0,0 ) , ( tex2DNode717 * _PAINT3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color720 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float4 lerpResult731 = lerp( lerpResult729 , ( tex2DNode717 * _PAINT2COLOR ) , saturate( ( 1.0 - ( ( distance( color720.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color724 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float4 lerpResult738 = lerp( lerpResult731 , ( tex2DNode717 * _PAINT1COLOR ) , saturate( ( 1.0 - ( ( distance( color724.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color734 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult741 = lerp( lerpResult738 , ( tex2DNode717 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color734.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color737 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult746 = lerp( lerpResult741 , ( tex2DNode717 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color737.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color744 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult752 = lerp( lerpResult746 , ( tex2DNode717 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color744.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color745 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult758 = lerp( lerpResult752 , ( tex2DNode717 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color745.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color753 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult761 = lerp( lerpResult758 , ( tex2DNode717 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color753.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color757 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult767 = lerp( lerpResult761 , ( tex2DNode717 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color757.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color762 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float4 lerpResult772 = lerp( lerpResult767 , ( tex2DNode717 * _WOOD3COLOR ) , saturate( ( 1.0 - ( ( distance( color762.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color769 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult779 = lerp( lerpResult772 , ( tex2DNode717 * _WOOD2COLOR ) , saturate( ( 1.0 - ( ( distance( color769.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color771 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult785 = lerp( lerpResult779 , ( tex2DNode717 * _WOOD1COLOR ) , saturate( ( 1.0 - ( ( distance( color771.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color781 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float4 lerpResult788 = lerp( lerpResult785 , ( tex2DNode717 * _LEATHER3COLOR ) , saturate( ( 1.0 - ( ( distance( color781.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color782 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float4 lerpResult794 = lerp( lerpResult788 , ( tex2DNode717 * _LEATHER2COLOR ) , saturate( ( 1.0 - ( ( distance( color782.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color789 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float4 lerpResult801 = lerp( lerpResult794 , ( tex2DNode717 * _LEATHER1COLOR ) , saturate( ( 1.0 - ( ( distance( color789.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color797 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float4 lerpResult804 = lerp( lerpResult801 , ( tex2DNode717 * _METAL4COLOR ) , saturate( ( 1.0 - ( ( distance( color797.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color799 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float4 lerpResult810 = lerp( lerpResult804 , ( tex2DNode717 * _METAL3COLOR ) , saturate( ( 1.0 - ( ( distance( color799.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color806 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float4 lerpResult814 = lerp( lerpResult810 , ( tex2DNode717 * _METAL2COLOR ) , saturate( ( 1.0 - ( ( distance( color806.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color808 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float4 lerpResult821 = lerp( lerpResult814 , ( tex2DNode717 * _METAL1COLOR ) , saturate( ( 1.0 - ( ( distance( color808.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color815 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float4 lerpResult824 = lerp( lerpResult821 , ( tex2DNode717 * _GEMS3COLOR ) , saturate( ( 1.0 - ( ( distance( color815.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color822 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult830 = lerp( lerpResult824 , ( tex2DNode717 * _GEMS2COLOR ) , saturate( ( 1.0 - ( ( distance( color822.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color825 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float4 lerpResult832 = lerp( lerpResult830 , ( tex2DNode717 * _GEMS1COLOR ) , saturate( ( 1.0 - ( ( distance( color825.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord7.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g213 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g213 = color25;
				float temp_output_11_0_g213 = distance( temp_output_1_0_g213 , temp_output_2_0_g213 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g213 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g213 , saturate( ( ( temp_output_11_0_g213 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult832 , lerpResult21_g213 , ( 1.0 - temp_output_9_0 ));
				float fresnelNdotV369 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode369 = ( 0.0 + 1.0 * pow( max( 1.0 - fresnelNdotV369 , 0.0001 ), 1.0 ) );
				float4 temp_cast_45 = (step( fresnelNode369 , _CEL1SIZE )).xxxx;
				float4 blendOpSrc689 = temp_cast_45;
				float4 blendOpDest689 = _CEL1COLOR;
				float4 temp_cast_46 = ((0.0 + (_CEL1POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc683 = ( saturate( ( blendOpSrc689 * blendOpDest689 ) ));
				float4 blendOpDest683 = temp_cast_46;
				float4 blendOpSrc661 = ( blendOpSrc683 * blendOpDest683 );
				float4 blendOpDest661 = lerpResult64;
				float fresnelNdotV365 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode365 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV365, 1.0 ) );
				float4 temp_cast_47 = (step( fresnelNode365 , _CEL2SIZE )).xxxx;
				float4 blendOpSrc696 = temp_cast_47;
				float4 blendOpDest696 = _CEL2COLOR;
				float4 temp_cast_48 = ((0.0 + (_CEL2POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc686 = ( saturate( ( blendOpSrc696 * blendOpDest696 ) ));
				float4 blendOpDest686 = temp_cast_48;
				float4 blendOpSrc662 = ( blendOpSrc686 * blendOpDest686 );
				float4 blendOpDest662 = lerpResult64;
				float fresnelNdotV368 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode368 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV368, 1.0 ) );
				float4 temp_cast_49 = (step( fresnelNode368 , _CEL3SIZE )).xxxx;
				float4 blendOpSrc698 = temp_cast_49;
				float4 blendOpDest698 = _CEL3COLOR;
				float4 temp_cast_50 = ((0.0 + (_CEL3POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc687 = ( saturate( ( blendOpSrc698 * blendOpDest698 ) ));
				float4 blendOpDest687 = temp_cast_50;
				float4 blendOpSrc663 = ( blendOpSrc687 * blendOpDest687 );
				float4 blendOpDest663 = lerpResult64;
				

				float3 BaseColor = float3(0.5, 0.5, 0.5);
				float3 Normal = float3(0, 0, 1);
				float3 Emission = ( lerpResult64 + ( ( blendOpSrc661 * blendOpDest661 ) + ( saturate( ( blendOpSrc662 * blendOpDest662 ) )) + ( saturate( ( blendOpSrc663 * blendOpDest663 ) )) ) ).rgb;
				float3 Specular = 0.5;
				float Metallic = 0;
				float Smoothness = 0.5;
				float Occlusion = 1;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = 0;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData;
				inputData.positionWS = WorldPosition;
				inputData.viewDirectionWS = WorldViewDirection;
				inputData.shadowCoord = ShadowCoords;

				#ifdef _NORMALMAP
						#if _NORMAL_DROPOFF_TS
							inputData.normalWS = TransformTangentToWorld(Normal, half3x3(WorldTangent, WorldBiTangent, WorldNormal));
						#elif _NORMAL_DROPOFF_OS
							inputData.normalWS = TransformObjectToWorldNormal(Normal);
						#elif _NORMAL_DROPOFF_WS
							inputData.normalWS = Normal;
						#endif
					inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				#else
					inputData.normalWS = WorldNormal;
				#endif

				#ifdef ASE_FOG
					inputData.fogCoord = IN.fogFactorAndVertexLight.x;
				#endif
					inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = IN.lightmapUVOrVertexSH.xyz;
				#endif

				inputData.bakedGI = SAMPLE_GI( IN.lightmapUVOrVertexSH.xy, SH, inputData.normalWS );

				#ifdef ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#endif

				inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.clipPos);
				inputData.shadowMask = SAMPLE_SHADOWMASK(IN.lightmapUVOrVertexSH.xy);

				half4 color = UniversalFragmentPBR(
					inputData,
					BaseColor,
					Metallic,
					Specular,
					Smoothness,
					Occlusion,
					Emission,
					Alpha);

				#ifdef ASE_TRANSMISSION
				{
					float shadow = _TransmissionShadow;

					Light mainLight = GetMainLight( inputData.shadowCoord );
					float3 mainAtten = mainLight.color * mainLight.distanceAttenuation;
					mainAtten = lerp( mainAtten, mainAtten * mainLight.shadowAttenuation, shadow );
					half3 mainTransmission = max(0 , -dot(inputData.normalWS, mainLight.direction)) * mainAtten * Transmission;
					color.rgb += BaseColor * mainTransmission;

					#ifdef _ADDITIONAL_LIGHTS
						int transPixelLightCount = GetAdditionalLightsCount();
						for (int i = 0; i < transPixelLightCount; ++i)
						{
							Light light = GetAdditionalLight(i, inputData.positionWS);
							float3 atten = light.color * light.distanceAttenuation;
							atten = lerp( atten, atten * light.shadowAttenuation, shadow );

							half3 transmission = max(0 , -dot(inputData.normalWS, light.direction)) * atten * Transmission;
							color.rgb += BaseColor * transmission;
						}
					#endif
				}
				#endif

				#ifdef ASE_TRANSLUCENCY
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					Light mainLight = GetMainLight( inputData.shadowCoord );
					float3 mainAtten = mainLight.color * mainLight.distanceAttenuation;
					mainAtten = lerp( mainAtten, mainAtten * mainLight.shadowAttenuation, shadow );

					half3 mainLightDir = mainLight.direction + inputData.normalWS * normal;
					half mainVdotL = pow( saturate( dot( inputData.viewDirectionWS, -mainLightDir ) ), scattering );
					half3 mainTranslucency = mainAtten * ( mainVdotL * direct + inputData.bakedGI * ambient ) * Translucency;
					color.rgb += BaseColor * mainTranslucency * strength;

					#ifdef _ADDITIONAL_LIGHTS
						int transPixelLightCount = GetAdditionalLightsCount();
						for (int i = 0; i < transPixelLightCount; ++i)
						{
							Light light = GetAdditionalLight(i, inputData.positionWS);
							float3 atten = light.color * light.distanceAttenuation;
							atten = lerp( atten, atten * light.shadowAttenuation, shadow );

							half3 lightDir = light.direction + inputData.normalWS * normal;
							half VdotL = pow( saturate( dot( inputData.viewDirectionWS, -lightDir ) ), scattering );
							half3 translucency = atten * ( VdotL * direct + inputData.bakedGI * ambient ) * Translucency;
							color.rgb += BaseColor * translucency * strength;
						}
					#endif
				}
				#endif

				#ifdef ASE_REFRACTION
					float4 projScreenPos = ScreenPos / ScreenPos.w;
					float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, float4( WorldNormal,0 ) ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
					projScreenPos.xy += refractionOffset.xy;
					float3 refraction = SHADERGRAPH_SAMPLE_SCENE_COLOR( projScreenPos.xy ) * RefractionColor;
					color.rgb = lerp( refraction, color.rgb, color.a );
					color.a = 1;
				#endif

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_FOG
					#ifdef TERRAIN_SPLAT_ADDPASS
						color.rgb = MixFogColor(color.rgb, half3( 0, 0, 0 ), IN.fogFactorAndVertexLight.x );
					#else
						color.rgb = MixFog(color.rgb, IN.fogFactorAndVertexLight.x);
					#endif
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return color;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			ZWrite On
			ZTest LEqual
			AlphaToMask Off
			ColorMask 0

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_SHADOWCASTER

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			float3 _LightDirection;
			#if ASE_SRP_VERSION >= 110000
				float3 _LightPosition;
			#endif

			VertexOutput VertexFunction( VertexInput v )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				float3 normalWS = TransformObjectToWorldDir(v.ase_normal);

				#if ASE_SRP_VERSION >= 110000
					#if _CASTING_PUNCTUAL_LIGHT_SHADOW
						float3 lightDirectionWS = normalize(_LightPosition - positionWS);
					#else
						float3 lightDirectionWS = _LightDirection;
					#endif

					float4 clipPos = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

					#if UNITY_REVERSED_Z
						clipPos.z = min(clipPos.z, UNITY_NEAR_CLIP_VALUE);
					#else
						clipPos.z = max(clipPos.z, UNITY_NEAR_CLIP_VALUE);
					#endif
				#else
					float4 clipPos = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
					#if UNITY_REVERSED_Z
						clipPos.z = min(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
					#else
						clipPos.z = max(clipPos.z, clipPos.w * UNITY_NEAR_CLIP_VALUE);
					#endif
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = clipPos;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE)
				#define ASE_SV_DEPTH SV_DepthLessEqual
			#else
				#define ASE_SV_DEPTH SV_Depth
			#endif

			half4 frag(	VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = 0;
				#endif

				#ifdef _ALPHATEST_ON
					#ifdef _ALPHATEST_SHADOW_ON
						clip(Alpha - AlphaClipThresholdShadow);
					#else
						clip(Alpha - AlphaClipThreshold);
					#endif
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;
				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE)
				#define ASE_SV_DEPTH SV_DepthLessEqual
			#else
				#define ASE_SV_DEPTH SV_Depth
			#endif

			half4 frag(	VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = 0;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Meta"
			Tags { "LightMode"="Meta" }

			Cull Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_META

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"

			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_VERT_NORMAL


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _COATOFARMSMASK;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord3.xyz = ase_worldNormal;
				
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord2.zw = v.texcoord1.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.w = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				o.clipPos = MetaVertexPosition( v.vertex, v.texcoord1.xy, v.texcoord1.xy, unity_LightmapST, unity_DynamicLightmapST );

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 uv_TextureSample2 = IN.ase_texcoord2.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 tex2DNode717 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color719 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = IN.ase_texcoord2.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode718 = tex2D( _TextureSample9, uv_TextureSample9 );
				float4 lerpResult729 = lerp( float4( 0,0,0,0 ) , ( tex2DNode717 * _PAINT3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color720 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float4 lerpResult731 = lerp( lerpResult729 , ( tex2DNode717 * _PAINT2COLOR ) , saturate( ( 1.0 - ( ( distance( color720.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color724 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float4 lerpResult738 = lerp( lerpResult731 , ( tex2DNode717 * _PAINT1COLOR ) , saturate( ( 1.0 - ( ( distance( color724.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color734 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult741 = lerp( lerpResult738 , ( tex2DNode717 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color734.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color737 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult746 = lerp( lerpResult741 , ( tex2DNode717 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color737.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color744 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult752 = lerp( lerpResult746 , ( tex2DNode717 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color744.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color745 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult758 = lerp( lerpResult752 , ( tex2DNode717 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color745.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color753 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult761 = lerp( lerpResult758 , ( tex2DNode717 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color753.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color757 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult767 = lerp( lerpResult761 , ( tex2DNode717 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color757.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color762 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float4 lerpResult772 = lerp( lerpResult767 , ( tex2DNode717 * _WOOD3COLOR ) , saturate( ( 1.0 - ( ( distance( color762.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color769 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult779 = lerp( lerpResult772 , ( tex2DNode717 * _WOOD2COLOR ) , saturate( ( 1.0 - ( ( distance( color769.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color771 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult785 = lerp( lerpResult779 , ( tex2DNode717 * _WOOD1COLOR ) , saturate( ( 1.0 - ( ( distance( color771.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color781 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float4 lerpResult788 = lerp( lerpResult785 , ( tex2DNode717 * _LEATHER3COLOR ) , saturate( ( 1.0 - ( ( distance( color781.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color782 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float4 lerpResult794 = lerp( lerpResult788 , ( tex2DNode717 * _LEATHER2COLOR ) , saturate( ( 1.0 - ( ( distance( color782.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color789 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float4 lerpResult801 = lerp( lerpResult794 , ( tex2DNode717 * _LEATHER1COLOR ) , saturate( ( 1.0 - ( ( distance( color789.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color797 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float4 lerpResult804 = lerp( lerpResult801 , ( tex2DNode717 * _METAL4COLOR ) , saturate( ( 1.0 - ( ( distance( color797.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color799 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float4 lerpResult810 = lerp( lerpResult804 , ( tex2DNode717 * _METAL3COLOR ) , saturate( ( 1.0 - ( ( distance( color799.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color806 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float4 lerpResult814 = lerp( lerpResult810 , ( tex2DNode717 * _METAL2COLOR ) , saturate( ( 1.0 - ( ( distance( color806.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color808 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float4 lerpResult821 = lerp( lerpResult814 , ( tex2DNode717 * _METAL1COLOR ) , saturate( ( 1.0 - ( ( distance( color808.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color815 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float4 lerpResult824 = lerp( lerpResult821 , ( tex2DNode717 * _GEMS3COLOR ) , saturate( ( 1.0 - ( ( distance( color815.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color822 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult830 = lerp( lerpResult824 , ( tex2DNode717 * _GEMS2COLOR ) , saturate( ( 1.0 - ( ( distance( color822.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color825 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float4 lerpResult832 = lerp( lerpResult830 , ( tex2DNode717 * _GEMS1COLOR ) , saturate( ( 1.0 - ( ( distance( color825.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord2.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g213 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g213 = color25;
				float temp_output_11_0_g213 = distance( temp_output_1_0_g213 , temp_output_2_0_g213 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g213 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g213 , saturate( ( ( temp_output_11_0_g213 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult832 , lerpResult21_g213 , ( 1.0 - temp_output_9_0 ));
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord3.xyz;
				float fresnelNdotV369 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode369 = ( 0.0 + 1.0 * pow( max( 1.0 - fresnelNdotV369 , 0.0001 ), 1.0 ) );
				float4 temp_cast_45 = (step( fresnelNode369 , _CEL1SIZE )).xxxx;
				float4 blendOpSrc689 = temp_cast_45;
				float4 blendOpDest689 = _CEL1COLOR;
				float4 temp_cast_46 = ((0.0 + (_CEL1POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc683 = ( saturate( ( blendOpSrc689 * blendOpDest689 ) ));
				float4 blendOpDest683 = temp_cast_46;
				float4 blendOpSrc661 = ( blendOpSrc683 * blendOpDest683 );
				float4 blendOpDest661 = lerpResult64;
				float fresnelNdotV365 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode365 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV365, 1.0 ) );
				float4 temp_cast_47 = (step( fresnelNode365 , _CEL2SIZE )).xxxx;
				float4 blendOpSrc696 = temp_cast_47;
				float4 blendOpDest696 = _CEL2COLOR;
				float4 temp_cast_48 = ((0.0 + (_CEL2POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc686 = ( saturate( ( blendOpSrc696 * blendOpDest696 ) ));
				float4 blendOpDest686 = temp_cast_48;
				float4 blendOpSrc662 = ( blendOpSrc686 * blendOpDest686 );
				float4 blendOpDest662 = lerpResult64;
				float fresnelNdotV368 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode368 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV368, 1.0 ) );
				float4 temp_cast_49 = (step( fresnelNode368 , _CEL3SIZE )).xxxx;
				float4 blendOpSrc698 = temp_cast_49;
				float4 blendOpDest698 = _CEL3COLOR;
				float4 temp_cast_50 = ((0.0 + (_CEL3POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc687 = ( saturate( ( blendOpSrc698 * blendOpDest698 ) ));
				float4 blendOpDest687 = temp_cast_50;
				float4 blendOpSrc663 = ( blendOpSrc687 * blendOpDest687 );
				float4 blendOpDest663 = lerpResult64;
				

				float3 BaseColor = float3(0.5, 0.5, 0.5);
				float3 Emission = ( lerpResult64 + ( ( blendOpSrc661 * blendOpDest661 ) + ( saturate( ( blendOpSrc662 * blendOpDest662 ) )) + ( saturate( ( blendOpSrc663 * blendOpDest663 ) )) ) ).rgb;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				MetaInput metaInput = (MetaInput)0;
				metaInput.Albedo = BaseColor;
				metaInput.Emission = Emission;

				return MetaFragment(metaInput);
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Universal2D"
			Tags { "LightMode"="Universal2D" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_2D

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float3 BaseColor = float3(0.5, 0.5, 0.5);
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				half4 color = half4(BaseColor, Alpha );

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				return color;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthNormals"
			Tags { "LightMode"="DepthNormals" }

			ZWrite On
			Blend One Zero
			ZTest LEqual
			ZWrite On

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_DEPTHNORMALSONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD1;
				#endif
				float3 worldNormal : TEXCOORD2;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;
				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 normalWS = TransformObjectToWorldNormal( v.ase_normal );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				o.worldNormal = normalWS;

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE)
				#define ASE_SV_DEPTH SV_DepthLessEqual
			#else
				#define ASE_SV_DEPTH SV_Depth
			#endif

			half4 frag(	VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = 0;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				#if ASE_SRP_VERSION >= 110000
					return float4(PackNormalOctRectEncode(TransformWorldToViewDir(IN.worldNormal, true)), 0.0, 0.0);
				#elif ASE_SRP_VERSION >= 100900
					return float4(PackNormalOctRectEncode(normalize(IN.worldNormal)), 0.0, 0.0);
				#else
					return float4(PackNormalOctRectEncode(TransformWorldToViewDir(IN.worldNormal, true)), 0.0, 0.0);
				#endif
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "GBuffer"
			Tags { "LightMode"="UniversalGBuffer" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _EMISSION
			#define ASE_SRP_VERSION 101001


			#pragma shader_feature_local _RECEIVE_SHADOWS_OFF
			#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF

			
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
		

			

			#pragma multi_compile_fragment _ _SHADOWS_SOFT

			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_GBUFFER

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
				#define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR
			#define ASE_NEEDS_FRAG_WORLD_NORMAL


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 lightmapUVOrVertexSH : TEXCOORD0;
				half4 fogFactorAndVertexLight : TEXCOORD1;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord : TEXCOORD2;
				#endif
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
				float4 screenPos : TEXCOORD6;
				#endif
				float4 ase_texcoord7 : TEXCOORD7;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _CEL2COLOR;
			float4 _CEL1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _GEMS1COLOR;
			float4 _GEMS2COLOR;
			float4 _GEMS3COLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _CEL3COLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER1COLOR;
			float4 _WOOD2COLOR;
			float4 _PAINT3COLOR;
			float4 _TextureSample9_ST;
			float4 _WOOD1COLOR;
			float4 _PAINT1COLOR;
			float4 _FEATHERS6COLOR;
			float4 _PAINT2COLOR;
			float4 _FEATHERS4COLOR;
			float4 _FEATHERS3COLOR;
			float4 _FEATHERS2COLOR;
			float4 _FEATHERS1COLOR;
			float4 _WOOD3COLOR;
			float4 _FEATHERS5COLOR;
			float _CEL1SIZE;
			float _CEL1POWER;
			float _CEL2SIZE;
			float _CEL2POWER;
			float _CEL3SIZE;
			float _CEL3POWER;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			// Property used by ScenePickingPass
			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			// Properties used by SceneSelectionPass
			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			sampler2D _TextureSample2;
			sampler2D _TextureSample9;
			sampler2D _COATOFARMSMASK;


			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord7.xy = v.texcoord.xy;
				o.ase_texcoord7.zw = v.texcoord1.xyzw.xy;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 positionVS = TransformWorldToView( positionWS );
				float4 positionCS = TransformWorldToHClip( positionWS );

				VertexNormalInputs normalInput = GetVertexNormalInputs( v.ase_normal, v.ase_tangent );

				o.tSpace0 = float4( normalInput.normalWS, positionWS.x);
				o.tSpace1 = float4( normalInput.tangentWS, positionWS.y);
				o.tSpace2 = float4( normalInput.bitangentWS, positionWS.z);

				OUTPUT_LIGHTMAP_UV( v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy );

				#if !defined(LIGHTMAP_ON)
					OUTPUT_SH(normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz);
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					o.lightmapUVOrVertexSH.zw = v.texcoord;
					o.lightmapUVOrVertexSH.xy = v.texcoord * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				half3 vertexLight = VertexLighting( positionWS, normalInput.normalWS );

				#ifdef ASE_FOG
					half fogFactor = ComputeFogFactor( positionCS.z );
				#else
					half fogFactor = 0;
				#endif

				o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

					o.clipPos = positionCS;

				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					o.screenPos = ComputeScreenPos(positionCS);
				#endif

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord1;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE)
				#define ASE_SV_DEPTH SV_DepthLessEqual
			#else
				#define ASE_SV_DEPTH SV_Depth
			#endif

			FragmentOutput frag ( VertexOutput IN
								#ifdef ASE_DEPTH_WRITE_ON
								,out float outputDepth : ASE_SV_DEPTH
								#endif
								 )
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (IN.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( IN.tSpace0.xyz );
					float3 WorldTangent = IN.tSpace1.xyz;
					float3 WorldBiTangent = IN.tSpace2.xyz;
				#endif

				float3 WorldPosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SCREEN_POSITION)
					float4 ScreenPos = IN.screenPos;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#else
					ShadowCoords = float4(0, 0, 0, 0);
				#endif

				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_TextureSample2 = IN.ase_texcoord7.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 tex2DNode717 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color719 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float2 uv_TextureSample9 = IN.ase_texcoord7.xy * _TextureSample9_ST.xy + _TextureSample9_ST.zw;
				float4 tex2DNode718 = tex2D( _TextureSample9, uv_TextureSample9 );
				float4 lerpResult729 = lerp( float4( 0,0,0,0 ) , ( tex2DNode717 * _PAINT3COLOR ) , saturate( ( 1.0 - ( ( distance( color719.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color720 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float4 lerpResult731 = lerp( lerpResult729 , ( tex2DNode717 * _PAINT2COLOR ) , saturate( ( 1.0 - ( ( distance( color720.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color724 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float4 lerpResult738 = lerp( lerpResult731 , ( tex2DNode717 * _PAINT1COLOR ) , saturate( ( 1.0 - ( ( distance( color724.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color734 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult741 = lerp( lerpResult738 , ( tex2DNode717 * _FEATHERS6COLOR ) , saturate( ( 1.0 - ( ( distance( color734.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color737 = IsGammaSpace() ? float4(0,0,0,1) : float4(0,0,0,1);
				float4 lerpResult746 = lerp( lerpResult741 , ( tex2DNode717 * _FEATHERS5COLOR ) , saturate( ( 1.0 - ( ( distance( color737.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color744 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult752 = lerp( lerpResult746 , ( tex2DNode717 * _FEATHERS4COLOR ) , saturate( ( 1.0 - ( ( distance( color744.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color745 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult758 = lerp( lerpResult752 , ( tex2DNode717 * _FEATHERS3COLOR ) , saturate( ( 1.0 - ( ( distance( color745.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color753 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult761 = lerp( lerpResult758 , ( tex2DNode717 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color753.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color757 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult767 = lerp( lerpResult761 , ( tex2DNode717 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color757.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color762 = IsGammaSpace() ? float4(0,0,1,0) : float4(0,0,1,0);
				float4 lerpResult772 = lerp( lerpResult767 , ( tex2DNode717 * _WOOD3COLOR ) , saturate( ( 1.0 - ( ( distance( color762.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color769 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult779 = lerp( lerpResult772 , ( tex2DNode717 * _WOOD2COLOR ) , saturate( ( 1.0 - ( ( distance( color769.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color771 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult785 = lerp( lerpResult779 , ( tex2DNode717 * _WOOD1COLOR ) , saturate( ( 1.0 - ( ( distance( color771.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color781 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float4 lerpResult788 = lerp( lerpResult785 , ( tex2DNode717 * _LEATHER3COLOR ) , saturate( ( 1.0 - ( ( distance( color781.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color782 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float4 lerpResult794 = lerp( lerpResult788 , ( tex2DNode717 * _LEATHER2COLOR ) , saturate( ( 1.0 - ( ( distance( color782.rgb , tex2DNode718.rgb ) - 0.0 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color789 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float4 lerpResult801 = lerp( lerpResult794 , ( tex2DNode717 * _LEATHER1COLOR ) , saturate( ( 1.0 - ( ( distance( color789.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color797 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float4 lerpResult804 = lerp( lerpResult801 , ( tex2DNode717 * _METAL4COLOR ) , saturate( ( 1.0 - ( ( distance( color797.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color799 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float4 lerpResult810 = lerp( lerpResult804 , ( tex2DNode717 * _METAL3COLOR ) , saturate( ( 1.0 - ( ( distance( color799.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color806 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float4 lerpResult814 = lerp( lerpResult810 , ( tex2DNode717 * _METAL2COLOR ) , saturate( ( 1.0 - ( ( distance( color806.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color808 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float4 lerpResult821 = lerp( lerpResult814 , ( tex2DNode717 * _METAL1COLOR ) , saturate( ( 1.0 - ( ( distance( color808.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color815 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float4 lerpResult824 = lerp( lerpResult821 , ( tex2DNode717 * _GEMS3COLOR ) , saturate( ( 1.0 - ( ( distance( color815.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color822 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult830 = lerp( lerpResult824 , ( tex2DNode717 * _GEMS2COLOR ) , saturate( ( 1.0 - ( ( distance( color822.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color825 = IsGammaSpace() ? float4(0.4980392,0,1,1) : float4(0.2122307,0,1,1);
				float4 lerpResult832 = lerp( lerpResult830 , ( tex2DNode717 * _GEMS1COLOR ) , saturate( ( 1.0 - ( ( distance( color825.rgb , tex2DNode718.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord7.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_44 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g213 = temp_cast_44;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g213 = color25;
				float temp_output_11_0_g213 = distance( temp_output_1_0_g213 , temp_output_2_0_g213 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g213 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g213 , saturate( ( ( temp_output_11_0_g213 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult832 , lerpResult21_g213 , ( 1.0 - temp_output_9_0 ));
				float fresnelNdotV369 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode369 = ( 0.0 + 1.0 * pow( max( 1.0 - fresnelNdotV369 , 0.0001 ), 1.0 ) );
				float4 temp_cast_45 = (step( fresnelNode369 , _CEL1SIZE )).xxxx;
				float4 blendOpSrc689 = temp_cast_45;
				float4 blendOpDest689 = _CEL1COLOR;
				float4 temp_cast_46 = ((0.0 + (_CEL1POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc683 = ( saturate( ( blendOpSrc689 * blendOpDest689 ) ));
				float4 blendOpDest683 = temp_cast_46;
				float4 blendOpSrc661 = ( blendOpSrc683 * blendOpDest683 );
				float4 blendOpDest661 = lerpResult64;
				float fresnelNdotV365 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode365 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV365, 1.0 ) );
				float4 temp_cast_47 = (step( fresnelNode365 , _CEL2SIZE )).xxxx;
				float4 blendOpSrc696 = temp_cast_47;
				float4 blendOpDest696 = _CEL2COLOR;
				float4 temp_cast_48 = ((0.0 + (_CEL2POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc686 = ( saturate( ( blendOpSrc696 * blendOpDest696 ) ));
				float4 blendOpDest686 = temp_cast_48;
				float4 blendOpSrc662 = ( blendOpSrc686 * blendOpDest686 );
				float4 blendOpDest662 = lerpResult64;
				float fresnelNdotV368 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode368 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV368, 1.0 ) );
				float4 temp_cast_49 = (step( fresnelNode368 , _CEL3SIZE )).xxxx;
				float4 blendOpSrc698 = temp_cast_49;
				float4 blendOpDest698 = _CEL3COLOR;
				float4 temp_cast_50 = ((0.0 + (_CEL3POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc687 = ( saturate( ( blendOpSrc698 * blendOpDest698 ) ));
				float4 blendOpDest687 = temp_cast_50;
				float4 blendOpSrc663 = ( blendOpSrc687 * blendOpDest687 );
				float4 blendOpDest663 = lerpResult64;
				

				float3 BaseColor = float3(0.5, 0.5, 0.5);
				float3 Normal = float3(0, 0, 1);
				float3 Emission = ( lerpResult64 + ( ( blendOpSrc661 * blendOpDest661 ) + ( saturate( ( blendOpSrc662 * blendOpDest662 ) )) + ( saturate( ( blendOpSrc663 * blendOpDest663 ) )) ) ).rgb;
				float3 Specular = 0.5;
				float Metallic = 0;
				float Smoothness = 0.5;
				float Occlusion = 1;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = 0;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData;
				inputData.positionWS = WorldPosition;
				inputData.viewDirectionWS = WorldViewDirection;
				inputData.shadowCoord = ShadowCoords;

				#ifdef _NORMALMAP
					#if _NORMAL_DROPOFF_TS
					inputData.normalWS = TransformTangentToWorld(Normal, half3x3( WorldTangent, WorldBiTangent, WorldNormal ));
					#elif _NORMAL_DROPOFF_OS
					inputData.normalWS = TransformObjectToWorldNormal(Normal);
					#elif _NORMAL_DROPOFF_WS
					inputData.normalWS = Normal;
					#endif
					inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				#else
					inputData.normalWS = WorldNormal;
				#endif

				#ifdef ASE_FOG
					inputData.fogCoord = 0; // we don't apply fog in the gbuffer pass
				#endif

				inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;
				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = IN.lightmapUVOrVertexSH.xyz;
				#endif

				inputData.bakedGI = SAMPLE_GI( IN.lightmapUVOrVertexSH.xy, SH, inputData.normalWS );

				#ifdef ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#endif

				BRDFData brdfData;
				InitializeBRDFData( BaseColor, Metallic, Specular, Smoothness, Alpha, brdfData);
				half4 color;
				color.rgb = GlobalIllumination( brdfData, inputData.bakedGI, Occlusion, inputData.normalWS, inputData.viewDirectionWS);
				color.a = Alpha;

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_FOG
					#ifdef TERRAIN_SPLAT_ADDPASS
						color.rgb = MixFogColor(color.rgb, half3( 0, 0, 0 ), IN.fogFactorAndVertexLight.x );
					#else
						color.rgb = MixFog(color.rgb, IN.fogFactorAndVertexLight.x);
					#endif
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return BRDFDataToGbuffer(brdfData, inputData, Smoothness, Emission + color.rgb);
			}

			ENDHLSL
		}
		
	}
	
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.CommentaryNode;715;-21084.23,128.5433;Inherit;False;341.4902;248.4146;Comment;1;717;BASE TEXTURE;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;713;-20390.66,128.141;Inherit;False;2305.307;694.8573;Comment;15;738;732;731;730;729;727;726;725;724;723;722;721;720;719;716;PAINT COLORS;0.05562881,0.9716981,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;714;-21078.86,529.9666;Inherit;False;336;252;Comment;1;718;MASK TEXTURE;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;718;-21048.77,579.2703;Inherit;True;Property;_TextureSample9;Texture Sample 9;34;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;f88a8d19ac8f5724aa4e309405c476b6;f88a8d19ac8f5724aa4e309405c476b6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;717;-21051.59,174.4154;Inherit;True;Property;_TextureSample2;Texture Sample 2;33;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;7772baab606a3874d99a1e30d4b6b891;7772baab606a3874d99a1e30d4b6b891;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;716;-19866.61,189.4188;Inherit;False;Property;_PAINT3COLOR;PAINT 3 COLOR;23;1;[HDR];Create;True;0;0;0;False;0;False;0.5849056,0.5418971,0.4331613,0;0,0.1132075,0.01206957,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;719;-20092.05,599.5247;Inherit;False;Constant;_Color28;Color 28;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;721;-19642.4,178.141;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;720;-19492.61,597.0018;Inherit;False;Constant;_Color29;Color 29;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;722;-19836.67,583.8914;Inherit;True;Color Mask;-1;;177;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;723;-19259.45,201.534;Inherit;False;Property;_PAINT2COLOR;PAINT 2 COLOR;22;1;[HDR];Create;True;0;0;0;False;0;False;0.3649431,0.5566038,0.4386422,0;0.2023368,0,0.4339623,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;726;-18686.87,200.3568;Inherit;False;Property;_PAINT1COLOR;PAINT 1 COLOR;21;1;[HDR];Create;True;0;0;0;False;0;False;0.5450981,0.6936808,0.6980392,0;0.3773585,0,0.06650025,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;729;-19466.58,398.5252;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;725;-19029.53,180.5199;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;724;-18826.06,593.2327;Inherit;False;Constant;_Color30;Color 30;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;727;-19238.46,579.1321;Inherit;True;Color Mask;-1;;179;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;728;-17686.24,137.2083;Inherit;False;4637.659;753.5428;Comment;30;767;764;763;761;760;759;758;757;755;754;753;752;751;750;749;748;747;746;745;744;743;742;741;740;739;737;736;735;734;733;FEATHERS COLORS;0.735849,0.7152051,0.3158597,1;0;0
Node;AmplifyShaderEditor.FunctionNode;730;-18571.74,588.2972;Inherit;True;Color Mask;-1;;180;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;731;-18846.12,383.993;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;733;-17181.62,197.1996;Inherit;False;Property;_FEATHERS6COLOR;FEATHERS 6 COLOR;32;1;[HDR];Create;True;0;0;0;False;0;False;0.7924528,0.7444169,0.6391954,0;0,0.1793142,0.7264151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;734;-17361.7,638.9216;Inherit;False;Constant;_Color3;Color 3;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;732;-18452.73,185.5819;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;736;-17110.21,563.6277;Inherit;True;Color Mask;-1;;195;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;735;-16941.62,181.1996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;738;-18269.34,393.3084;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;739;-16589.62,197.1996;Inherit;False;Property;_FEATHERS5COLOR;FEATHERS 5 COLOR;31;1;[HDR];Create;True;0;0;0;False;0;False;0.4150943,0.2615769,0.1468494,0;0.6792453,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;737;-16761.11,634.5681;Inherit;False;Constant;_Color15;Color 15;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;741;-16765.62,405.1996;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;743;-16045.62,200.0697;Inherit;False;Property;_FEATHERS4COLOR;FEATHERS 4 COLOR;30;1;[HDR];Create;True;0;0;0;False;0;False;0.8113208,0.2104842,0.2104842,0;0.7735849,0.492613,0.492613,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;740;-16516.56,563.9022;Inherit;True;Color Mask;-1;;196;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;744;-16141.55,635.4514;Inherit;False;Constant;_Color22;Color 22;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;742;-16333.62,181.1996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;745;-15378.99,631.0828;Inherit;False;Constant;_Color38;Color 38;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;747;-15757.62,181.1996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;746;-16141.62,389.1996;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;749;-15285.23,189.7469;Inherit;False;Property;_FEATHERS3COLOR;FEATHERS 3 COLOR;29;1;[HDR];Create;True;0;0;0;False;0;False;0.509434,0.25712,0.25712,0;0,0.1793142,0.7264151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;748;-15867.92,585.827;Inherit;True;Color Mask;-1;;197;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;752;-15574.35,387.4535;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;753;-14780.25,640.8616;Inherit;False;Constant;_Color39;Color 39;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;754;-14693.24,189.7469;Inherit;False;Property;_FEATHERS2COLOR;FEATHERS 2 COLOR;28;1;[HDR];Create;True;0;0;0;False;0;False;0.509434,0.4260285,0.1802243,0;0.6792453,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;751;-15045.23,173.7469;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;750;-15112.63,561.9422;Inherit;True;Color Mask;-1;;198;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;760;-14437.23,173.7469;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;758;-14870.98,389.016;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;756;-12815.87,123.182;Inherit;False;2359.399;695.7338;Comment;15;785;780;779;777;776;774;773;772;771;770;769;768;766;765;762;WOOD COLORS;0.4690726,0.7830189,0.47128,1;0;0
Node;AmplifyShaderEditor.ColorNode;759;-14174.23,198.7469;Inherit;False;Property;_FEATHERS1COLOR;FEATHERS 1 COLOR;27;1;[HDR];Create;True;0;0;0;False;0;False;0.3301887,0.1241556,0.04516733,0;0.7735849,0.492613,0.492613,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;755;-14535.3,568.5594;Inherit;True;Color Mask;-1;;199;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;757;-14208.83,616.2229;Inherit;False;Constant;_Color40;Color 40;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;765;-12304.31,190.7249;Inherit;False;Property;_WOOD3COLOR;WOOD 3 COLOR;17;1;[HDR];Create;True;0;0;0;False;0;False;0.1037736,0.07509367,0.04650232,0;0.9056604,0.6815338,0.4229263,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;762;-12503.72,622.7976;Inherit;False;Constant;_Color21;Color 21;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;764;-13861.23,173.7469;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;763;-13943.64,562.6443;Inherit;True;Color Mask;-1;;200;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;761;-14243.49,390.4784;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;767;-13670.97,371.2699;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;766;-11676.17,188.4364;Inherit;False;Property;_WOOD2COLOR;WOOD 2 COLOR;16;1;[HDR];Create;True;0;0;0;False;0;False;0.1320755,0.06452555,0.05420079,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;768;-12013.51,173.182;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;769;-11840.96,591.8792;Inherit;False;Constant;_Color9;Color 9;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;770;-12203.74,559.1131;Inherit;True;Color Mask;-1;;201;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;775;-10308.57,103.0511;Inherit;False;2342.301;700.673;Comment;15;801;795;794;793;792;791;790;789;788;786;784;783;782;781;778;LEATHER COLORS;0.7735849,0.5371538,0.1788003,1;0;0
Node;AmplifyShaderEditor.FunctionNode;776;-11570.05,562.9573;Inherit;True;Color Mask;-1;;202;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;772;-11837.69,393.5663;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;771;-11242.23,610.1169;Inherit;False;Constant;_Color10;Color 10;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,1,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;774;-11400.65,175.5609;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;773;-11095.51,197.5897;Inherit;False;Property;_WOOD1COLOR;WOOD 1 COLOR;15;1;[HDR];Create;True;0;0;0;False;0;False;0.1981132,0.08345769,0.06261124,0;0,0.1142961,0.1698113,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;778;-9795.144,180.4691;Inherit;False;Property;_LEATHER3COLOR;LEATHER 3 COLOR;20;1;[HDR];Create;True;0;0;0;False;0;False;0.1320755,0.03139969,0.02180491,1;0.1698113,0.04637412,0.02963688,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;779;-11217.24,379.0345;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;781;-9876.713,637.7366;Inherit;False;Constant;_Color23;Color 23;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0.4980392,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;780;-10823.83,180.6229;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;777;-10972.32,563.1951;Inherit;True;Color Mask;-1;;203;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;783;-9203.768,186.2713;Inherit;False;Property;_LEATHER2COLOR;LEATHER 2 COLOR;19;1;[HDR];Create;True;0;0;0;False;0;False;0.06603771,0.03523636,0.03146137,1;0.4245283,0.190437,0.09011215,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;782;-9266.412,619.0491;Inherit;False;Constant;_Color31;Color 31;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;784;-9528.563,158.3075;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;785;-10640.47,388.3495;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;786;-9604.023,559.1605;Inherit;True;Color Mask;-1;;204;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;790;-9004.503,555.2908;Inherit;True;Color Mask;-1;;205;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;787;-7754.735,102.2855;Inherit;False;3503.256;733.6797;Comment;20;821;818;816;814;813;812;811;810;808;807;806;805;804;803;802;800;799;798;797;796;METAL COLORS;0.259434,0.8569208,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;792;-8910.451,155.43;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;789;-8617.618,617.2185;Inherit;False;Constant;_Color41;Color 41;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;788;-9347.497,373.4349;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;791;-8613.768,179.2713;Inherit;False;Property;_LEATHER1COLOR;LEATHER 1 COLOR;18;1;[HDR];Create;True;0;0;0;False;0;False;0.2924528,0.1296404,0.09242612,1;0.4811321,0.2041155,0.08851016,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;796;-7186.3,194.3319;Inherit;False;Property;_METAL4COLOR;METAL 4 COLOR;13;1;[HDR];Create;True;0;0;0;False;0;False;0.8791043,0.8044721,0.9422547,0;0.9528302,0.9528302,0.9528302,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;793;-8333.638,160.492;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;794;-8727.046,371.1292;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;797;-7332.446,653.6995;Inherit;False;Constant;_Color42;Color 42;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0.4980392,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;795;-8371.881,557.5023;Inherit;True;Color Mask;-1;;206;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;798;-6878.193,170.3597;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;799;-6661.072,644.3533;Inherit;False;Constant;_Color43;Color 43;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;801;-8150.274,368.2181;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;800;-6541.527,181.1996;Inherit;False;Property;_METAL3COLOR;METAL 3 COLOR;9;1;[HDR];Create;True;0;0;0;False;0;False;0.597023,0.6237553,0.7395956,0;0.9528302,0.9528302,0.9528302,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;802;-7107.291,556.6673;Inherit;True;Color Mask;-1;;207;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;24;-1812.858,-1400.5;Inherit;False;1262.249;589.4722;;10;16;18;26;25;17;9;10;290;293;294;COAT OF ARMS;1,0,0.7651567,1;0;0
Node;AmplifyShaderEditor.FunctionNode;807;-6426.507,554.9715;Inherit;True;Color Mask;-1;;208;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;804;-6710.174,367.368;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;803;-5867.646,182.1424;Inherit;False;Property;_METAL2COLOR;METAL 2 COLOR;5;1;[HDR];Create;True;0;0;0;False;0;False;1.678431,1.003922,0.1176471,0;0.3301887,0.3301887,0.3301887,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;805;-6221.528,165.1996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;806;-6024.123,641.906;Inherit;False;Constant;_Color44;Color 44;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0,0,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;810;-6045.527,373.1996;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;808;-5324.733,652.864;Inherit;False;Constant;_Color37;Color 37;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;813;-5758.907,551.0652;Inherit;True;Color Mask;-1;;209;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;812;-5261.947,181.0785;Inherit;False;Property;_METAL1COLOR;METAL 1 COLOR;1;1;[HDR];Create;True;0;0;0;False;0;False;0.7261481,0.7735849,0.7528313,0;2,0.682353,0.1960784,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;811;-5613.526,165.1996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;10;-1769.44,-1325.746;Inherit;True;Property;_COATOFARMSMASK;COAT OF ARMS MASK;2;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;None;True;1;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;809;-3266.14,61.07749;Inherit;False;2305.296;707.5152;Comment;15;832;831;830;829;828;827;826;825;824;823;822;820;819;817;815;GEMS COLORS;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;9;-1435.856,-1221.265;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;816;-5124.448,547.888;Inherit;True;Color Mask;-1;;210;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;815;-3163.023,558.6448;Inherit;False;Constant;_Color46;Color 46;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;814;-5425.291,372.2596;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;817;-3048.401,177.1615;Inherit;False;Property;_GEMS3COLOR;GEMS 3 COLOR;26;1;[HDR];Create;True;0;0;0;False;0;False;0,0.4716981,0.1359325,0;0.8490566,0.5037117,0.3884835,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;818;-4989.092,165.0492;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;820;-2680.687,160.4696;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;822;-2419.836,574.9734;Inherit;False;Constant;_Color45;Color 45;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,1,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;16;-1249.699,-965.4095;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;26;-1063.837,-1185.19;Inherit;False;Property;_COATOFARMSCOLOR;COAT OF ARMS COLOR;0;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;25;-1067.646,-1364.901;Inherit;False;Constant;_Color4;Color 4;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;821;-4805.727,372.7757;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;18;-1043.557,-1002.802;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;1.6,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;823;-2862.413,544.0218;Inherit;True;Color Mask;-1;;211;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;819;-2344.428,180.5321;Inherit;False;Property;_GEMS2COLOR;GEMS 2 COLOR;25;1;[HDR];Create;True;0;0;0;False;0;False;0,0.3218706,0.5754717,0;0.8301887,0.3185886,0.2780349,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;825;-1672.001,567.4695;Inherit;False;Constant;_Color47;Color 47;51;1;[HideInInspector];Create;True;0;0;0;False;0;False;0.4980392,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;827;-2000.743,160.3109;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;360;-2384.559,956.9141;Inherit;False;2541.61;1272.682;Comment;32;379;662;663;661;686;559;560;390;683;687;688;684;685;374;371;370;369;367;368;365;364;366;362;361;363;689;690;695;697;699;700;701;TOON;0.990566,0.1822268,0.8019541,1;0;0
Node;AmplifyShaderEditor.LerpOp;824;-2583.34,345.9955;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;828;-2143.468,551.3631;Inherit;True;Color Mask;-1;;212;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;294;-644.1989,-930.3562;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;826;-1621.909,187.0326;Inherit;False;Property;_GEMS1COLOR;GEMS 1 COLOR;24;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;0.9056604,0.8159487,0.8159487,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;17;-814.7589,-1227.691;Inherit;False;Replace Color;-1;;213;896dccb3016c847439def376a728b869;1,12,0;5;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;293;-633.4137,-898.618;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;363;-2264.815,1559.836;Inherit;False;Constant;_Float3;Float 3;31;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;290;-633.3123,-1147.438;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;361;-2262.561,1359.56;Inherit;False;Constant;_1;1;32;0;Create;True;0;0;0;False;0;False;1;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;362;-2290.99,1147.003;Inherit;False;Constant;_2;2;35;0;Create;True;0;0;0;False;0;False;1;0.3991557;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;830;-1850.834,352.2831;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;829;-1292.387,163.2166;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;831;-1407.66,544.9827;Inherit;True;Color Mask;-1;;214;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;369;-1956.678,1036.234;Inherit;False;Standard;WorldNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;368;-1997.462,1519.467;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;367;-1763.215,1206.693;Inherit;False;Property;_CEL1SIZE;CEL 1 SIZE;3;0;Create;True;0;0;0;False;0;False;0.1;0.45;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;365;-1958.192,1272.703;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;292;-620.0846,401.4227;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;366;-1764.589,1647.723;Inherit;False;Property;_CEL3SIZE;CEL 3 SIZE;6;0;Create;True;0;0;0;False;0;False;0.8;0.706;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;364;-1744.519,1402.56;Inherit;False;Property;_CEL2SIZE;CEL 2 SIZE;4;0;Create;True;0;0;0;False;0;False;0.4;0.243;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;832;-1151.245,335.702;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;291;-617.0846,363.4227;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;371;-1481.249,1063.553;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;697;-1341.959,1671.755;Inherit;False;Property;_CEL3COLOR;CEL 3 COLOR;14;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;684;-1152.591,1217.51;Inherit;False;Property;_CEL1POWER;CEL 1 POWER;7;0;Create;True;0;0;0;False;0;False;0.15;0.22;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;695;-1338.313,1422.022;Inherit;False;Property;_CEL2COLOR;CEL 2 COLOR;12;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;690;-1382.336,1176.092;Inherit;False;Property;_CEL1COLOR;CEL 1 COLOR;11;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;64;-583.6503,335.5556;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;688;-1102.397,1725.987;Inherit;False;Property;_CEL3POWER;CEL 3 POWER;10;0;Create;True;0;0;0;False;0;False;0.15;0.247;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;370;-1455.669,1593.09;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;374;-1422.798,1338.325;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;685;-1116.953,1474.047;Inherit;False;Property;_CEL2POWER;CEL 2 POWER;8;0;Create;True;0;0;0;False;0;False;0.15;0.206;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;696;-1115.047,1332.02;Inherit;False;Multiply;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;396;-404.1466,380.3712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;394;-405.1466,380.3712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;395;-405.1466,381.3712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;700;-848.9835,1463.261;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;689;-1114.33,1075.988;Inherit;False;Multiply;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;698;-1118.693,1581.753;Inherit;False;Multiply;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;699;-868.8555,1208.175;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;701;-822.0625,1730.397;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;559;-412.1298,1549.633;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;683;-760.7569,1072.747;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;390;-409.3758,1069.185;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;560;-404.1298,1339.633;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;687;-694.37,1584.548;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;686;-704.9259,1334.608;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;661;-351.4232,1095.355;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;663;-359.8096,1571.366;Inherit;False;Multiply;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;662;-345.4095,1304.167;Inherit;False;Multiply;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;379;-48.5603,1282.605;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;382;501.8256,334.2374;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;845;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;846;756.0967,327.2389;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;Polytope Studio/ PT_Medieval Weapons Shader Toon;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;18;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;Hidden/InternalErrorShader;0;0;Standard;38;Workflow;1;0;Surface;0;0;  Refraction Model;0;0;  Blend;0;0;Two Sided;1;0;Fragment Normal Space,InvertActionOnDeselection;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;  Use Shadow Threshold;0;0;Receive Shadows;1;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;_FinalColorxAlpha;0;0;Meta Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;DOTS Instancing;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Write Depth;0;0;  Early Z;0;0;Vertex Position,InvertActionOnDeselection;1;0;0;8;False;True;True;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;847;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;848;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;849;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;850;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;851;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthNormals;0;6;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormals;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;852;756.0967,327.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;GBuffer;0;7;GBuffer;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalGBuffer;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;721;0;717;0
WireConnection;721;1;716;0
WireConnection;722;1;718;0
WireConnection;722;3;719;0
WireConnection;729;1;721;0
WireConnection;729;2;722;0
WireConnection;725;0;717;0
WireConnection;725;1;723;0
WireConnection;727;1;718;0
WireConnection;727;3;720;0
WireConnection;730;1;718;0
WireConnection;730;3;724;0
WireConnection;731;0;729;0
WireConnection;731;1;725;0
WireConnection;731;2;727;0
WireConnection;732;0;717;0
WireConnection;732;1;726;0
WireConnection;736;1;718;0
WireConnection;736;3;734;0
WireConnection;735;0;717;0
WireConnection;735;1;733;0
WireConnection;738;0;731;0
WireConnection;738;1;732;0
WireConnection;738;2;730;0
WireConnection;741;0;738;0
WireConnection;741;1;735;0
WireConnection;741;2;736;0
WireConnection;740;1;718;0
WireConnection;740;3;737;0
WireConnection;742;0;717;0
WireConnection;742;1;739;0
WireConnection;747;0;717;0
WireConnection;747;1;743;0
WireConnection;746;0;741;0
WireConnection;746;1;742;0
WireConnection;746;2;740;0
WireConnection;748;1;718;0
WireConnection;748;3;744;0
WireConnection;752;0;746;0
WireConnection;752;1;747;0
WireConnection;752;2;748;0
WireConnection;751;0;717;0
WireConnection;751;1;749;0
WireConnection;750;1;718;0
WireConnection;750;3;745;0
WireConnection;760;0;717;0
WireConnection;760;1;754;0
WireConnection;758;0;752;0
WireConnection;758;1;751;0
WireConnection;758;2;750;0
WireConnection;755;1;718;0
WireConnection;755;3;753;0
WireConnection;764;0;717;0
WireConnection;764;1;759;0
WireConnection;763;1;718;0
WireConnection;763;3;757;0
WireConnection;761;0;758;0
WireConnection;761;1;760;0
WireConnection;761;2;755;0
WireConnection;767;0;761;0
WireConnection;767;1;764;0
WireConnection;767;2;763;0
WireConnection;768;0;717;0
WireConnection;768;1;765;0
WireConnection;770;1;718;0
WireConnection;770;3;762;0
WireConnection;776;1;718;0
WireConnection;776;3;769;0
WireConnection;772;0;767;0
WireConnection;772;1;768;0
WireConnection;772;2;770;0
WireConnection;774;0;717;0
WireConnection;774;1;766;0
WireConnection;779;0;772;0
WireConnection;779;1;774;0
WireConnection;779;2;776;0
WireConnection;780;0;717;0
WireConnection;780;1;773;0
WireConnection;777;1;718;0
WireConnection;777;3;771;0
WireConnection;784;0;717;0
WireConnection;784;1;778;0
WireConnection;785;0;779;0
WireConnection;785;1;780;0
WireConnection;785;2;777;0
WireConnection;786;1;718;0
WireConnection;786;3;781;0
WireConnection;790;1;718;0
WireConnection;790;3;782;0
WireConnection;792;0;717;0
WireConnection;792;1;783;0
WireConnection;788;0;785;0
WireConnection;788;1;784;0
WireConnection;788;2;786;0
WireConnection;793;0;717;0
WireConnection;793;1;791;0
WireConnection;794;0;788;0
WireConnection;794;1;792;0
WireConnection;794;2;790;0
WireConnection;795;1;718;0
WireConnection;795;3;789;0
WireConnection;798;0;717;0
WireConnection;798;1;796;0
WireConnection;801;0;794;0
WireConnection;801;1;793;0
WireConnection;801;2;795;0
WireConnection;802;1;718;0
WireConnection;802;3;797;0
WireConnection;807;1;718;0
WireConnection;807;3;799;0
WireConnection;804;0;801;0
WireConnection;804;1;798;0
WireConnection;804;2;802;0
WireConnection;805;0;717;0
WireConnection;805;1;800;0
WireConnection;810;0;804;0
WireConnection;810;1;805;0
WireConnection;810;2;807;0
WireConnection;813;1;718;0
WireConnection;813;3;806;0
WireConnection;811;0;717;0
WireConnection;811;1;803;0
WireConnection;9;0;10;4
WireConnection;816;1;718;0
WireConnection;816;3;808;0
WireConnection;814;0;810;0
WireConnection;814;1;811;0
WireConnection;814;2;813;0
WireConnection;818;0;717;0
WireConnection;818;1;812;0
WireConnection;820;0;717;0
WireConnection;820;1;817;0
WireConnection;16;0;9;0
WireConnection;821;0;814;0
WireConnection;821;1;818;0
WireConnection;821;2;816;0
WireConnection;823;1;718;0
WireConnection;823;3;815;0
WireConnection;827;0;717;0
WireConnection;827;1;819;0
WireConnection;824;0;821;0
WireConnection;824;1;820;0
WireConnection;824;2;823;0
WireConnection;828;1;718;0
WireConnection;828;3;822;0
WireConnection;294;0;16;0
WireConnection;17;1;9;0
WireConnection;17;2;25;0
WireConnection;17;3;26;0
WireConnection;17;4;18;1
WireConnection;17;5;18;2
WireConnection;293;0;294;0
WireConnection;290;0;17;0
WireConnection;830;0;824;0
WireConnection;830;1;827;0
WireConnection;830;2;828;0
WireConnection;829;0;717;0
WireConnection;829;1;826;0
WireConnection;831;1;718;0
WireConnection;831;3;825;0
WireConnection;369;3;362;0
WireConnection;368;3;363;0
WireConnection;365;3;361;0
WireConnection;292;0;293;0
WireConnection;832;0;830;0
WireConnection;832;1;829;0
WireConnection;832;2;831;0
WireConnection;291;0;290;0
WireConnection;371;0;369;0
WireConnection;371;1;367;0
WireConnection;64;0;832;0
WireConnection;64;1;291;0
WireConnection;64;2;292;0
WireConnection;370;0;368;0
WireConnection;370;1;366;0
WireConnection;374;0;365;0
WireConnection;374;1;364;0
WireConnection;696;0;374;0
WireConnection;696;1;695;0
WireConnection;396;0;64;0
WireConnection;394;0;64;0
WireConnection;395;0;64;0
WireConnection;700;0;685;0
WireConnection;689;0;371;0
WireConnection;689;1;690;0
WireConnection;698;0;370;0
WireConnection;698;1;697;0
WireConnection;699;0;684;0
WireConnection;701;0;688;0
WireConnection;559;0;394;0
WireConnection;683;0;689;0
WireConnection;683;1;699;0
WireConnection;390;0;396;0
WireConnection;560;0;395;0
WireConnection;687;0;698;0
WireConnection;687;1;701;0
WireConnection;686;0;696;0
WireConnection;686;1;700;0
WireConnection;661;0;683;0
WireConnection;661;1;390;0
WireConnection;663;0;687;0
WireConnection;663;1;559;0
WireConnection;662;0;686;0
WireConnection;662;1;560;0
WireConnection;379;0;661;0
WireConnection;379;1;662;0
WireConnection;379;2;663;0
WireConnection;382;0;64;0
WireConnection;382;1;379;0
WireConnection;846;2;382;0
ASEEND*/
//CHKSM=5105F3921E2DD1167FB536AD9E21B9C794DD53E1