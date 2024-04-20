#ifndef GroundMatTest
#define GroundMatTest

void GroundMatTest_float (float2 screenPosition, out float4 color){
	//float3 normal = SHADERGRAPH_SAMPLE_SCENE_NORMAL(screenPosition);
	float3 depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(screenPosition);
	float3 worldPos = ComputeWorldSpacePosition(screenPosition, depth, UNITY_MATRIX_I_VP);
    // The following part creates the checkerboard effect.


    // Scale is the inverse size of the squares.
    //uint scale = 1;
    // Scale, mirror and snap the coordinates.
    //uint3 worldIntPos = uint3(abs(worldPos.xyz));// * scale
    


    // Divide the surface into squares. Calculate the color ID value.
    //bool white = true;// ((worldIntPos.x) & 1) ^ (worldIntPos.y & 1) ;//^ (worldIntPos.z & 1)

    float xPos = worldPos.x - floor(worldPos.x);
    float zPos = worldPos.y - floor(worldPos.y);
    const float width  = 0.01;
    if(xPos < width || xPos > 1-width ||
        zPos < width || zPos > 1-width){
        color = float4(0,0,0,1);
    }else{
        color = float4(xPos, zPos,1,1);
    }

    // Color the square based on the ID value (black or white).
    //color = white ? float4(1,1,1,1) : float4(0,0,0,1);
}
#endif // GroundMatTest
