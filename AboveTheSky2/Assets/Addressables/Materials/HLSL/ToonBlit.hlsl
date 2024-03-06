
///RGB to Hue Saturation Value
float3 ColorspaceConversion_RGB_To_HSV(float3 In)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
    float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
    float D = Q.x - min(Q.w, Q.y);
    float  E = 1e-10;
    return float3(abs(Q.z + (Q.w - Q.y)/(6.0 * D + E)), D / (Q.x + E), Q.x);
}
float3 ColorspaceConversion_HSV_To_RGB(float3 In)
{
    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
    return In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
}

void ToonBlit_float(float4 Input, float _RGap, float _GGap, float _BGap, out float4 Result)
{
    float3 HSV = ColorspaceConversion_RGB_To_HSV(Input);
    HSV.r = round(HSV.r * _RGap) / _RGap;
    HSV.g = round(HSV.g * _GGap) / _GGap;
    HSV.b = round(HSV.b * _BGap) / _BGap;


    float3 RGB = ColorspaceConversion_HSV_To_RGB(HSV);
    Result = float4(RGB.r,RGB.g,RGB.b, Input.a);

	//float4 col = Input;
	//float aLight = col.r + col.g + col.b;
	//if (aLight < 0.0001) {
	//	Result = float4(0, 0, 0, col.a);
	//	return;
	//}
 //   float aR = col.r / aLight;
 //   float aG = col.g / aLight;
 //   float aB = col.b / aLight;
 //   if (aR < _MinCol) aR = _MinCol;
 //   if (aG < _MinCol) aG = _MinCol;
 //   if (aB < _MinCol) aB = _MinCol;
 //   aLight = round(aLight * _LightGap) / _LightGap;
 //   if (aLight < 0.0001) {
 //       Result = float4(0, 0, 0, Input.a);
 //       return;
 //   }
 //   aR = (round(aR * _RGap) * aLight) / _RGap;
 //   aG = (round(aG * _GGap) * aLight) / _GGap;
 //   aB = (round(aB * _BGap) * aLight) / _BGap;


 //   if (aR > 1) aR = 1;
             
 //   if (aG > 1) aG = 1;
              
 //   if (aB > 1) aB = 1;

 //   col.r = aR;// aR;
 //   col.g = aG;
 //   col.b = aB;

	//Result = col;
	
}
