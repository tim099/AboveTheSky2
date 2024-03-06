// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Polytope Studio/ PT_Medieval Modular NPC Shader Toon"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][HDR]_SKINCOLOR1("SKIN COLOR", Color) = (2.02193,1.0081,0.6199315,0)
		[HDR]_COATOFARMSCOLOR("COAT OF ARMS COLOR", Color) = (1,0,0,0)
		[NoScaleOffset]_COATOFARMSMASK("COAT OF ARMS MASK", 2D) = "black" {}
		[HDR]_EYESCOLOR1("EYES COLOR", Color) = (0.0734529,0.1320755,0.05046281,1)
		[HDR]_HAIRCOLOR1("HAIR COLOR", Color) = (1,0,0,0)
		[HDR]_SCLERACOLOR1("SCLERA COLOR", Color) = (0.9056604,0.8159487,0.8159487,0)
		[HDR]_LIPSCOLOR1("LIPS COLOR", Color) = (0.8301887,0.3185886,0.2780349,0)
		[HDR]_OTHERCOLOR1("OTHER COLOR", Color) = (0.5188679,0.4637216,0.3206212,0)
		[HDR]_METAL1COLOR1("METAL 1 COLOR", Color) = (0.8792791,0.9922886,1.007606,0)
		[HDR]_METAL2COLOR1("METAL 2 COLOR", Color) = (0.4674706,0.4677705,0.5188679,0)
		[HDR]_METAL3COLOR1("METAL 3 COLOR", Color) = (0.4383232,0.4383232,0.4716981,0)
		[HDR]_METAL4COLOR1("METAL 4 COLOR", Color) = (0.4383232,0.4383232,0.4716981,0)
		[HDR]_LEATHER1COLOR1("LEATHER 1 COLOR", Color) = (0.4811321,0.2041155,0.08851016,1)
		[HDR]_LEATHER2COLOR1("LEATHER 2 COLOR", Color) = (0.4245283,0.190437,0.09011215,1)
		[HDR]_LEATHER3COLOR1("LEATHER 3 COLOR", Color) = (0.1698113,0.04637412,0.02963688,1)
		[HDR]_LEATHER4COLOR1("LEATHER 4 COLOR", Color) = (0.1698113,0.04637412,0.02963688,1)
		[HDR]_CLOTH1COLOR1("CLOTH 1 COLOR", Color) = (0,0.1792453,0.05062231,0)
		[HDR]_CLOTH2COLOR1("CLOTH 2 COLOR", Color) = (1,0,0,0)
		[HDR]_CLOTH3COLOR1("CLOTH 3 COLOR", Color) = (0.3962264,0.3391397,0.2710039,0)
		[HDR]_CLOTH4COLOR1("CLOTH 4 COLOR", Color) = (0.2011392,0.3773585,0.3739074,0)
		[HDR]_FEATHERS1COLOR1("FEATHERS 1 COLOR", Color) = (0.7735849,0.492613,0.492613,0)
		[HDR]_FEATHERS2COLOR1("FEATHERS 2 COLOR", Color) = (0.6792453,0,0,0)
		_LIGHT1SIZE("LIGHT 1 SIZE", Range( 0 , 1)) = 0.1
		_LIGHT1POWER("LIGHT 1 POWER", Range( 0 , 1)) = 0
		[HDR]_LIGHT1COLOR("LIGHT 1 COLOR", Color) = (1,1,1,0)
		_LIGHT2SIZE("LIGHT 2 SIZE", Range( 0 , 1)) = 0.4
		_LIGHT2POWER("LIGHT 2 POWER", Range( 0 , 1)) = 0.15
		[HDR]_LIGHT2COLOR("LIGHT 2 COLOR", Color) = (1,1,1,0)
		_LIGHT3SIZE("LIGHT 3 SIZE", Range( 0 , 1)) = 0.8
		_LIGHT3POWER("LIGHT 3 POWER", Range( 0 , 1)) = 0.15
		[ASEEnd][HDR]_LIGHT3COLOR("LIGHT 3 COLOR", Color) = (1,1,1,0)
		[HideInInspector]_TextureSample1("Texture Sample 0", 2D) = "white" {}
		[HideInInspector]_TextureSample3("Texture Sample 2", 2D) = "white" {}
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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

			sampler2D _TextureSample3;
			sampler2D _TextureSample1;
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

				float2 uv_TextureSample3 = IN.ase_texcoord7.xy * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
				float4 tex2DNode722 = tex2D( _TextureSample3, uv_TextureSample3 );
				float4 color721 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample1 = IN.ase_texcoord7.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
				float4 tex2DNode724 = tex2D( _TextureSample1, uv_TextureSample1 );
				float4 lerpResult729 = lerp( float4( 0,0,0,0 ) , ( tex2DNode722 * _FEATHERS2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color721.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color728 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult737 = lerp( lerpResult729 , ( tex2DNode722 * _FEATHERS1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color728.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color732 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult745 = lerp( lerpResult737 , ( tex2DNode722 * _CLOTH4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color732.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color734 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult750 = lerp( lerpResult745 , ( tex2DNode722 * _CLOTH3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color734.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color741 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult754 = lerp( lerpResult750 , ( tex2DNode722 * _CLOTH2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color741.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color748 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult761 = lerp( lerpResult754 , ( tex2DNode722 * _CLOTH1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color748.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color735 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float4 lerpResult766 = lerp( lerpResult761 , ( tex2DNode722 * _LEATHER4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color735.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color746 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float4 lerpResult771 = lerp( lerpResult766 , ( tex2DNode722 * _LEATHER3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color746.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color753 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float4 lerpResult776 = lerp( lerpResult771 , ( tex2DNode722 * _LEATHER2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color753.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color755 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float4 lerpResult782 = lerp( lerpResult776 , ( tex2DNode722 * _LEATHER1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color755.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color762 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float4 lerpResult788 = lerp( lerpResult782 , ( tex2DNode722 * _METAL4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color762.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color769 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float4 lerpResult794 = lerp( lerpResult788 , ( tex2DNode722 * _METAL3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color769.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color770 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float4 lerpResult799 = lerp( lerpResult794 , ( tex2DNode722 * _METAL2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color770.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color778 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float4 lerpResult803 = lerp( lerpResult799 , ( tex2DNode722 * _METAL1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color778.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color784 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult807 = lerp( lerpResult803 , ( tex2DNode722 * _OTHERCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color784.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color786 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float4 lerpResult811 = lerp( lerpResult807 , ( tex2DNode722 * _LIPSCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color786.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color792 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float4 lerpResult815 = lerp( lerpResult811 , ( tex2DNode722 * _SCLERACOLOR1 ) , saturate( ( 1.0 - ( ( distance( color792.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color790 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult820 = lerp( lerpResult815 , ( tex2DNode722 * _EYESCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color790.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color804 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult821 = lerp( lerpResult820 , ( tex2DNode722 * _HAIRCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color804.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color806 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult823 = lerp( lerpResult821 , ( tex2DNode722 * _SKINCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color806.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord7.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult823 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				float fresnelNdotV369 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode369 = ( 0.0 + 1.0 * pow( max( 1.0 - fresnelNdotV369 , 0.0001 ), 1.0 ) );
				float4 temp_cast_41 = (step( fresnelNode369 , _LIGHT1SIZE )).xxxx;
				float4 blendOpSrc689 = temp_cast_41;
				float4 blendOpDest689 = _LIGHT1COLOR;
				float4 temp_cast_42 = ((0.0 + (_LIGHT1POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc683 = ( saturate( ( blendOpSrc689 * blendOpDest689 ) ));
				float4 blendOpDest683 = temp_cast_42;
				float4 blendOpSrc661 = ( blendOpSrc683 * blendOpDest683 );
				float4 blendOpDest661 = lerpResult64;
				float fresnelNdotV365 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode365 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV365, 1.0 ) );
				float4 temp_cast_43 = (step( fresnelNode365 , _LIGHT2SIZE )).xxxx;
				float4 blendOpSrc696 = temp_cast_43;
				float4 blendOpDest696 = _LIGHT2COLOR;
				float4 temp_cast_44 = ((0.0 + (_LIGHT2POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc686 = ( saturate( ( blendOpSrc696 * blendOpDest696 ) ));
				float4 blendOpDest686 = temp_cast_44;
				float4 blendOpSrc662 = ( blendOpSrc686 * blendOpDest686 );
				float4 blendOpDest662 = lerpResult64;
				float fresnelNdotV368 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode368 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV368, 1.0 ) );
				float4 temp_cast_45 = (step( fresnelNode368 , _LIGHT3SIZE )).xxxx;
				float4 blendOpSrc698 = temp_cast_45;
				float4 blendOpDest698 = _LIGHT3COLOR;
				float4 temp_cast_46 = ((0.0 + (_LIGHT3POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc687 = ( saturate( ( blendOpSrc698 * blendOpDest698 ) ));
				float4 blendOpDest687 = temp_cast_46;
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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

			sampler2D _TextureSample3;
			sampler2D _TextureSample1;
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

				float2 uv_TextureSample3 = IN.ase_texcoord2.xy * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
				float4 tex2DNode722 = tex2D( _TextureSample3, uv_TextureSample3 );
				float4 color721 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample1 = IN.ase_texcoord2.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
				float4 tex2DNode724 = tex2D( _TextureSample1, uv_TextureSample1 );
				float4 lerpResult729 = lerp( float4( 0,0,0,0 ) , ( tex2DNode722 * _FEATHERS2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color721.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color728 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult737 = lerp( lerpResult729 , ( tex2DNode722 * _FEATHERS1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color728.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color732 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult745 = lerp( lerpResult737 , ( tex2DNode722 * _CLOTH4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color732.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color734 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult750 = lerp( lerpResult745 , ( tex2DNode722 * _CLOTH3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color734.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color741 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult754 = lerp( lerpResult750 , ( tex2DNode722 * _CLOTH2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color741.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color748 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult761 = lerp( lerpResult754 , ( tex2DNode722 * _CLOTH1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color748.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color735 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float4 lerpResult766 = lerp( lerpResult761 , ( tex2DNode722 * _LEATHER4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color735.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color746 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float4 lerpResult771 = lerp( lerpResult766 , ( tex2DNode722 * _LEATHER3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color746.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color753 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float4 lerpResult776 = lerp( lerpResult771 , ( tex2DNode722 * _LEATHER2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color753.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color755 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float4 lerpResult782 = lerp( lerpResult776 , ( tex2DNode722 * _LEATHER1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color755.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color762 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float4 lerpResult788 = lerp( lerpResult782 , ( tex2DNode722 * _METAL4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color762.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color769 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float4 lerpResult794 = lerp( lerpResult788 , ( tex2DNode722 * _METAL3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color769.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color770 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float4 lerpResult799 = lerp( lerpResult794 , ( tex2DNode722 * _METAL2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color770.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color778 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float4 lerpResult803 = lerp( lerpResult799 , ( tex2DNode722 * _METAL1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color778.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color784 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult807 = lerp( lerpResult803 , ( tex2DNode722 * _OTHERCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color784.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color786 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float4 lerpResult811 = lerp( lerpResult807 , ( tex2DNode722 * _LIPSCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color786.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color792 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float4 lerpResult815 = lerp( lerpResult811 , ( tex2DNode722 * _SCLERACOLOR1 ) , saturate( ( 1.0 - ( ( distance( color792.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color790 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult820 = lerp( lerpResult815 , ( tex2DNode722 * _EYESCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color790.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color804 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult821 = lerp( lerpResult820 , ( tex2DNode722 * _HAIRCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color804.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color806 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult823 = lerp( lerpResult821 , ( tex2DNode722 * _SKINCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color806.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord2.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult823 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord3.xyz;
				float fresnelNdotV369 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode369 = ( 0.0 + 1.0 * pow( max( 1.0 - fresnelNdotV369 , 0.0001 ), 1.0 ) );
				float4 temp_cast_41 = (step( fresnelNode369 , _LIGHT1SIZE )).xxxx;
				float4 blendOpSrc689 = temp_cast_41;
				float4 blendOpDest689 = _LIGHT1COLOR;
				float4 temp_cast_42 = ((0.0 + (_LIGHT1POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc683 = ( saturate( ( blendOpSrc689 * blendOpDest689 ) ));
				float4 blendOpDest683 = temp_cast_42;
				float4 blendOpSrc661 = ( blendOpSrc683 * blendOpDest683 );
				float4 blendOpDest661 = lerpResult64;
				float fresnelNdotV365 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode365 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV365, 1.0 ) );
				float4 temp_cast_43 = (step( fresnelNode365 , _LIGHT2SIZE )).xxxx;
				float4 blendOpSrc696 = temp_cast_43;
				float4 blendOpDest696 = _LIGHT2COLOR;
				float4 temp_cast_44 = ((0.0 + (_LIGHT2POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc686 = ( saturate( ( blendOpSrc696 * blendOpDest696 ) ));
				float4 blendOpDest686 = temp_cast_44;
				float4 blendOpSrc662 = ( blendOpSrc686 * blendOpDest686 );
				float4 blendOpDest662 = lerpResult64;
				float fresnelNdotV368 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode368 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV368, 1.0 ) );
				float4 temp_cast_45 = (step( fresnelNode368 , _LIGHT3SIZE )).xxxx;
				float4 blendOpSrc698 = temp_cast_45;
				float4 blendOpDest698 = _LIGHT3COLOR;
				float4 temp_cast_46 = ((0.0 + (_LIGHT3POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc687 = ( saturate( ( blendOpSrc698 * blendOpDest698 ) ));
				float4 blendOpDest687 = temp_cast_46;
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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
			float4 _TextureSample3_ST;
			float4 _LIGHT2COLOR;
			float4 _LIGHT1COLOR;
			float4 _COATOFARMSCOLOR;
			float4 _SKINCOLOR1;
			float4 _HAIRCOLOR1;
			float4 _EYESCOLOR1;
			float4 _SCLERACOLOR1;
			float4 _LIPSCOLOR1;
			float4 _OTHERCOLOR1;
			float4 _LIGHT3COLOR;
			float4 _METAL2COLOR1;
			float4 _METAL3COLOR1;
			float4 _METAL1COLOR1;
			float4 _LEATHER1COLOR1;
			float4 _FEATHERS2COLOR1;
			float4 _TextureSample1_ST;
			float4 _METAL4COLOR1;
			float4 _CLOTH4COLOR1;
			float4 _CLOTH3COLOR1;
			float4 _FEATHERS1COLOR1;
			float4 _CLOTH1COLOR1;
			float4 _LEATHER4COLOR1;
			float4 _LEATHER3COLOR1;
			float4 _LEATHER2COLOR1;
			float4 _CLOTH2COLOR1;
			float _LIGHT1SIZE;
			float _LIGHT1POWER;
			float _LIGHT2SIZE;
			float _LIGHT2POWER;
			float _LIGHT3SIZE;
			float _LIGHT3POWER;
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

			sampler2D _TextureSample3;
			sampler2D _TextureSample1;
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

				float2 uv_TextureSample3 = IN.ase_texcoord7.xy * _TextureSample3_ST.xy + _TextureSample3_ST.zw;
				float4 tex2DNode722 = tex2D( _TextureSample3, uv_TextureSample3 );
				float4 color721 = IsGammaSpace() ? float4(0.4980392,1,1,1) : float4(0.2122307,1,1,1);
				float2 uv_TextureSample1 = IN.ase_texcoord7.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
				float4 tex2DNode724 = tex2D( _TextureSample1, uv_TextureSample1 );
				float4 lerpResult729 = lerp( float4( 0,0,0,0 ) , ( tex2DNode722 * _FEATHERS2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color721.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color728 = IsGammaSpace() ? float4(0.4980392,1,0.4980392,1) : float4(0.2122307,1,0.2122307,1);
				float4 lerpResult737 = lerp( lerpResult729 , ( tex2DNode722 * _FEATHERS1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color728.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color732 = IsGammaSpace() ? float4(0,0,1,1) : float4(0,0,1,1);
				float4 lerpResult745 = lerp( lerpResult737 , ( tex2DNode722 * _CLOTH4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color732.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color734 = IsGammaSpace() ? float4(0,1,1,1) : float4(0,1,1,1);
				float4 lerpResult750 = lerp( lerpResult745 , ( tex2DNode722 * _CLOTH3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color734.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color741 = IsGammaSpace() ? float4(0,1,0,1) : float4(0,1,0,1);
				float4 lerpResult754 = lerp( lerpResult750 , ( tex2DNode722 * _CLOTH2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color741.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color748 = IsGammaSpace() ? float4(0,0.4980392,0,1) : float4(0,0.2122307,0,1);
				float4 lerpResult761 = lerp( lerpResult754 , ( tex2DNode722 * _CLOTH1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color748.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color735 = IsGammaSpace() ? float4(1,0.4980392,0.4980392,1) : float4(1,0.2122307,0.2122307,1);
				float4 lerpResult766 = lerp( lerpResult761 , ( tex2DNode722 * _LEATHER4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color735.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color746 = IsGammaSpace() ? float4(1,1,0.4980392,1) : float4(1,1,0.2122307,1);
				float4 lerpResult771 = lerp( lerpResult766 , ( tex2DNode722 * _LEATHER3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color746.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color753 = IsGammaSpace() ? float4(1,0,1,1) : float4(1,0,1,1);
				float4 lerpResult776 = lerp( lerpResult771 , ( tex2DNode722 * _LEATHER2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color753.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color755 = IsGammaSpace() ? float4(1,0.4980392,1,1) : float4(1,0.2122307,1,1);
				float4 lerpResult782 = lerp( lerpResult776 , ( tex2DNode722 * _LEATHER1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color755.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color762 = IsGammaSpace() ? float4(0.4980392,0.4980392,1,1) : float4(0.2122307,0.2122307,1,1);
				float4 lerpResult788 = lerp( lerpResult782 , ( tex2DNode722 * _METAL4COLOR1 ) , saturate( ( 1.0 - ( ( distance( color762.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color769 = IsGammaSpace() ? float4(0,0.4980392,0.4980392,1) : float4(0,0.2122307,0.2122307,1);
				float4 lerpResult794 = lerp( lerpResult788 , ( tex2DNode722 * _METAL3COLOR1 ) , saturate( ( 1.0 - ( ( distance( color769.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color770 = IsGammaSpace() ? float4(0,0,0.4980392,1) : float4(0,0,0.2122307,1);
				float4 lerpResult799 = lerp( lerpResult794 , ( tex2DNode722 * _METAL2COLOR1 ) , saturate( ( 1.0 - ( ( distance( color770.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color778 = IsGammaSpace() ? float4(0.4980392,0,0.4980392,1) : float4(0.2122307,0,0.2122307,1);
				float4 lerpResult803 = lerp( lerpResult799 , ( tex2DNode722 * _METAL1COLOR1 ) , saturate( ( 1.0 - ( ( distance( color778.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color784 = IsGammaSpace() ? float4(1,1,0,1) : float4(1,1,0,1);
				float4 lerpResult807 = lerp( lerpResult803 , ( tex2DNode722 * _OTHERCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color784.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color786 = IsGammaSpace() ? float4(0.4980392,0.4980392,0,1) : float4(0.2122307,0.2122307,0,1);
				float4 lerpResult811 = lerp( lerpResult807 , ( tex2DNode722 * _LIPSCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color786.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color792 = IsGammaSpace() ? float4(0.4980392,0.4980392,0.4980392,1) : float4(0.2122307,0.2122307,0.2122307,1);
				float4 lerpResult815 = lerp( lerpResult811 , ( tex2DNode722 * _SCLERACOLOR1 ) , saturate( ( 1.0 - ( ( distance( color792.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color790 = IsGammaSpace() ? float4(1,0,0,1) : float4(1,0,0,1);
				float4 lerpResult820 = lerp( lerpResult815 , ( tex2DNode722 * _EYESCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color790.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color804 = IsGammaSpace() ? float4(1,0.4980392,0,1) : float4(1,0.2122307,0,1);
				float4 lerpResult821 = lerp( lerpResult820 , ( tex2DNode722 * _HAIRCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color804.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float4 color806 = IsGammaSpace() ? float4(0.4980392,0,0,1) : float4(0.2122307,0,0,1);
				float4 lerpResult823 = lerp( lerpResult821 , ( tex2DNode722 * _SKINCOLOR1 ) , saturate( ( 1.0 - ( ( distance( color806.rgb , tex2DNode724.rgb ) - 0.1 ) / max( 0.0 , 1E-05 ) ) ) ));
				float2 uv1_COATOFARMSMASK10 = IN.ase_texcoord7.zw;
				float temp_output_9_0 = ( 1.0 - tex2D( _COATOFARMSMASK, uv1_COATOFARMSMASK10 ).a );
				float4 temp_cast_40 = (temp_output_9_0).xxxx;
				float4 temp_output_1_0_g141 = temp_cast_40;
				float4 color25 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float4 temp_output_2_0_g141 = color25;
				float temp_output_11_0_g141 = distance( temp_output_1_0_g141 , temp_output_2_0_g141 );
				float2 _Vector0 = float2(1.6,1);
				float4 lerpResult21_g141 = lerp( _COATOFARMSCOLOR , temp_output_1_0_g141 , saturate( ( ( temp_output_11_0_g141 - _Vector0.x ) / max( _Vector0.y , 1E-05 ) ) ));
				float4 lerpResult64 = lerp( lerpResult823 , lerpResult21_g141 , ( 1.0 - temp_output_9_0 ));
				float fresnelNdotV369 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode369 = ( 0.0 + 1.0 * pow( max( 1.0 - fresnelNdotV369 , 0.0001 ), 1.0 ) );
				float4 temp_cast_41 = (step( fresnelNode369 , _LIGHT1SIZE )).xxxx;
				float4 blendOpSrc689 = temp_cast_41;
				float4 blendOpDest689 = _LIGHT1COLOR;
				float4 temp_cast_42 = ((0.0 + (_LIGHT1POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc683 = ( saturate( ( blendOpSrc689 * blendOpDest689 ) ));
				float4 blendOpDest683 = temp_cast_42;
				float4 blendOpSrc661 = ( blendOpSrc683 * blendOpDest683 );
				float4 blendOpDest661 = lerpResult64;
				float fresnelNdotV365 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode365 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV365, 1.0 ) );
				float4 temp_cast_43 = (step( fresnelNode365 , _LIGHT2SIZE )).xxxx;
				float4 blendOpSrc696 = temp_cast_43;
				float4 blendOpDest696 = _LIGHT2COLOR;
				float4 temp_cast_44 = ((0.0 + (_LIGHT2POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc686 = ( saturate( ( blendOpSrc696 * blendOpDest696 ) ));
				float4 blendOpDest686 = temp_cast_44;
				float4 blendOpSrc662 = ( blendOpSrc686 * blendOpDest686 );
				float4 blendOpDest662 = lerpResult64;
				float fresnelNdotV368 = dot( WorldNormal, WorldViewDirection );
				float fresnelNode368 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV368, 1.0 ) );
				float4 temp_cast_45 = (step( fresnelNode368 , _LIGHT3SIZE )).xxxx;
				float4 blendOpSrc698 = temp_cast_45;
				float4 blendOpDest698 = _LIGHT3COLOR;
				float4 temp_cast_46 = ((0.0 + (_LIGHT3POWER - 0.0) * (3.0 - 0.0) / (1.0 - 0.0))).xxxx;
				float4 blendOpSrc687 = ( saturate( ( blendOpSrc698 * blendOpDest698 ) ));
				float4 blendOpDest687 = temp_cast_46;
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
Node;AmplifyShaderEditor.CommentaryNode;714;-15992.46,563.8831;Inherit;False;439.4697;254.3918;Comment;1;724;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;713;-16030.87,83.65945;Inherit;False;483.4482;271.3363;Comment;1;722;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;720;-15283.12,84.74319;Inherit;False;1271.642;720.5787;Comment;10;737;733;730;729;728;727;726;725;723;721;FEATHERS COLORS;0.735849,0.7152051,0.3158597,1;0;0
Node;AmplifyShaderEditor.ColorNode;721;-15217.29,631.9681;Inherit;False;Constant;_Color28;Color 27;53;0;Create;True;0;0;0;False;0;False;0.4980392,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;722;-15932.07,128.9046;Inherit;True;Property;_TextureSample3;Texture Sample 2;32;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;f7390e40451b1494fbe67eb39e53d4ec;f7390e40451b1494fbe67eb39e53d4ec;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;723;-15207.44,146.0448;Inherit;False;Property;_FEATHERS2COLOR1;FEATHERS 2 COLOR;21;1;[HDR];Create;True;0;0;0;False;0;False;0.6792453,0,0,0;0.6792453,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;724;-15918.78,599.306;Inherit;True;Property;_TextureSample1;Texture Sample 0;31;1;[HideInInspector];Create;True;0;0;0;False;0;False;-1;6bb85a5d9f66498428d452f1d30c189c;6bb85a5d9f66498428d452f1d30c189c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;728;-14679.29,624.9681;Inherit;False;Constant;_Color27;Color 26;53;0;Create;True;0;0;0;False;0;False;0.4980392,1,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;715;-13927.46,82.72341;Inherit;False;2359.399;774.7212;Comment;19;761;758;756;754;752;751;750;749;748;745;744;742;741;740;739;738;736;734;731;CLOTH COLORS;0.4690726,0.7830189,0.47128,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;727;-14943.84,127.6043;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;726;-14662.44,142.0448;Inherit;False;Property;_FEATHERS1COLOR1;FEATHERS 1 COLOR;20;1;[HDR];Create;True;0;0;0;False;0;False;0.7735849,0.492613,0.492613,0;0.7735849,0.492613,0.492613,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;725;-14973.4,612.2277;Inherit;True;Color Mask;-1;;112;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;732;-13927.79,652.941;Inherit;False;Constant;_Color26;Color 25;53;0;Create;True;0;0;0;False;0;False;0,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;730;-14367.03,132.6664;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;733;-14440.32,610.5724;Inherit;True;Color Mask;-1;;113;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;729;-14760.43,331.0778;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;731;-13887.35,177.7767;Inherit;False;Property;_CLOTH4COLOR1;CLOTH 4 COLOR;19;1;[HDR];Create;True;0;0;0;False;0;False;0.2011392,0.3773585,0.3739074,0;0.9056604,0.6815338,0.4229263,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;739;-13415.9,150.2664;Inherit;False;Property;_CLOTH3COLOR1;CLOTH 3 COLOR;18;1;[HDR];Create;True;0;0;0;False;0;False;0.3962264,0.3391397,0.2710039,0;0.9056604,0.6815338,0.4229263,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;738;-13651.44,616.4454;Inherit;True;Color Mask;-1;;114;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;737;-14183.66,340.3929;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;736;-13606.99,152.3287;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;734;-13371.38,631.2274;Inherit;False;Constant;_Color25;Color 24;53;0;Create;True;0;0;0;False;0;False;0,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;742;-12797.08,158.3336;Inherit;False;Property;_CLOTH2COLOR1;CLOTH 2 COLOR;17;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;741;-12766.9,635.0768;Inherit;False;Constant;_Color24;Color 23;53;0;Create;True;0;0;0;False;0;False;0,1,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;745;-13436.17,379.7128;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;740;-13145.61,615.8624;Inherit;True;Color Mask;-1;;115;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;744;-13125.1,132.7235;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;749;-12537.79,617.9735;Inherit;True;Color Mask;-1;;124;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;716;-11420.16,62.59231;Inherit;False;2539.317;705.8575;Comment;20;782;779;776;773;772;771;768;767;766;764;763;760;759;757;755;753;747;746;743;735;LEATHER COLORS;0.7735849,0.5371538,0.1788003,1;0;0
Node;AmplifyShaderEditor.ColorNode;748;-12240.9,637.0768;Inherit;False;Constant;_Color23;Color 22;53;0;Create;True;0;0;0;False;0;False;0,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;750;-12949.28,353.1078;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;751;-12203.1,157.1312;Inherit;False;Property;_CLOTH1COLOR1;CLOTH 1 COLOR;16;1;[HDR];Create;True;0;0;0;False;0;False;0,0.1792453,0.05062231,0;0,0.1142961,0.1698113,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;752;-12512.24,135.1024;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;758;-11969.11,618.0704;Inherit;True;Color Mask;-1;;126;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;756;-11935.42,140.1644;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;754;-12328.83,338.5758;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;735;-11311.61,649.3312;Inherit;False;Constant;_Color22;Color 21;53;0;Create;True;0;0;0;False;0;False;1,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;757;-11279.49,140.4166;Inherit;False;Property;_LEATHER4COLOR1;LEATHER 4 COLOR;15;1;[HDR];Create;True;0;0;0;False;0;False;0.1698113,0.04637412,0.02963688,1;0.1698113,0.04637412,0.02963688,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;764;-11012.91,115.8241;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;746;-10726.61,642.3312;Inherit;False;Constant;_Color21;Color 20;53;0;Create;True;0;0;0;False;0;False;1,1,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;743;-11040.97,621.3073;Inherit;True;Color Mask;-1;;127;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;761;-11752.06,347.891;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;763;-10709.72,147.6256;Inherit;False;Property;_LEATHER3COLOR1;LEATHER 3 COLOR;14;1;[HDR];Create;True;0;0;0;False;0;False;0.1698113,0.04637412,0.02963688,1;0.1698113,0.04637412,0.02963688,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;747;-10460.7,623.1163;Inherit;True;Color Mask;-1;;128;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;753;-10071.61,641.3312;Inherit;False;Constant;_Color20;Color 19;53;0;Create;True;0;0;0;False;0;False;1,0,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;768;-10118.34,150.9977;Inherit;False;Property;_LEATHER2COLOR1;LEATHER 2 COLOR;13;1;[HDR];Create;True;0;0;0;False;0;False;0.4245283,0.190437,0.09011215,1;0.4245283,0.190437,0.09011215,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;766;-10842.84,355.9518;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;767;-10443.14,123.0333;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;755;-9547.609,642.3312;Inherit;False;Constant;_Color19;Color 18;53;0;Create;True;0;0;0;False;0;False;1,0.4980392,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;772;-9528.34,143.9977;Inherit;False;Property;_LEATHER1COLOR1;LEATHER 1 COLOR;12;1;[HDR];Create;True;0;0;0;False;0;False;0.4811321,0.2041155,0.08851016,1;0.4811321,0.2041155,0.08851016,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;717;-8576.588,59.73391;Inherit;False;2541.828;775.4952;Comment;20;803;799;796;794;793;791;788;787;785;783;781;780;778;777;775;774;770;769;765;762;METAL COLORS;0.259434,0.8569208,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;773;-9825.023,120.1558;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;759;-9804.076,626.6273;Inherit;True;Color Mask;-1;;129;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;771;-10262.07,338.161;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;760;-9306.664,628.1376;Inherit;True;Color Mask;-1;;130;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;779;-9248.209,125.2181;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;762;-8394.195,597.5253;Inherit;False;Constant;_Color18;Color 17;53;0;Create;True;0;0;0;False;0;False;0.4980392,0.4980392,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;776;-9641.617,335.8553;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;777;-8372.627,128.2391;Inherit;False;Property;_METAL4COLOR1;METAL 4 COLOR;11;1;[HDR];Create;True;0;0;0;False;0;False;0.4383232,0.4383232,0.4716981,0;0.9528302,0.9528302,0.9528302,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;781;-8071.521,119.1009;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;769;-7832.06,610.1915;Inherit;False;Constant;_Color17;Color 16;53;0;Create;True;0;0;0;False;0;False;0,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;765;-8123.002,582.6984;Inherit;True;Color Mask;-1;;131;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;782;-9064.844,332.9442;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;783;-7860.932,128.5431;Inherit;False;Property;_METAL3COLOR1;METAL 3 COLOR;10;1;[HDR];Create;True;0;0;0;False;0;False;0.4383232,0.4383232,0.4716981,0;0.9528302,0.9528302,0.9528302,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;770;-7216.364,613.3094;Inherit;False;Constant;_Color16;Color 15;53;0;Create;True;0;0;0;False;0;False;0,0,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;787;-7550.363,109.9341;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;788;-7895.707,339.4852;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;774;-7576.477,583.6781;Inherit;True;Color Mask;-1;;132;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;785;-7197.117,114.9959;Inherit;False;Property;_METAL2COLOR1;METAL 2 COLOR;9;1;[HDR];Create;True;0;0;0;False;0;False;0.4674706,0.4677705,0.5188679,0;0.3301887,0.3301887,0.3301887,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;775;-6975.905,589.6952;Inherit;True;Color Mask;-1;;133;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;794;-7374.548,330.3185;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;778;-6721.364,612.3094;Inherit;False;Constant;_Color15;Color 14;53;0;Create;True;0;0;0;False;0;False;0.4980392,0,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;791;-6937.502,112.3133;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;793;-6629.755,84.15578;Inherit;False;Property;_METAL1COLOR1;METAL 1 COLOR;8;1;[HDR];Create;True;0;0;0;False;0;False;0.8792791,0.9922886,1.007606,0;2,0.682353,0.1960784,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;718;-5429.199,76.74099;Inherit;False;1932.122;803.2549;Comment;15;815;814;811;810;808;807;802;801;798;797;795;792;789;786;784;LIPS - SCARS - SCLERA COLORS;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;799;-6754.095,315.7865;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;796;-6360.688,117.3753;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;798;-5296.699,144.348;Inherit;False;Property;_OTHERCOLOR1;OTHER COLOR;7;1;[HDR];Create;True;0;0;0;False;0;False;0.5188679,0.4637216,0.3206212,0;0.8490566,0.5037117,0.3884835,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;784;-5313.053,610.7535;Inherit;False;Constant;_Color14;Color 13;53;0;Create;True;0;0;0;False;0;False;1,1,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;780;-6464.14,593.1395;Inherit;True;Color Mask;-1;;134;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;786;-4799.532,610.7535;Inherit;False;Constant;_Color13;Color 12;53;0;Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;24;-1812.858,-1400.5;Inherit;False;1262.249;589.4722;;10;16;18;26;25;17;9;10;290;293;294;COAT OF ARMS;1,0,0.7651567,1;0;0
Node;AmplifyShaderEditor.LerpOp;803;-6177.323,325.1014;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;789;-5080.262,601.6461;Inherit;True;Color Mask;-1;;135;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;801;-5018.789,142.1561;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;802;-4655.907,91.4075;Inherit;False;Property;_LIPSCOLOR1;LIPS COLOR;6;1;[HDR];Create;True;0;0;0;False;0;False;0.8301887,0.3185886,0.2780349,0;0.8301887,0.3185886,0.2780349,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1769.44,-1325.746;Inherit;True;Property;_COATOFARMSMASK;COAT OF ARMS MASK;2;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;d294e9544b9eca64188ea9d2482ea8a1;d294e9544b9eca64188ea9d2482ea8a1;True;1;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;719;-3292.328,102.2051;Inherit;False;2291.459;750.2154;Comment;15;823;822;821;820;819;818;817;816;813;812;809;806;804;800;790;SKIN - HAIR - EYES COLORS;1,0,0,1;0;0
Node;AmplifyShaderEditor.ColorNode;792;-4228.064,637.2845;Inherit;False;Constant;_Color12;Color 11;53;0;Create;True;0;0;0;False;0;False;0.4980392,0.4980392,0.4980392,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;807;-4847.907,299.4076;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;808;-4383.907,123.4076;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;795;-4541.546,591.6554;Inherit;True;Color Mask;-1;;136;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;810;-4079.907,107.4075;Inherit;False;Property;_SCLERACOLOR1;SCLERA COLOR;5;1;[HDR];Create;True;0;0;0;False;0;False;0.9056604,0.8159487,0.8159487,0;0.9056604,0.8159487,0.8159487,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;790;-2969.373,587.4012;Inherit;False;Constant;_Color9;Color 8;53;0;Create;True;0;0;0;False;0;False;1,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;9;-1435.856,-1221.265;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;797;-3918.423,624.0341;Inherit;True;Color Mask;-1;;137;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;811;-4239.907,299.4076;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;812;-2826.188,157.8355;Inherit;False;Property;_EYESCOLOR1;EYES COLOR;3;1;[HDR];Create;True;0;0;0;False;0;False;0.0734529,0.1320755,0.05046281,1;0.0734529,0.1320755,0.05046281,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;814;-3807.906,123.4076;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;16;-1249.699,-965.4095;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;18;-1043.557,-1002.802;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;1.6,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;817;-2488.186,135.877;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;816;-2187.755,151.8919;Inherit;False;Property;_HAIRCOLOR1;HAIR COLOR;4;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;0.2735849,0.213428,0.1432449,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;815;-3663.905,299.4076;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;800;-2677.48,569.6414;Inherit;True;Color Mask;-1;;138;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;25;-1067.646,-1364.901;Inherit;False;Constant;_Color4;Color 4;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;26;-1063.837,-1185.19;Inherit;False;Property;_COATOFARMSCOLOR;COAT OF ARMS COLOR;1;1;[HDR];Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;804;-2294.182,594.7792;Inherit;False;Constant;_Color10;Color 9;53;0;Create;True;0;0;0;False;0;False;1,0.4980392,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;809;-2043.755,579.5519;Inherit;True;Color Mask;-1;;140;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;818;-1631.842,144.4456;Inherit;False;Property;_SKINCOLOR1;SKIN COLOR;0;1;[HDR];Create;True;0;0;0;False;0;False;2.02193,1.0081,0.6199315,0;2.02193,1.0081,0.6199315,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;17;-814.7589,-1227.691;Inherit;False;Replace Color;-1;;141;896dccb3016c847439def376a728b869;1,12,0;5;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;806;-1685.014,598.4681;Inherit;False;Constant;_Color11;Color 10;53;0;Create;True;0;0;0;False;0;False;0.4980392,0,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;360;-2384.559,956.9141;Inherit;False;2541.61;1272.682;Comment;32;379;662;663;661;686;559;560;390;683;687;688;684;685;374;371;370;369;367;368;365;364;366;362;361;363;689;690;695;697;699;700;701;TOON;0.990566,0.1822268,0.8019541,1;0;0
Node;AmplifyShaderEditor.LerpOp;820;-2331.755,311.8919;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;819;-1834.258,138.4847;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;294;-644.1989,-930.3562;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;821;-1736.424,317.5475;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;813;-1428.671,581.6993;Inherit;True;Color Mask;-1;;142;eec747d987850564c95bde0e5a6d1867;0;4;1;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.1;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;362;-2290.99,1147.003;Inherit;False;Constant;_2;2;35;0;Create;True;0;0;0;False;0;False;1;0.3991557;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;290;-633.3123,-1147.438;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;822;-1355.756,135.8919;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;363;-2264.815,1559.836;Inherit;False;Constant;_Float3;Float 3;31;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;361;-2262.561,1359.56;Inherit;False;Constant;_1;1;32;0;Create;True;0;0;0;False;0;False;1;0.3;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;293;-633.4137,-898.618;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;366;-1764.589,1647.723;Inherit;False;Property;_LIGHT3SIZE;LIGHT 3 SIZE;28;0;Create;True;0;0;0;False;0;False;0.8;0.706;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;823;-1147.756,311.8919;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;368;-1997.462,1519.467;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;291;-617.0846,363.4227;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;365;-1958.192,1272.703;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;369;-1956.678,1036.234;Inherit;False;Standard;WorldNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;364;-1741.184,1402.56;Inherit;False;Property;_LIGHT2SIZE;LIGHT 2 SIZE;25;0;Create;True;0;0;0;False;0;False;0.4;0.243;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;292;-620.0846,401.4227;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;367;-1763.215,1206.693;Inherit;False;Property;_LIGHT1SIZE;LIGHT 1 SIZE;22;0;Create;True;0;0;0;False;0;False;0.1;0.45;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;684;-1152.591,1217.51;Inherit;False;Property;_LIGHT1POWER;LIGHT 1 POWER;23;0;Create;True;0;0;0;False;0;False;0;0.22;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;697;-1341.959,1671.755;Inherit;False;Property;_LIGHT3COLOR;LIGHT 3 COLOR;30;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;695;-1338.313,1422.022;Inherit;False;Property;_LIGHT2COLOR;LIGHT 2 COLOR;27;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;690;-1382.336,1176.092;Inherit;False;Property;_LIGHT1COLOR;LIGHT 1 COLOR;24;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;685;-1116.953,1474.047;Inherit;False;Property;_LIGHT2POWER;LIGHT 2 POWER;26;0;Create;True;0;0;0;False;0;False;0.15;0.206;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;688;-1102.397,1725.987;Inherit;False;Property;_LIGHT3POWER;LIGHT 3 POWER;29;0;Create;True;0;0;0;False;0;False;0.15;0.247;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;64;-588.6503,335.5556;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;374;-1422.798,1338.325;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;370;-1455.669,1593.09;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;371;-1481.249,1063.553;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;698;-1118.693,1581.753;Inherit;False;Multiply;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;700;-848.9835,1463.261;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;699;-868.8555,1208.175;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;395;-405.1466,381.3712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;394;-405.1466,380.3712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;696;-1115.047,1332.02;Inherit;False;Multiply;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;396;-404.1466,380.3712;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;689;-1114.33,1075.988;Inherit;False;Multiply;True;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;701;-822.0625,1730.397;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;559;-412.1298,1549.633;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;390;-409.3758,1069.185;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;687;-694.37,1584.548;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;686;-704.9259,1334.608;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;560;-404.1298,1339.633;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;683;-760.7569,1072.747;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;661;-351.4232,1095.355;Inherit;False;Multiply;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;663;-359.8096,1571.366;Inherit;False;Multiply;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;662;-345.4095,1304.167;Inherit;False;Multiply;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;379;-48.5603,1282.605;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;382;501.8256,334.2374;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;824;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;825;765.0967,296.2389;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;Polytope Studio/ PT_Medieval Modular NPC Shader Toon;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;18;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;Hidden/InternalErrorShader;0;0;Standard;38;Workflow;1;0;Surface;0;0;  Refraction Model;0;0;  Blend;0;0;Two Sided;1;0;Fragment Normal Space,InvertActionOnDeselection;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;  Use Shadow Threshold;0;0;Receive Shadows;1;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;_FinalColorxAlpha;0;0;Meta Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;DOTS Instancing;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Write Depth;0;0;  Early Z;0;0;Vertex Position,InvertActionOnDeselection;1;0;0;8;False;True;True;True;True;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;826;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;827;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;828;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;829;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;830;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthNormals;0;6;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormals;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;831;765.0967,296.2389;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;GBuffer;0;7;GBuffer;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;3;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalGBuffer;False;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;727;0;722;0
WireConnection;727;1;723;0
WireConnection;725;1;724;0
WireConnection;725;3;721;0
WireConnection;730;0;722;0
WireConnection;730;1;726;0
WireConnection;733;1;724;0
WireConnection;733;3;728;0
WireConnection;729;1;727;0
WireConnection;729;2;725;0
WireConnection;738;1;724;0
WireConnection;738;3;732;0
WireConnection;737;0;729;0
WireConnection;737;1;730;0
WireConnection;737;2;733;0
WireConnection;736;0;722;0
WireConnection;736;1;731;0
WireConnection;745;0;737;0
WireConnection;745;1;736;0
WireConnection;745;2;738;0
WireConnection;740;1;724;0
WireConnection;740;3;734;0
WireConnection;744;0;722;0
WireConnection;744;1;739;0
WireConnection;749;1;724;0
WireConnection;749;3;741;0
WireConnection;750;0;745;0
WireConnection;750;1;744;0
WireConnection;750;2;740;0
WireConnection;752;0;722;0
WireConnection;752;1;742;0
WireConnection;758;1;724;0
WireConnection;758;3;748;0
WireConnection;756;0;722;0
WireConnection;756;1;751;0
WireConnection;754;0;750;0
WireConnection;754;1;752;0
WireConnection;754;2;749;0
WireConnection;764;0;722;0
WireConnection;764;1;757;0
WireConnection;743;1;724;0
WireConnection;743;3;735;0
WireConnection;761;0;754;0
WireConnection;761;1;756;0
WireConnection;761;2;758;0
WireConnection;747;1;724;0
WireConnection;747;3;746;0
WireConnection;766;0;761;0
WireConnection;766;1;764;0
WireConnection;766;2;743;0
WireConnection;767;0;722;0
WireConnection;767;1;763;0
WireConnection;773;0;722;0
WireConnection;773;1;768;0
WireConnection;759;1;724;0
WireConnection;759;3;753;0
WireConnection;771;0;766;0
WireConnection;771;1;767;0
WireConnection;771;2;747;0
WireConnection;760;1;724;0
WireConnection;760;3;755;0
WireConnection;779;0;722;0
WireConnection;779;1;772;0
WireConnection;776;0;771;0
WireConnection;776;1;773;0
WireConnection;776;2;759;0
WireConnection;781;0;722;0
WireConnection;781;1;777;0
WireConnection;765;1;724;0
WireConnection;765;3;762;0
WireConnection;782;0;776;0
WireConnection;782;1;779;0
WireConnection;782;2;760;0
WireConnection;787;0;722;0
WireConnection;787;1;783;0
WireConnection;788;0;782;0
WireConnection;788;1;781;0
WireConnection;788;2;765;0
WireConnection;774;1;724;0
WireConnection;774;3;769;0
WireConnection;775;1;724;0
WireConnection;775;3;770;0
WireConnection;794;0;788;0
WireConnection;794;1;787;0
WireConnection;794;2;774;0
WireConnection;791;0;722;0
WireConnection;791;1;785;0
WireConnection;799;0;794;0
WireConnection;799;1;791;0
WireConnection;799;2;775;0
WireConnection;796;0;722;0
WireConnection;796;1;793;0
WireConnection;780;1;724;0
WireConnection;780;3;778;0
WireConnection;803;0;799;0
WireConnection;803;1;796;0
WireConnection;803;2;780;0
WireConnection;789;1;724;0
WireConnection;789;3;784;0
WireConnection;801;0;722;0
WireConnection;801;1;798;0
WireConnection;807;0;803;0
WireConnection;807;1;801;0
WireConnection;807;2;789;0
WireConnection;808;0;722;0
WireConnection;808;1;802;0
WireConnection;795;1;724;0
WireConnection;795;3;786;0
WireConnection;9;0;10;4
WireConnection;797;1;724;0
WireConnection;797;3;792;0
WireConnection;811;0;807;0
WireConnection;811;1;808;0
WireConnection;811;2;795;0
WireConnection;814;0;722;0
WireConnection;814;1;810;0
WireConnection;16;0;9;0
WireConnection;817;0;722;0
WireConnection;817;1;812;0
WireConnection;815;0;811;0
WireConnection;815;1;814;0
WireConnection;815;2;797;0
WireConnection;800;1;724;0
WireConnection;800;3;790;0
WireConnection;809;1;724;0
WireConnection;809;3;804;0
WireConnection;17;1;9;0
WireConnection;17;2;25;0
WireConnection;17;3;26;0
WireConnection;17;4;18;1
WireConnection;17;5;18;2
WireConnection;820;0;815;0
WireConnection;820;1;817;0
WireConnection;820;2;800;0
WireConnection;819;0;722;0
WireConnection;819;1;816;0
WireConnection;294;0;16;0
WireConnection;821;0;820;0
WireConnection;821;1;819;0
WireConnection;821;2;809;0
WireConnection;813;1;724;0
WireConnection;813;3;806;0
WireConnection;290;0;17;0
WireConnection;822;0;722;0
WireConnection;822;1;818;0
WireConnection;293;0;294;0
WireConnection;823;0;821;0
WireConnection;823;1;822;0
WireConnection;823;2;813;0
WireConnection;368;3;363;0
WireConnection;291;0;290;0
WireConnection;365;3;361;0
WireConnection;369;3;362;0
WireConnection;292;0;293;0
WireConnection;64;0;823;0
WireConnection;64;1;291;0
WireConnection;64;2;292;0
WireConnection;374;0;365;0
WireConnection;374;1;364;0
WireConnection;370;0;368;0
WireConnection;370;1;366;0
WireConnection;371;0;369;0
WireConnection;371;1;367;0
WireConnection;698;0;370;0
WireConnection;698;1;697;0
WireConnection;700;0;685;0
WireConnection;699;0;684;0
WireConnection;395;0;64;0
WireConnection;394;0;64;0
WireConnection;696;0;374;0
WireConnection;696;1;695;0
WireConnection;396;0;64;0
WireConnection;689;0;371;0
WireConnection;689;1;690;0
WireConnection;701;0;688;0
WireConnection;559;0;394;0
WireConnection;390;0;396;0
WireConnection;687;0;698;0
WireConnection;687;1;701;0
WireConnection;686;0;696;0
WireConnection;686;1;700;0
WireConnection;560;0;395;0
WireConnection;683;0;689;0
WireConnection;683;1;699;0
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
WireConnection;825;2;382;0
ASEEND*/
//CHKSM=967C9BFB2799AEFE325A45AE660CF9C938AE60F7