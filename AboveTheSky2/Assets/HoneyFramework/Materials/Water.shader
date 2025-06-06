// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "HoneyFramework/Water" { 
Properties {
	_WaveScale ("Wave scale", Range (0.02,0.15)) = 0.063
	_ReflDistort ("Reflection distort", Range (0,1.5)) = 0.44	
    _WaveSpeed ("Wave speed (map1 x,y; map2 x,y)", Vector) = (2,.5,-2,-1.5)		
	_BumpMap ("Normalmap ", 2D) = "bump" {}	
	_ReflectiveColor ("Reflective color (RGB) fresnel (A) ", 2D) = "" {}	
	_WaterTone ("Water tone", COLOR)  = ( 0, .17, .29, 0)	    
}


// -----------------------------------------------------------
// Fragment program cards


Subshader { 
	Tags { "WaterMode"="Refractive" "RenderType"="Transparent" }
	Pass {
    
Blend SrcAlpha OneMinusSrcAlpha
      ZWrite Off
      Cull Off

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 



#include "UnityCG.cginc"

sampler2D _ReflectionTex;
sampler2D _ReflectiveColor;
float _ReflDistort;
float _WaveScale;
sampler2D _RefractionTex;
sampler2D _BumpMap;
sampler2D _CameraDepthTexture;
half4 _WaterTone;
half4 _WaveSpeed;

struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};

struct v2f {
	float4 pos : SV_POSITION;
	
	float4 ref : TEXCOORD0;
	float2 bumpuv0 : TEXCOORD1;
	float2 bumpuv1 : TEXCOORD2;
	float3 viewDir : TEXCOORD3;
    float3 worldPos : TEXCOORD4;
	
};

v2f vert(appdata v)
{
	v2f o;
	o.pos = UnityObjectToClipPos (v.vertex);
	

    float4 waveScale = float4(_WaveScale, _WaveScale, _WaveScale * 0.4, _WaveScale * 0.45);

    float4 waveOffset = float4( fmod(_WaveSpeed.x * waveScale.x * _Time.x, 1 ),
                                fmod(_WaveSpeed.y * waveScale.y * _Time.x, 1 ),
                                fmod(_WaveSpeed.z * waveScale.z * _Time.x, 1 ),
                                fmod(_WaveSpeed.w * waveScale.w * _Time.x, 1 ));


	// scroll bump waves
	float4 temp;
	temp.xyzw = v.vertex.xzxz * waveScale + waveOffset;
	o.bumpuv0 = temp.xy;
	o.bumpuv1 = temp.wz;
	
	// object space view direction (will normalize per pixel)
	o.viewDir.xzy = ObjSpaceViewDir(v.vertex);		
	o.ref = ComputeScreenPos(o.pos);
    o.worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;    	
	return o;
}

half4 frag( v2f i ) : SV_Target
{    
    //Water depth
    //Get the distance to the camera from the depth buffer for this point
    float sceneZ = LinearEyeDepth (tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.ref)).r);

	float3 viewDir = UNITY_MATRIX_IT_MV[2].xyz;
	//sceneZ = LinearEyeDepth(tex2D(_CameraDepthTexture, i.ref).r);
	float3 cameraDist = i.worldPos - _WorldSpaceCameraPos;

	float partZ = -dot(viewDir, cameraDist);

    //If the two are similar, then there is an object intersecting with our object
    fixed diff = sceneZ.x - partZ;
	diff = pow(diff, 0.3);
 
    fixed minDepthBorder = 0.32;
	fixed midDepth = 0.4;
    fixed depthBorder = 0.5;
    fixed waterDepth = 0;

	//low intensity on border to smoothen it.
	if (diff > minDepthBorder && diff <= midDepth)
	{
		fixed shalowBrackets = midDepth - minDepthBorder;
		waterDepth = 0.5 * (diff - minDepthBorder) / shalowBrackets;
	}
    else if (diff > midDepth && diff <= depthBorder)
    {        
		fixed deepBrackets = depthBorder - midDepth;
        waterDepth = 0.5 + 0.4 * (diff - midDepth) / deepBrackets;
    }
    else if (diff > depthBorder)
    {
        //higher intensity when depth is entered
        waterDepth = 0.9 + (diff - depthBorder)*10; 
    } 
	//waterDepth = diff * 5;
    //Water reflection adn refraction

	i.viewDir = normalize(i.viewDir);
	
	// combine two scrolling bumpmaps into one
	half3 bump1 = UnpackNormal(tex2D( _BumpMap, i.bumpuv0 )).rgb;
	half3 bump2 = UnpackNormal(tex2D( _BumpMap, i.bumpuv1 )).rgb;
	half3 bump = (bump1 + bump2) * 0.5;

	// fresnel factor
	half fresnelFac = dot( i.viewDir, bump*waterDepth );
	
	// perturb reflection/refraction UVs by bumpmap, and lookup colors
	
	
	float4 uv1 = i.ref; uv1.xy += bump * _ReflDistort;    
	
	// final color is between refracted and reflected based on fresnel	
	half4 color;

    float2 tmpUV = float2(i.worldPos.x, i.worldPos.z)/10;
    tmpUV += bump * waterDepth * _ReflDistort;
    tmpUV.xy += _Time.x/3;        
    half4 sky = tex2D(_ReflectiveColor, tmpUV);

	    
	color.rgb =  sky * 0.25 + _WaterTone * 0.75;
	color.a = waterDepth;

	return color;
}
ENDCG

	}
}
}
