

void ToonBlit_float(float4 Input, float _MinCol, float _LightGap, float _RGap, float _GGap, float _BGap, out float4 Result)
{
	float4 col = Input;
	float aLight = col.r + col.g + col.b;
	if (aLight < 0.0001) {
		Result = float4(0, 0, 0, col.a);
		return;
	}
    float aR = col.r / aLight;
    float aG = col.g / aLight;
    float aB = col.b / aLight;
    if (aR < _MinCol) aR = _MinCol;
    if (aG < _MinCol) aG = _MinCol;
    if (aB < _MinCol) aB = _MinCol;
    aLight = round(aLight * _LightGap) / _LightGap;
    if (aLight < 0.0001) {
        Result = float4(0, 0, 0, Input.a);
        return;
    }
    aR = (round(aR * _RGap) * aLight) / _RGap;
    aG = (round(aG * _GGap) * aLight) / _GGap;
    aB = (round(aB * _BGap) * aLight) / _BGap;


    if (aR > 1) aR = 1;
             
    if (aG > 1) aG = 1;
              
    if (aB > 1) aB = 1;

    col.r = aR;// aR;
    col.g = aG;
    col.b = aB;

	Result = col;
	
}
