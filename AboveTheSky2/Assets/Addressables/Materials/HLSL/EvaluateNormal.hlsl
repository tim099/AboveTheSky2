

void EvaluateNormal_float(float3 A, float3 B, float3 C, float3 D, out float Result)
{
	float3 DelAB = A-B;
	float3 DelCD = C-D;
	//if(cross(DelAB,DelCD).r > 0){
		Result = length(DelAB) + length(DelCD);
	//}else
	//{
	//	Result = 0;
	//}
	
	
}
