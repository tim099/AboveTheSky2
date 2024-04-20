

void EvaluateNormal_float(float2 _A, float2 _B, float2 _C, float2 _D, out float Result)
{
	float3 A = SHADERGRAPH_SAMPLE_SCENE_NORMAL(_A);
	float3 B = SHADERGRAPH_SAMPLE_SCENE_NORMAL(_B);
	float3 C = SHADERGRAPH_SAMPLE_SCENE_NORMAL(_C);
	float3 D = SHADERGRAPH_SAMPLE_SCENE_NORMAL(_D);

	float3 DelAB = A-B;
	float3 DelCD = C-D;
	//if(cross(DelAB,DelCD).r > 0){
		Result = length(DelAB) + length(DelCD);
	//}else
	//{
	//	Result = 0;
	//}
	
	
}
