

void MainLight_float(out float3 Direction, out float3 Color){

#if defined(SHADERGRAPH_PREVIEW)
    Direction = half3(0.5, 0.5, 0);
    Color = 1;
#else
    Light light = GetMainLight();
    Direction = light.direction;
    Color = light.color;
#endif

}
