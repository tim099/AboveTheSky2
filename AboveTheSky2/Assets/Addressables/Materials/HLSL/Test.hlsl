

void Test_float (float2 screenPosition, float4 col,bool enableTestDot, out float4 outCol){
	if(!enableTestDot){
		outCol = col;
		return;
	}
	int x = floor(screenPosition.x * _ScreenParams.x);
	int y = floor(screenPosition.y * _ScreenParams.y);
	if(x % 2 == 0 || y % 2 == 0)
	{
		outCol = col;
	}else{
		if(x % 4 == 1){
			outCol = 0.3 * col;
		}
		if(y % 4 == 1){
			outCol = 0.4 * col;
		}
		else{
			outCol = 0.6 * col;
		}
		
	}
	

}
