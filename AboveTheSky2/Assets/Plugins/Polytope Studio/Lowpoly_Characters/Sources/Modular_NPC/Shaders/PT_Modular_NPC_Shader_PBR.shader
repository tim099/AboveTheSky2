// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Polytope Studio/ PT_Medieval Modular NPC Shader PBR"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][HDR]_SKINCOLOR("SKIN COLOR", Color) = (2.02193,1.0081,0.6199315,0)
		_SKINSMOOTHNESS("SKIN SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_EYESCOLOR("EYES COLOR", Color) = (0.0734529,0.1320755,0.05046281,1)
		_EYESSMOOTHNESS("EYES SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_HAIRCOLOR("HAIR COLOR", Color) = (1,0,0,0)
		_HAIRSMOOTHNESS("HAIR SMOOTHNESS", Range( 0 , 1)) = 0.1
		[HDR]_SCLERACOLOR("SCLERA COLOR", Color) = (0.9056604,0.8159487,0.8159487,0)
		_SCLERASMOOTHNESS("SCLERA SMOOTHNESS", Range( 0 , 1)) = 0.5
		[HDR]_LIPSCOLOR("LIPS COLOR", Color) = (0.8301887,0.3185886,0.2780349,0)
		_LIPSSMOOTHNESS("LIPS SMOOTHNESS", Range( 0 , 1)) = 0.4
		[HDR]_OTHERCOLOR("OTHER COLOR", Color) = (0.5188679,0.4637216,0.3206212,0)
		_OTHERSMOOTHNESS("OTHER SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_METAL1COLOR("METAL 1 COLOR", Color) = (0.8792791,0.9922886,1.007606,0)
		_METAL1METALLIC("METAL 1 METALLIC", Range( 0 , 1)) = 0.65
		_METAL1SMOOTHNESS("METAL 1 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_METAL2COLOR("METAL 2 COLOR", Color) = (0.4674706,0.4677705,0.5188679,0)
		_METAL2METALLIC("METAL 2 METALLIC", Range( 0 , 1)) = 0.65
		_METAL2SMOOTHNESS("METAL 2 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_METAL3COLOR("METAL 3 COLOR", Color) = (0.4383232,0.4383232,0.4716981,0)
		_METAL3METALLIC("METAL 3 METALLIC", Range( 0 , 1)) = 0.65
		_METAL3SMOOTHNESS("METAL 3 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_METAL4COLOR("METAL 4 COLOR", Color) = (0.4383232,0.4383232,0.4716981,0)
		_METAL4METALLIC("METAL 4 METALLIC", Range( 0 , 1)) = 0.65
		_METAL4SMOOTHNESS("METAL 4 SMOOTHNESS", Range( 0 , 1)) = 0.7
		[HDR]_LEATHER1COLOR("LEATHER 1 COLOR", Color) = (0.4811321,0.2041155,0.08851016,1)
		_LEATHER1SMOOTHNESS("LEATHER 1 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_LEATHER2COLOR("LEATHER 2 COLOR", Color) = (0.4245283,0.190437,0.09011215,1)
		_LEATHER2SMOOTHNESS("LEATHER 2 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_LEATHER3COLOR("LEATHER 3 COLOR", Color) = (0.1698113,0.04637412,0.02963688,1)
		_LEATHER3SMOOTHNESS("LEATHER 3 SMOOTHNESS", Range( 0 , 1)) = 0.3
		[HDR]_LEATHER4COLOR("LEATHER 4 COLOR", Color) = (0.1698113,0.04637412,0.02963688,1)
		_LEATHER4SMOOTHNES("LEATHER 4 SMOOTHNES", Range( 0 , 1)) = 0.3
		[HDR]_CLOTH1COLOR("CLOTH 1 COLOR", Color) = (0,0.1792453,0.05062231,0)
		[HDR]_CLOTH2COLOR("CLOTH 2 COLOR", Color) = (1,0,0,0)
		[HDR]_CLOTH3COLOR("CLOTH 3 COLOR", Color) = (0.3962264,0.3391397,0.2710039,0)
		[HDR]_CLOTH4COLOR("CLOTH 4 COLOR", Color) = (0.2011392,0.3773585,0.3739074,0)
		[HDR]_FEATHERS1COLOR("FEATHERS 1 COLOR", Color) = (0.7735849,0.492613,0.492613,0)
		[HDR]_FEATHERS2COLOR("FEATHERS 2 COLOR", Color) = (0.6792453,0,0,0)
		[HDR]_COATOFARMSCOLOR("COAT OF ARMS COLOR", Color) = (1,0,0,0)
		[NoScaleOffset]_COATOFARMSMASK("COAT OF ARMS MASK", 2D) = "black" {}
		_OCCLUSION("OCCLUSION", Range( 0 , 1)) = 0.5
		[Toggle]_MetalicOn("Metalic On", Float) = 1
		[ASEEnd][Toggle]_SmoothnessOn("Smoothness On", Float) = 1
		[HideInInspector]_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector]_TextureSample2("Texture Sample 2", 2D) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}


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
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _TextureSample0;
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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color638 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample0 = IN.ase_texcoord7.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode617 = tex2D( _TextureSample0, uv_TextureSample0 );
				float4 lerpResult189 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color638.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color637 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color637.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color636 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult598 = lerp( lerpResult184 , ( tex2DNode199 * _CLOTH4COLOR ) , saturate( ( 1.0 - ( ( distance( color636.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color635 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult171 = lerp( lerpResult598 , ( tex2DNode199 * _CLOTH3COLOR ) , saturate( ( 1.0 - ( ( distance( color635.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color634 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _CLOTH2COLOR ) , saturate( ( 1.0 - ( ( distance( color634.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color633 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _CLOTH1COLOR ) , saturate( ( 1.0 - ( ( distance( color633.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color632 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float temp_output_599_0 = saturate( ( 1.0 - ( ( distance( color632.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult602 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER4COLOR ) , temp_output_599_0);
				float4 color631 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_165_0 = saturate( ( 1.0 - ( ( distance( color631.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult602 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_165_0);
				float4 color630 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_158_0 = saturate( ( 1.0 - ( ( distance( color630.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_158_0);
				float4 color629 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_157_0 = saturate( ( 1.0 - ( ( distance( color629.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_157_0);
				float4 color628 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_603_0 = saturate( ( 1.0 - ( ( distance( color628.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult606 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_603_0);
				float4 color627 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_117_0 = saturate( ( 1.0 - ( ( distance( color627.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult606 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_117_0);
				float4 color625 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_127_0 = saturate( ( 1.0 - ( ( distance( color625.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_127_0);
				float4 color624 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_123_0 = saturate( ( 1.0 - ( ( distance( color624.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_123_0);
				float4 color623 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float temp_output_145_0 = saturate( ( 1.0 - ( ( distance( color623.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _OTHERCOLOR ) , temp_output_145_0);
				float4 color622 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color622.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _LIPSCOLOR ) , temp_output_149_0);
				float4 color621 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_150_0 = saturate( ( 1.0 - ( ( distance( color621.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _SCLERACOLOR ) , temp_output_150_0);
				float4 color618 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float temp_output_71_0 = saturate( ( 1.0 - ( ( distance( color618.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult73 = lerp( lerpResult153 , ( tex2DNode199 * _EYESCOLOR ) , temp_output_71_0);
				float4 color619 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float temp_output_67_0 = saturate( ( 1.0 - ( ( distance( color619.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult69 = lerp( lerpResult73 , ( tex2DNode199 * _HAIRCOLOR ) , temp_output_67_0);
				float4 color620 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float temp_output_63_0 = saturate( ( 1.0 - ( ( distance( color620.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult62 = lerp( lerpResult69 , ( tex2DNode199 * _SKINCOLOR ) , temp_output_63_0);
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord7.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult62 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				
				float lerpResult610 = lerp( 0.0 , _METAL4METALLIC , temp_output_603_0);
				float lerpResult315 = lerp( lerpResult610 , _METAL3METALLIC , temp_output_117_0);
				float lerpResult319 = lerp( lerpResult315 , _METAL2METALLIC , temp_output_127_0);
				float lerpResult316 = lerp( lerpResult319 , _METAL1METALLIC , temp_output_123_0);
				
				float lerpResult612 = lerp( 0.0 , _LEATHER4SMOOTHNES , temp_output_599_0);
				float lerpResult336 = lerp( lerpResult612 , _LEATHER3SMOOTHNESS , temp_output_165_0);
				float lerpResult332 = lerp( lerpResult336 , _LEATHER2SMOOTHNESS , temp_output_158_0);
				float lerpResult334 = lerp( lerpResult332 , _LEATHER1SMOOTHNESS , temp_output_157_0);
				float lerpResult608 = lerp( lerpResult334 , _METAL4SMOOTHNESS , temp_output_603_0);
				float lerpResult327 = lerp( lerpResult608 , _METAL3SMOOTHNESS , temp_output_117_0);
				float lerpResult331 = lerp( lerpResult327 , _METAL2SMOOTHNESS , temp_output_127_0);
				float lerpResult328 = lerp( lerpResult331 , _METAL1SMOOTHNESS , temp_output_123_0);
				float lerpResult321 = lerp( lerpResult328 , _OTHERSMOOTHNESS , temp_output_145_0);
				float lerpResult325 = lerp( lerpResult321 , _LIPSSMOOTHNESS , temp_output_149_0);
				float lerpResult322 = lerp( lerpResult325 , _SCLERASMOOTHNESS , temp_output_150_0);
				float lerpResult306 = lerp( lerpResult322 , _EYESSMOOTHNESS , temp_output_71_0);
				float lerpResult304 = lerp( lerpResult306 , _HAIRSMOOTHNESS , temp_output_67_0);
				float lerpResult302 = lerp( lerpResult304 , _SKINSMOOTHNESS , temp_output_63_0);
				

				float3 BaseColor = lerpResult64.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = (( _MetalicOn )?( lerpResult316 ):( 0.0 ));
				float Smoothness = (( _SmoothnessOn )?( lerpResult302 ):( 0.0 ));
				float Occlusion = (1.0 + (_OCCLUSION - 0.0) * (0.5 - 1.0) / (1.0 - 0.0));
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
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			#define ASE_SRP_VERSION 101001


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_META

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"

			

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
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _TextureSample0;
			sampler2D _COATOFARMSMASK;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord2.zw = v.texcoord1.xy;

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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color638 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample0 = IN.ase_texcoord2.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode617 = tex2D( _TextureSample0, uv_TextureSample0 );
				float4 lerpResult189 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color638.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color637 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color637.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color636 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult598 = lerp( lerpResult184 , ( tex2DNode199 * _CLOTH4COLOR ) , saturate( ( 1.0 - ( ( distance( color636.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color635 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult171 = lerp( lerpResult598 , ( tex2DNode199 * _CLOTH3COLOR ) , saturate( ( 1.0 - ( ( distance( color635.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color634 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _CLOTH2COLOR ) , saturate( ( 1.0 - ( ( distance( color634.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color633 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _CLOTH1COLOR ) , saturate( ( 1.0 - ( ( distance( color633.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color632 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float temp_output_599_0 = saturate( ( 1.0 - ( ( distance( color632.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult602 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER4COLOR ) , temp_output_599_0);
				float4 color631 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_165_0 = saturate( ( 1.0 - ( ( distance( color631.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult602 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_165_0);
				float4 color630 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_158_0 = saturate( ( 1.0 - ( ( distance( color630.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_158_0);
				float4 color629 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_157_0 = saturate( ( 1.0 - ( ( distance( color629.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_157_0);
				float4 color628 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_603_0 = saturate( ( 1.0 - ( ( distance( color628.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult606 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_603_0);
				float4 color627 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_117_0 = saturate( ( 1.0 - ( ( distance( color627.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult606 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_117_0);
				float4 color625 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_127_0 = saturate( ( 1.0 - ( ( distance( color625.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_127_0);
				float4 color624 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_123_0 = saturate( ( 1.0 - ( ( distance( color624.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_123_0);
				float4 color623 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float temp_output_145_0 = saturate( ( 1.0 - ( ( distance( color623.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _OTHERCOLOR ) , temp_output_145_0);
				float4 color622 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color622.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _LIPSCOLOR ) , temp_output_149_0);
				float4 color621 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_150_0 = saturate( ( 1.0 - ( ( distance( color621.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _SCLERACOLOR ) , temp_output_150_0);
				float4 color618 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float temp_output_71_0 = saturate( ( 1.0 - ( ( distance( color618.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult73 = lerp( lerpResult153 , ( tex2DNode199 * _EYESCOLOR ) , temp_output_71_0);
				float4 color619 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float temp_output_67_0 = saturate( ( 1.0 - ( ( distance( color619.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult69 = lerp( lerpResult73 , ( tex2DNode199 * _HAIRCOLOR ) , temp_output_67_0);
				float4 color620 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float temp_output_63_0 = saturate( ( 1.0 - ( ( distance( color620.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult62 = lerp( lerpResult69 , ( tex2DNode199 * _SKINCOLOR ) , temp_output_63_0);
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord2.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult62 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				

				float3 BaseColor = lerpResult64.rgb;
				float3 Emission = 0;
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
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
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
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _TextureSample2_ST;
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _TextureSample0;
			sampler2D _COATOFARMSMASK;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord2.zw = v.ase_texcoord1.xy;

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
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;

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
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
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
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
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

				float2 uv_TextureSample2 = IN.ase_texcoord2.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color638 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample0 = IN.ase_texcoord2.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode617 = tex2D( _TextureSample0, uv_TextureSample0 );
				float4 lerpResult189 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color638.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color637 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color637.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color636 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult598 = lerp( lerpResult184 , ( tex2DNode199 * _CLOTH4COLOR ) , saturate( ( 1.0 - ( ( distance( color636.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color635 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult171 = lerp( lerpResult598 , ( tex2DNode199 * _CLOTH3COLOR ) , saturate( ( 1.0 - ( ( distance( color635.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color634 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _CLOTH2COLOR ) , saturate( ( 1.0 - ( ( distance( color634.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color633 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _CLOTH1COLOR ) , saturate( ( 1.0 - ( ( distance( color633.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color632 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float temp_output_599_0 = saturate( ( 1.0 - ( ( distance( color632.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult602 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER4COLOR ) , temp_output_599_0);
				float4 color631 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_165_0 = saturate( ( 1.0 - ( ( distance( color631.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult602 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_165_0);
				float4 color630 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_158_0 = saturate( ( 1.0 - ( ( distance( color630.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_158_0);
				float4 color629 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_157_0 = saturate( ( 1.0 - ( ( distance( color629.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_157_0);
				float4 color628 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_603_0 = saturate( ( 1.0 - ( ( distance( color628.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult606 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_603_0);
				float4 color627 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_117_0 = saturate( ( 1.0 - ( ( distance( color627.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult606 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_117_0);
				float4 color625 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_127_0 = saturate( ( 1.0 - ( ( distance( color625.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_127_0);
				float4 color624 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_123_0 = saturate( ( 1.0 - ( ( distance( color624.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_123_0);
				float4 color623 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float temp_output_145_0 = saturate( ( 1.0 - ( ( distance( color623.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _OTHERCOLOR ) , temp_output_145_0);
				float4 color622 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color622.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _LIPSCOLOR ) , temp_output_149_0);
				float4 color621 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_150_0 = saturate( ( 1.0 - ( ( distance( color621.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _SCLERACOLOR ) , temp_output_150_0);
				float4 color618 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float temp_output_71_0 = saturate( ( 1.0 - ( ( distance( color618.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult73 = lerp( lerpResult153 , ( tex2DNode199 * _EYESCOLOR ) , temp_output_71_0);
				float4 color619 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float temp_output_67_0 = saturate( ( 1.0 - ( ( distance( color619.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult69 = lerp( lerpResult73 , ( tex2DNode199 * _HAIRCOLOR ) , temp_output_67_0);
				float4 color620 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float temp_output_63_0 = saturate( ( 1.0 - ( ( distance( color620.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult62 = lerp( lerpResult69 , ( tex2DNode199 * _SKINCOLOR ) , temp_output_63_0);
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord2.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult62 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				

				float3 BaseColor = lerpResult64.rgb;
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
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			float4 _COATOFARMSCOLOR;
			float4 _HAIRCOLOR;
			float4 _EYESCOLOR;
			float4 _SCLERACOLOR;
			float4 _LIPSCOLOR;
			float4 _OTHERCOLOR;
			float4 _METAL1COLOR;
			float4 _METAL2COLOR;
			float4 _METAL3COLOR;
			float4 _METAL4COLOR;
			float4 _SKINCOLOR;
			float4 _LEATHER2COLOR;
			float4 _LEATHER3COLOR;
			float4 _LEATHER4COLOR;
			float4 _CLOTH1COLOR;
			float4 _CLOTH2COLOR;
			float4 _CLOTH3COLOR;
			float4 _CLOTH4COLOR;
			float4 _FEATHERS1COLOR;
			float4 _TextureSample0_ST;
			float4 _FEATHERS2COLOR;
			float4 _LEATHER1COLOR;
			float _METAL2SMOOTHNESS;
			float _METAL1SMOOTHNESS;
			float _OTHERSMOOTHNESS;
			float _HAIRSMOOTHNESS;
			float _SCLERASMOOTHNESS;
			float _EYESSMOOTHNESS;
			float _METAL3SMOOTHNESS;
			float _LIPSSMOOTHNESS;
			float _METAL4SMOOTHNESS;
			float _METAL1METALLIC;
			float _LEATHER2SMOOTHNESS;
			float _LEATHER3SMOOTHNESS;
			float _LEATHER4SMOOTHNES;
			float _SmoothnessOn;
			float _METAL2METALLIC;
			float _METAL3METALLIC;
			float _METAL4METALLIC;
			float _MetalicOn;
			float _SKINSMOOTHNESS;
			float _LEATHER1SMOOTHNESS;
			float _OCCLUSION;
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
			sampler2D _TextureSample0;
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
				float4 tex2DNode199 = tex2D( _TextureSample2, uv_TextureSample2 );
				float4 color638 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample0 = IN.ase_texcoord7.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float4 tex2DNode617 = tex2D( _TextureSample0, uv_TextureSample0 );
				float4 lerpResult189 = lerp( float4( 0,0,0,0 ) , ( tex2DNode199 * _FEATHERS2COLOR ) , saturate( ( 1.0 - ( ( distance( color638.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color637 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult184 = lerp( lerpResult189 , ( tex2DNode199 * _FEATHERS1COLOR ) , saturate( ( 1.0 - ( ( distance( color637.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color636 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult598 = lerp( lerpResult184 , ( tex2DNode199 * _CLOTH4COLOR ) , saturate( ( 1.0 - ( ( distance( color636.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color635 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult171 = lerp( lerpResult598 , ( tex2DNode199 * _CLOTH3COLOR ) , saturate( ( 1.0 - ( ( distance( color635.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color634 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult178 = lerp( lerpResult171 , ( tex2DNode199 * _CLOTH2COLOR ) , saturate( ( 1.0 - ( ( distance( color634.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color633 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult173 = lerp( lerpResult178 , ( tex2DNode199 * _CLOTH1COLOR ) , saturate( ( 1.0 - ( ( distance( color633.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color632 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float temp_output_599_0 = saturate( ( 1.0 - ( ( distance( color632.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult602 = lerp( lerpResult173 , ( tex2DNode199 * _LEATHER4COLOR ) , temp_output_599_0);
				float4 color631 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float temp_output_165_0 = saturate( ( 1.0 - ( ( distance( color631.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult160 = lerp( lerpResult602 , ( tex2DNode199 * _LEATHER3COLOR ) , temp_output_165_0);
				float4 color630 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float temp_output_158_0 = saturate( ( 1.0 - ( ( distance( color630.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult167 = lerp( lerpResult160 , ( tex2DNode199 * _LEATHER2COLOR ) , temp_output_158_0);
				float4 color629 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float temp_output_157_0 = saturate( ( 1.0 - ( ( distance( color629.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult162 = lerp( lerpResult167 , ( tex2DNode199 * _LEATHER1COLOR ) , temp_output_157_0);
				float4 color628 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float temp_output_603_0 = saturate( ( 1.0 - ( ( distance( color628.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult606 = lerp( lerpResult162 , ( tex2DNode199 * _METAL4COLOR ) , temp_output_603_0);
				float4 color627 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float temp_output_117_0 = saturate( ( 1.0 - ( ( distance( color627.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult118 = lerp( lerpResult606 , ( tex2DNode199 * _METAL3COLOR ) , temp_output_117_0);
				float4 color625 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float temp_output_127_0 = saturate( ( 1.0 - ( ( distance( color625.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult128 = lerp( lerpResult118 , ( tex2DNode199 * _METAL2COLOR ) , temp_output_127_0);
				float4 color624 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float temp_output_123_0 = saturate( ( 1.0 - ( ( distance( color624.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult122 = lerp( lerpResult128 , ( tex2DNode199 * _METAL1COLOR ) , temp_output_123_0);
				float4 color623 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float temp_output_145_0 = saturate( ( 1.0 - ( ( distance( color623.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult148 = lerp( lerpResult122 , ( tex2DNode199 * _OTHERCOLOR ) , temp_output_145_0);
				float4 color622 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float temp_output_149_0 = saturate( ( 1.0 - ( ( distance( color622.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult151 = lerp( lerpResult148 , ( tex2DNode199 * _LIPSCOLOR ) , temp_output_149_0);
				float4 color621 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float temp_output_150_0 = saturate( ( 1.0 - ( ( distance( color621.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult153 = lerp( lerpResult151 , ( tex2DNode199 * _SCLERACOLOR ) , temp_output_150_0);
				float4 color618 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float temp_output_71_0 = saturate( ( 1.0 - ( ( distance( color618.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult73 = lerp( lerpResult153 , ( tex2DNode199 * _EYESCOLOR ) , temp_output_71_0);
				float4 color619 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float temp_output_67_0 = saturate( ( 1.0 - ( ( distance( color619.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult69 = lerp( lerpResult73 , ( tex2DNode199 * _HAIRCOLOR ) , temp_output_67_0);
				float4 color620 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float temp_output_63_0 = saturate( ( 1.0 - ( ( distance( color620.rgb , tex2DNode617.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) );
				float4 lerpResult62 = lerp( lerpResult69 , ( tex2DNode199 * _SKINCOLOR ) , temp_output_63_0);
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord7.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult62 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				
				float lerpResult610 = lerp( 0.0 , _METAL4METALLIC , temp_output_603_0);
				float lerpResult315 = lerp( lerpResult610 , _METAL3METALLIC , temp_output_117_0);
				float lerpResult319 = lerp( lerpResult315 , _METAL2METALLIC , temp_output_127_0);
				float lerpResult316 = lerp( lerpResult319 , _METAL1METALLIC , temp_output_123_0);
				
				float lerpResult612 = lerp( 0.0 , _LEATHER4SMOOTHNES , temp_output_599_0);
				float lerpResult336 = lerp( lerpResult612 , _LEATHER3SMOOTHNESS , temp_output_165_0);
				float lerpResult332 = lerp( lerpResult336 , _LEATHER2SMOOTHNESS , temp_output_158_0);
				float lerpResult334 = lerp( lerpResult332 , _LEATHER1SMOOTHNESS , temp_output_157_0);
				float lerpResult608 = lerp( lerpResult334 , _METAL4SMOOTHNESS , temp_output_603_0);
				float lerpResult327 = lerp( lerpResult608 , _METAL3SMOOTHNESS , temp_output_117_0);
				float lerpResult331 = lerp( lerpResult327 , _METAL2SMOOTHNESS , temp_output_127_0);
				float lerpResult328 = lerp( lerpResult331 , _METAL1SMOOTHNESS , temp_output_123_0);
				float lerpResult321 = lerp( lerpResult328 , _OTHERSMOOTHNESS , temp_output_145_0);
				float lerpResult325 = lerp( lerpResult321 , _LIPSSMOOTHNESS , temp_output_149_0);
				float lerpResult322 = lerp( lerpResult325 , _SCLERASMOOTHNESS , temp_output_150_0);
				float lerpResult306 = lerp( lerpResult322 , _EYESSMOOTHNESS , temp_output_71_0);
				float lerpResult304 = lerp( lerpResult306 , _HAIRSMOOTHNESS , temp_output_67_0);
				float lerpResult302 = lerp( lerpResult304 , _SKINSMOOTHNESS , temp_output_63_0);
				

				float3 BaseColor = lerpResult64.rgb;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = (( _MetalicOn )?( lerpResult316 ):( 0.0 ));
				float Smoothness = (( _SmoothnessOn )?( lerpResult302 ):( 0.0 ));
				float Occlusion = (1.0 + (_OCCLUSION - 0.0) * (0.5 - 1.0) / (1.0 - 0.0));
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
Node;AmplifyShaderEditor.CommentaryNode;648;-15523.26,583.142;Inherit;False;439.4697;254.3918;Comment;1;617;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;647;-15561.67,102.9185;Inherit;False;483.4482;271.3363;Comment;1;199;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;212;-14813.92,104.0023;Inherit;False;1271.642;720.5787;Comment;8;184;186;189;637;183;226;225;638;FEATHERS COLORS;0.735849,0.7152051,0.3158597,1;0;0
Node;AmplifyShaderEditor.SamplerNode;617;-15449.58,618.5649;Inherit;True;Property;_TextureSample0;Texture Sample 0;43;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;6bb85a5d9f66498428d452f1d30c189c;6bb85a5d9f66498428d452f1d30c189c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;638;-14748.09,651.2271;Inherit;False;Constant;_Color27;Color 27;53;0;Create;True;0;0;0;False;0;False;0.4980392,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;225;-14738.24,165.3038;Inherit;False;Property;_FEATHERS2COLOR;FEATHERS 2 COLOR;37;1;[HDR];Create;True;0;0;0;False;0;False;0.6792453,0,0,0;0.6792453,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;199;-15462.87,148.1635;Inherit;True;Property;_TextureSample2;Texture Sample 2;44;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;f7390e40451b1494fbe67eb39e53d4ec;f7390e40451b1494fbe67eb39e53d4ec;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;637;-14210.09,644.2271;Inherit;False;Constant;_Color26;Color 26;53;0;Create;True;0;0;0;False;0;False;0.4980392,1,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;-14474.64,146.8632;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;226;-14193.24,161.3038;Inherit;False;Property;_FEATHERS1COLOR;FEATHERS 1 COLOR;36;1;[HDR];Create;True;0;0;0;False;0;False;0.7735849,0.492613,0.492613,0;0.7735849,0.492613,0.492613,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;180;-14504.2,631.4867;Inherit;True;Color Mask;-1;;112;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;208;-13458.26,101.9824;Inherit;False;2359.399;774.7212;Comment;20;596;598;595;636;597;173;168;178;175;633;169;172;171;209;177;210;176;634;635;211;CLOTH COLORS;0.4690726,0.7830189,0.47128,1;0;0
Node;AmplifyShaderEditor.FunctionNode;179;-13971.12,629.8313;Inherit;True;Color Mask;-1;;113;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;189;-14291.23,350.3367;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;636;-13458.59,672.1999;Inherit;False;Constant;_Color25;Color 25;53;0;Create;True;0;0;0;False;0;False;0,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;186;-13897.83,151.9253;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;595;-13418.15,197.0357;Inherit;False;Property;_CLOTH4COLOR;CLOTH 4 COLOR;35;1;[HDR];Create;True;0;0;0;False;0;False;0.2011392,0.3773585,0.3739074,0;0.9056604,0.6815338,0.4229263,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;597;-13137.79,171.5876;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;635;-12902.18,650.4863;Inherit;False;Constant;_Color24;Color 24;53;0;Create;True;0;0;0;False;0;False;0,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;211;-12946.7,169.5253;Inherit;False;Property;_CLOTH3COLOR;CLOTH 3 COLOR;34;1;[HDR];Create;True;0;0;0;False;0;False;0.3962264,0.3391397,0.2710039,0;0.9056604,0.6815338,0.4229263,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;596;-13182.24,635.7042;Inherit;True;Color Mask;-1;;114;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;184;-13714.46,359.6518;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;210;-12327.88,177.5926;Inherit;False;Property;_CLOTH2COLOR;CLOTH 2 COLOR;33;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;598;-12966.97,398.9717;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;345;-11038.79,1619.041;Inherit;False;2488.125;194.1752;Comment;8;334;332;333;335;336;337;611;612;LEATHER SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;204;-10950.96,81.85144;Inherit;False;2539.317;705.8575;Comment;16;601;602;600;162;164;167;160;161;201;202;166;157;203;158;165;599;LEATHER COLORS;0.7735849,0.5371538,0.1788003,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;177;-12655.9,151.9825;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;176;-12676.41,635.1213;Inherit;True;Color Mask;-1;;117;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;634;-12297.7,654.3358;Inherit;False;Constant;_Color23;Color 23;53;0;Create;True;0;0;0;False;0;False;0,1,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;632;-10842.41,668.59;Inherit;False;Constant;_Color21;Color 21;53;0;Create;True;0;0;0;False;0;False;1,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;599;-10571.77,640.5661;Inherit;True;Color Mask;-1;;118;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;633;-11771.7,656.3358;Inherit;False;Constant;_Color22;Color 22;53;0;Create;True;0;0;0;False;0;False;0,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;209;-11733.9,176.3902;Inherit;False;Property;_CLOTH1COLOR;CLOTH 1 COLOR;32;1;[HDR];Create;True;0;0;0;False;0;False;0,0.1792453,0.05062231,0;0,0.1142961,0.1698113,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;171;-12480.08,372.3667;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;611;-10859.22,1715.814;Inherit;False;Property;_LEATHER4SMOOTHNES;LEATHER 4 SMOOTHNES;31;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;169;-12068.59,637.2324;Inherit;True;Color Mask;-1;;124;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;631;-10257.41,661.59;Inherit;False;Constant;_Color20;Color 20;53;0;Create;True;0;0;0;False;0;False;1,1,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;-12043.04,154.3614;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;612;-10560.09,1692.95;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;175;-11466.22,159.4234;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;178;-11859.63,357.8348;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;168;-11499.91,637.3292;Inherit;True;Color Mask;-1;;128;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;630;-9602.412,660.59;Inherit;False;Constant;_Color19;Color 19;53;0;Create;True;0;0;0;False;0;False;1,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;600;-10810.29,159.6755;Inherit;False;Property;_LEATHER4COLOR;LEATHER 4 COLOR;30;1;[HDR];Create;True;0;0;0;False;0;False;0.1698113,0.04637412,0.02963688,1;0.1698113,0.04637412,0.02963688,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;337;-10344.01,1699.238;Inherit;False;Property;_LEATHER3SMOOTHNESS;LEATHER 3 SMOOTHNESS;29;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;165;-9991.497,642.3751;Inherit;True;Color Mask;-1;;129;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;335;-9711.607,1699.418;Inherit;False;Property;_LEATHER2SMOOTHNESS;LEATHER 2 SMOOTHNESS;27;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;629;-9078.412,661.59;Inherit;False;Constant;_Color18;Color 18;53;0;Create;True;0;0;0;False;0;False;1,0.4980392,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;336;-10044.88,1676.374;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;173;-11282.86,367.1498;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;601;-10543.71,135.0831;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;203;-10240.52,166.8846;Inherit;False;Property;_LEATHER3COLOR;LEATHER 3 COLOR;28;1;[HDR];Create;True;0;0;0;False;0;False;0.1698113,0.04637412,0.02963688,1;0.1698113,0.04637412,0.02963688,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;194;-8107.391,78.99294;Inherit;False;2541.828;775.4952;Comment;20;606;605;604;603;122;128;119;118;195;120;123;196;126;127;197;117;628;627;625;624;METAL COLORS;0.259434,0.8569208,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;158;-9334.879,645.8863;Inherit;True;Color Mask;-1;;130;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;332;-9418.603,1677.365;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;346;-7959.562,1650.51;Inherit;False;2395.888;216.9493;Comment;8;328;330;331;326;327;329;608;607;METAL SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;602;-10373.64,375.2108;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;628;-7924.997,616.7841;Inherit;False;Constant;_Color17;Color 17;53;0;Create;True;0;0;0;False;0;False;0.4980392,0.4980392,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;157;-8837.466,647.3965;Inherit;True;Color Mask;-1;;131;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;333;-9129.321,1694.408;Inherit;False;Property;_LEATHER1SMOOTHNESS;LEATHER 1 SMOOTHNESS;25;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;202;-9649.142,170.2566;Inherit;False;Property;_LEATHER2COLOR;LEATHER 2 COLOR;26;1;[HDR];Create;True;0;0;0;False;0;False;0.4245283,0.190437,0.09011215,1;0.4245283,0.190437,0.09011215,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;-9973.938,142.2922;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;334;-8809.511,1671.919;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;607;-7882.286,1718.982;Inherit;False;Property;_METAL4SMOOTHNESS;METAL 4 SMOOTHNESS;23;0;Create;True;0;0;0;False;0;False;0.7;0.721;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;627;-7362.862,629.4504;Inherit;False;Constant;_Color16;Color 16;53;0;Create;True;0;0;0;False;0;False;0,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;160;-9792.871,357.42;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;603;-7653.806,601.9574;Inherit;True;Color Mask;-1;;132;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;161;-9355.825,139.4149;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;201;-9059.142,163.2566;Inherit;False;Property;_LEATHER1COLOR;LEATHER 1 COLOR;24;1;[HDR];Create;True;0;0;0;False;0;False;0.4811321,0.2041155,0.08851016,1;0.4811321,0.2041155,0.08851016,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;625;-6747.165,632.5683;Inherit;False;Constant;_Color15;Color 15;53;0;Create;True;0;0;0;False;0;False;0,0,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;164;-8779.012,144.477;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;167;-9172.42,355.1142;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;604;-7903.43,147.498;Inherit;False;Property;_METAL4COLOR;METAL 4 COLOR;21;1;[HDR];Create;True;0;0;0;False;0;False;0.4383232,0.4383232,0.4716981,0;0.9528302,0.9528302,0.9528302,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;329;-7308.938,1704.062;Inherit;False;Property;_METAL3SMOOTHNESS;METAL 3 SMOOTHNESS;20;0;Create;True;0;0;0;False;0;False;0.7;0.721;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;117;-7107.278,602.937;Inherit;True;Color Mask;-1;;133;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;608;-7560.376,1669.961;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;326;-6674.535,1703.243;Inherit;False;Property;_METAL2SMOOTHNESS;METAL 2 SMOOTHNESS;17;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;624;-6252.165,631.5683;Inherit;False;Constant;_Color14;Color 14;53;0;Create;True;0;0;0;False;0;False;0.4980392,0,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;127;-6506.706,608.9541;Inherit;True;Color Mask;-1;;134;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;327;-7007.81,1680.198;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;190;-4960,96;Inherit;False;1932.122;803.2549;Comment;15;150;149;145;153;154;151;191;155;148;192;156;193;621;622;623;LIPS - SCARS - SCLERA COLORS;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;605;-7602.324,138.3599;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;162;-8595.646,352.2031;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;197;-7391.734,147.8021;Inherit;False;Property;_METAL3COLOR;METAL 3 COLOR;18;1;[HDR];Create;True;0;0;0;False;0;False;0.4383232,0.4383232,0.4716981,0;0.9528302,0.9528302,0.9528302,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;196;-6727.918,134.2548;Inherit;False;Property;_METAL2COLOR;METAL 2 COLOR;15;1;[HDR];Create;True;0;0;0;False;0;False;0.4674706,0.4677705,0.5188679,0;0.3301887,0.3301887,0.3301887,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;606;-7426.509,358.7441;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;331;-6381.532,1681.19;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;330;-6092.251,1698.233;Inherit;False;Property;_METAL1SMOOTHNESS;METAL 1 SMOOTHNESS;14;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;347;-4730.452,1597.091;Inherit;False;1768.499;211.4459;Comment;6;324;320;325;322;321;323;LIPS - SCARS - SCLERA SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;123;-5994.941,612.3985;Inherit;True;Color Mask;-1;;135;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;-7081.164,129.1932;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;623;-4843.854,630.0126;Inherit;False;Constant;_Color13;Color 13;53;0;Create;True;0;0;0;False;0;False;1,1,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;118;-6905.349,349.5774;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;195;-6160.556,103.4147;Inherit;False;Property;_METAL1COLOR;METAL 1 COLOR;12;1;[HDR];Create;True;0;0;0;False;0;False;0.8792791,0.9922886,1.007606,0;2,0.682353,0.1960784,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;-6468.303,131.5722;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;102;-2823.131,121.464;Inherit;False;2291.459;750.2154;Comment;16;63;67;71;62;581;69;41;73;582;583;75;74;618;619;620;649;SKIN - HAIR - EYES COLORS;1,0,0,1;0;0
Node;AmplifyShaderEditor.ColorNode;622;-4330.333,630.0126;Inherit;False;Constant;_Color12;Color 12;53;0;Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;328;-5784.439,1676.744;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;354;-7949.042,1067.135;Inherit;False;2384.88;225.2048;Comment;8;316;318;319;315;314;317;609;610;METAL METALLIC;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;323;-4683.02,1681.384;Inherit;False;Property;_OTHERSMOOTHNESS;OTHER SMOOTHNESS;11;0;Create;True;0;0;0;False;0;False;0.3;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;145;-4611.063,620.9051;Inherit;True;Color Mask;-1;;136;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;321;-4381.324,1651.545;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;128;-6284.896,335.0454;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;618;-2500.176,606.6602;Inherit;False;Constant;_Color8;Color 8;53;0;Create;True;0;0;0;False;0;False;1,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;149;-4072.347,610.9144;Inherit;True;Color Mask;-1;;137;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;621;-3758.865,656.5435;Inherit;False;Constant;_Color11;Color 11;53;0;Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;193;-4827.5,163.607;Inherit;False;Property;_OTHERCOLOR;OTHER COLOR;10;1;[HDR];Create;True;0;0;0;False;0;False;0.5188679,0.4637216,0.3206212,0;0.8490566,0.5037117,0.3884835,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;320;-4048.049,1674.59;Inherit;False;Property;_LIPSSMOOTHNESS;LIPS SMOOTHNESS;9;0;Create;True;0;0;0;False;0;False;0.4;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;609;-7857.733,1115.92;Inherit;False;Property;_METAL4METALLIC;METAL 4 METALLIC;22;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-5891.489,136.6342;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;-4549.59,161.4151;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;313;-2237.965,1601.742;Inherit;False;1768.499;211.4464;Comment;6;307;302;306;305;303;304;SKIN - EYES - HAIR SMOOTHNESS;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;122;-5708.124,344.3604;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;24;-1812.858,-1400.5;Inherit;False;1262.249;589.4722;;7;16;18;26;25;17;9;10;COAT OF ARMS;1,0,0.7651567,1;0;0
Node;AmplifyShaderEditor.FunctionNode;150;-3449.225,643.2929;Inherit;True;Color Mask;-1;;138;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;325;-3755.044,1652.537;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;71;-2208.283,588.9003;Inherit;True;Color Mask;-1;;139;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;324;-3465.761,1669.58;Inherit;False;Property;_SCLERASMOOTHNESS;SCLERA SMOOTHNESS;7;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;610;-7558.604,1092.056;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;317;-7296.417,1120.688;Inherit;False;Property;_METAL3METALLIC;METAL 3 METALLIC;19;0;Create;True;0;0;0;False;0;False;0.65;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;192;-4186.708,110.6664;Inherit;False;Property;_LIPSCOLOR;LIPS COLOR;8;1;[HDR];Create;True;0;0;0;False;0;False;0.8301887,0.3185886,0.2780349,0;0.8301887,0.3185886,0.2780349,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;322;-3145.953,1647.091;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;619;-1824.985,614.0381;Inherit;False;Constant;_Color9;Color 9;53;0;Create;True;0;0;0;False;0;False;1,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1769.44,-1325.746;Inherit;True;Property;_COATOFARMSMASK;COAT OF ARMS MASK;39;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;10;d294e9544b9eca64188ea9d2482ea8a1;d294e9544b9eca64188ea9d2482ea8a1;True;1;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;315;-6997.288,1096.824;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;314;-6664.014,1119.869;Inherit;False;Property;_METAL2METALLIC;METAL 2 METALLIC;16;0;Create;True;0;0;0;False;0;False;0.65;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;148;-4378.708,318.6664;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;649;-1978.038,693.0198;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;155;-3914.708,142.6664;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;191;-3610.708,126.6664;Inherit;False;Property;_SCLERACOLOR;SCLERA COLOR;6;1;[HDR];Create;True;0;0;0;False;0;False;0.9056604,0.8159487,0.8159487,0;0.9056604,0.8159487,0.8159487,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;307;-2187.965,1679.06;Inherit;False;Property;_EYESSMOOTHNESS;EYES SMOOTHNESS;3;0;Create;True;0;0;0;False;0;False;0.7;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-3338.708,142.6664;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;74;-2356.991,177.0945;Inherit;False;Property;_EYESCOLOR;EYES COLOR;2;1;[HDR];Create;True;0;0;0;False;0;False;0.0734529,0.1320755,0.05046281,1;0.0734529,0.1320755,0.05046281,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;67;-1574.557,598.8109;Inherit;True;Color Mask;-1;;140;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;318;-6082.924,1138.711;Inherit;False;Property;_METAL1METALLIC;METAL 1 METALLIC;13;0;Create;True;0;0;0;False;0;False;0.65;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;305;-1555.562,1679.241;Inherit;False;Property;_HAIRSMOOTHNESS;HAIR SMOOTHNESS;5;0;Create;True;0;0;0;False;0;False;0.1;0.622;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;620;-1215.816,617.7271;Inherit;False;Constant;_Color10;Color 10;53;0;Create;True;0;0;0;False;0;False;0.4980392,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;18;-1043.557,-1002.802;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;1.6,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;151;-3770.708,318.6664;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;9;-1435.856,-1221.265;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;319;-6371.01,1097.816;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;306;-1888.837,1656.196;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;26;-1063.837,-1185.19;Inherit;False;Property;_COATOFARMSCOLOR;COAT OF ARMS COLOR;38;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0.0990566,0.0990566,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;25;-1067.646,-1364.901;Inherit;False;Constant;_Color4;Color 4;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;153;-3194.708,318.6664;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;304;-1262.557,1657.188;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;17;-814.7589,-1227.691;Inherit;False;Replace Color;-1;;141;896dccb3016c847439def376a728b869;1,12,0;5;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;63;-959.473,600.9581;Inherit;True;Color Mask;-1;;142;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;583;-2018.989,155.1359;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;75;-1718.558,171.1508;Inherit;False;Property;_HAIRCOLOR;HAIR COLOR;4;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;0.2735849,0.213428,0.1432449,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;316;-5751.171,1108.492;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;303;-971.2782,1674.231;Inherit;False;Property;_SKINSMOOTHNESS;SKIN SMOOTHNESS;1;0;Create;True;0;0;0;False;0;False;0.3;0.426;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;-1243.999,-947.4095;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;584;754.506,1097.701;Inherit;False;Property;_MetalicOn;Metalic On;41;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;302;-653.4662,1651.742;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;574;-328.0893,-1201.828;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;576;-386.0649,-898.4222;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;73;-1862.558,331.1509;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;41;-1162.644,163.7047;Inherit;False;Property;_SKINCOLOR;SKIN COLOR;0;1;[HDR];Create;True;0;0;0;False;0;False;2.02193,1.0081,0.6199315,0;2.02193,1.0081,0.6199315,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;252;544.83,1759.57;Inherit;False;Property;_OCCLUSION;OCCLUSION;40;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;582;-1365.06,157.7437;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;579;854.0571,1759.464;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;581;-886.558,155.1508;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;573;-296.0682,-1167.712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;69;-1267.226,336.8063;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;575;-357.1902,-846.472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;585;788.8135,1603.558;Inherit;False;Property;_SmoothnessOn;Smoothness On;42;0;Create;True;0;0;0;False;0;False;1;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;62;-678.558,331.1509;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;578;-289.0642,343.9821;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;577;-353.6654,351.4588;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;64;-130.6749,330.3432;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;650;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;651;1178.721,338.6894;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;Polytope Studio/ PT_Medieval Modular NPC Shader PBR;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;18;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;Hidden/InternalErrorShader;0;0;Standard;38;Workflow;1;0;Surface;0;0;  Refraction Model;0;0;  Blend;0;0;Two Sided;1;0;Fragment Normal Space,InvertActionOnDeselection;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;  Use Shadow Threshold;0;0;Receive Shadows;1;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;_FinalColorxAlpha;0;0;Meta Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;DOTS Instancing;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Write Depth;0;0;  Early Z;0;0;Vertex Position,InvertActionOnDeselection;1;0;0;8;False;True;True;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;652;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;653;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;654;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;655;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;656;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthNormals;0;6;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormals;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;657;1178.721,338.6894;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;GBuffer;0;7;GBuffer;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalGBuffer;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;183;0;199;0
WireConnection;183;1;225;0
WireConnection;180;1;617;0
WireConnection;180;3;638;0
WireConnection;179;1;617;0
WireConnection;179;3;637;0
WireConnection;189;1;183;0
WireConnection;189;2;180;0
WireConnection;186;0;199;0
WireConnection;186;1;226;0
WireConnection;597;0;199;0
WireConnection;597;1;595;0
WireConnection;596;1;617;0
WireConnection;596;3;636;0
WireConnection;184;0;189;0
WireConnection;184;1;186;0
WireConnection;184;2;179;0
WireConnection;598;0;184;0
WireConnection;598;1;597;0
WireConnection;598;2;596;0
WireConnection;177;0;199;0
WireConnection;177;1;211;0
WireConnection;176;1;617;0
WireConnection;176;3;635;0
WireConnection;599;1;617;0
WireConnection;599;3;632;0
WireConnection;171;0;598;0
WireConnection;171;1;177;0
WireConnection;171;2;176;0
WireConnection;169;1;617;0
WireConnection;169;3;634;0
WireConnection;172;0;199;0
WireConnection;172;1;210;0
WireConnection;612;1;611;0
WireConnection;612;2;599;0
WireConnection;175;0;199;0
WireConnection;175;1;209;0
WireConnection;178;0;171;0
WireConnection;178;1;172;0
WireConnection;178;2;169;0
WireConnection;168;1;617;0
WireConnection;168;3;633;0
WireConnection;165;1;617;0
WireConnection;165;3;631;0
WireConnection;336;0;612;0
WireConnection;336;1;337;0
WireConnection;336;2;165;0
WireConnection;173;0;178;0
WireConnection;173;1;175;0
WireConnection;173;2;168;0
WireConnection;601;0;199;0
WireConnection;601;1;600;0
WireConnection;158;1;617;0
WireConnection;158;3;630;0
WireConnection;332;0;336;0
WireConnection;332;1;335;0
WireConnection;332;2;158;0
WireConnection;602;0;173;0
WireConnection;602;1;601;0
WireConnection;602;2;599;0
WireConnection;157;1;617;0
WireConnection;157;3;629;0
WireConnection;166;0;199;0
WireConnection;166;1;203;0
WireConnection;334;0;332;0
WireConnection;334;1;333;0
WireConnection;334;2;157;0
WireConnection;160;0;602;0
WireConnection;160;1;166;0
WireConnection;160;2;165;0
WireConnection;603;1;617;0
WireConnection;603;3;628;0
WireConnection;161;0;199;0
WireConnection;161;1;202;0
WireConnection;164;0;199;0
WireConnection;164;1;201;0
WireConnection;167;0;160;0
WireConnection;167;1;161;0
WireConnection;167;2;158;0
WireConnection;117;1;617;0
WireConnection;117;3;627;0
WireConnection;608;0;334;0
WireConnection;608;1;607;0
WireConnection;608;2;603;0
WireConnection;127;1;617;0
WireConnection;127;3;625;0
WireConnection;327;0;608;0
WireConnection;327;1;329;0
WireConnection;327;2;117;0
WireConnection;605;0;199;0
WireConnection;605;1;604;0
WireConnection;162;0;167;0
WireConnection;162;1;164;0
WireConnection;162;2;157;0
WireConnection;606;0;162;0
WireConnection;606;1;605;0
WireConnection;606;2;603;0
WireConnection;331;0;327;0
WireConnection;331;1;326;0
WireConnection;331;2;127;0
WireConnection;123;1;617;0
WireConnection;123;3;624;0
WireConnection;126;0;199;0
WireConnection;126;1;197;0
WireConnection;118;0;606;0
WireConnection;118;1;126;0
WireConnection;118;2;117;0
WireConnection;120;0;199;0
WireConnection;120;1;196;0
WireConnection;328;0;331;0
WireConnection;328;1;330;0
WireConnection;328;2;123;0
WireConnection;145;1;617;0
WireConnection;145;3;623;0
WireConnection;321;0;328;0
WireConnection;321;1;323;0
WireConnection;321;2;145;0
WireConnection;128;0;118;0
WireConnection;128;1;120;0
WireConnection;128;2;127;0
WireConnection;149;1;617;0
WireConnection;149;3;622;0
WireConnection;119;0;199;0
WireConnection;119;1;195;0
WireConnection;156;0;199;0
WireConnection;156;1;193;0
WireConnection;122;0;128;0
WireConnection;122;1;119;0
WireConnection;122;2;123;0
WireConnection;150;1;617;0
WireConnection;150;3;621;0
WireConnection;325;0;321;0
WireConnection;325;1;320;0
WireConnection;325;2;149;0
WireConnection;71;1;617;0
WireConnection;71;3;618;0
WireConnection;610;1;609;0
WireConnection;610;2;603;0
WireConnection;322;0;325;0
WireConnection;322;1;324;0
WireConnection;322;2;150;0
WireConnection;315;0;610;0
WireConnection;315;1;317;0
WireConnection;315;2;117;0
WireConnection;148;0;122;0
WireConnection;148;1;156;0
WireConnection;148;2;145;0
WireConnection;649;0;71;0
WireConnection;155;0;199;0
WireConnection;155;1;192;0
WireConnection;154;0;199;0
WireConnection;154;1;191;0
WireConnection;67;1;617;0
WireConnection;67;3;619;0
WireConnection;151;0;148;0
WireConnection;151;1;155;0
WireConnection;151;2;149;0
WireConnection;9;0;10;4
WireConnection;319;0;315;0
WireConnection;319;1;314;0
WireConnection;319;2;127;0
WireConnection;306;0;322;0
WireConnection;306;1;307;0
WireConnection;306;2;649;0
WireConnection;153;0;151;0
WireConnection;153;1;154;0
WireConnection;153;2;150;0
WireConnection;304;0;306;0
WireConnection;304;1;305;0
WireConnection;304;2;67;0
WireConnection;17;1;9;0
WireConnection;17;2;25;0
WireConnection;17;3;26;0
WireConnection;17;4;18;1
WireConnection;17;5;18;2
WireConnection;63;1;617;0
WireConnection;63;3;620;0
WireConnection;583;0;199;0
WireConnection;583;1;74;0
WireConnection;316;0;319;0
WireConnection;316;1;318;0
WireConnection;316;2;123;0
WireConnection;16;0;9;0
WireConnection;584;1;316;0
WireConnection;302;0;304;0
WireConnection;302;1;303;0
WireConnection;302;2;63;0
WireConnection;574;0;17;0
WireConnection;576;0;16;0
WireConnection;73;0;153;0
WireConnection;73;1;583;0
WireConnection;73;2;71;0
WireConnection;582;0;199;0
WireConnection;582;1;75;0
WireConnection;579;0;252;0
WireConnection;581;0;199;0
WireConnection;581;1;41;0
WireConnection;573;0;574;0
WireConnection;69;0;73;0
WireConnection;69;1;582;0
WireConnection;69;2;67;0
WireConnection;575;0;576;0
WireConnection;585;1;302;0
WireConnection;62;0;69;0
WireConnection;62;1;581;0
WireConnection;62;2;63;0
WireConnection;578;0;573;0
WireConnection;577;0;575;0
WireConnection;64;0;62;0
WireConnection;64;1;578;0
WireConnection;64;2;577;0
WireConnection;651;0;64;0
WireConnection;651;3;584;0
WireConnection;651;4;585;0
WireConnection;651;5;579;0
ASEEND*/
//CHKSM=91402B5362154A71E0BD4ED0B5CD7392456C5836