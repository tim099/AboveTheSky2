#ifndef SAMPLE_FULL_SCREEN_UV
#define SAMPLE_FULL_SCREEN_UV

void SampleFullScreenUV_float (float2 screenPosition, out float2 up, out float2 down, out float2 right, out float2 left){

	float Div_Screen_Width = 1.0/_ScreenParams.x;
	float Div_Screen_Height = 1.0/_ScreenParams.y;
	up = down = left = right = screenPosition;
	up.y += Div_Screen_Height;
	down.y -= Div_Screen_Height;

	right.x += Div_Screen_Width;
	left.x -= Div_Screen_Width;

}
void SampleFullScreenUV_half (float2 screenPosition, out float2 up, out float2 down, out float2 right, out float2 left){
	float Div_Screen_Width = 1.0/_ScreenParams.x;
	float Div_Screen_Height = 1.0/_ScreenParams.y;
	up = down = left = right = screenPosition;
	up.y += Div_Screen_Height;
	down.y -= Div_Screen_Height;

	right.x += Div_Screen_Width;
	left.x -= Div_Screen_Width;
}
#endif // SAMPLE_FULL_SCREEN_UV
