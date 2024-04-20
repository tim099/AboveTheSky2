static float2 sobelSamplePoints[9] = {
		float2(-1,1),float2(0,1),float2(1,1),
		float2(-1,0),float2(0,0),float2(1,0),
		float2(-1,-1),float2(0,-1),float2(1,-1),
};
static float sobelXMatrix[9] = {
	1,0,-1,
	2,0,-2,
	1,0,-1,
};
static float sobelYMatrix[9] = {
	 1,  2,  1,
	 0,  0,  0,
	-1, -2, -1,
};

void EvaluateNormal_float(float2 screenPosition, float normalThreshold, float depthWeight, out float Result)
{
	float Div_Screen_Width = 1.0/_ScreenParams.x;
	float Div_Screen_Height = 1.0/_ScreenParams.y;

	
	float2 up = screenPosition;
	float2 down = screenPosition;

	float2 left = screenPosition;
	float2 right = screenPosition;
	up.y += Div_Screen_Height;
	down.y -= Div_Screen_Height;
	right.x += Div_Screen_Width;
	left.x	-= Div_Screen_Width;
	
	float2 upR = up;
	float2 upL = up;

	float2 downL = down;
	float2 downR = down;	

	downR.x = upR.x = right.x;	
	downL.x = upL.x = left.x;





	float2 sobel = 0;
	float3 sobelNormalX = 0;
	float3 sobelNormalY = 0;
	float2 sobelNormalResults[9];
	float2 thickness = 0.5 * float2(Div_Screen_Width, Div_Screen_Height);//1 pixel outline!!
	[unroll] for(int i=0; i < 9; i++)
	{
		float2 uv = screenPosition + thickness * sobelSamplePoints[i];
		float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv);
		sobel += depth * float2(sobelXMatrix[i], sobelYMatrix[i]);


		float3 normal = SHADERGRAPH_SAMPLE_SCENE_NORMAL(uv);
		sobelNormalResults[i] = normal;
		sobelNormalX += normal * sobelXMatrix[i];
		sobelNormalY += normal * sobelYMatrix[i];
	}
	float depthResult = depthWeight * length(sobel);
	depthResult = step(0.5, depthResult);

	float3 normalUp = SHADERGRAPH_SAMPLE_SCENE_NORMAL(screenPosition + thickness * sobelSamplePoints[1]);//up
	float3 normalDown = SHADERGRAPH_SAMPLE_SCENE_NORMAL(screenPosition + thickness * sobelSamplePoints[7]);//down
	float3 normalRight = SHADERGRAPH_SAMPLE_SCENE_NORMAL(screenPosition + thickness * sobelSamplePoints[5]);//right
	float3 normalLeft = SHADERGRAPH_SAMPLE_SCENE_NORMAL(screenPosition + thickness * sobelSamplePoints[3]);//left

	float3 DelY = normalUp - normalDown;
	float3 DelX = normalRight - normalLeft;
	float normalResult = length(DelY) + length(DelX);
	normalResult = step(normalThreshold, normalResult);
	//float normalResult = depthWeight * length(sobelNormal);
	//normalResult = step(normalThreshold, normalResult);

	Result = step(1, depthResult + normalResult);//normalResult + 

	//if(cross(DelAB,DelCD).r > 0){
		//Result = length(DelAB) + length(DelCD);
	//}else
	//{
	//	Result = 0;
	//}
}
