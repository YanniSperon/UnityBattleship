void MainLight_float(float3 WorldPosition, out float3 Direction, out float3 Color, out float DistanceAttenuation, out float ShadowAttenuation)
{
#ifdef SHADERGRAPH_PREVIEW
	Direction = normalize(float3(0.5, 0.5, 0.25));
	Color = float3(1.0, 1.0, 1.0);
	DistanceAttenuation = 1.0;
	ShadowAttenuation = 1.0;
#else
	float4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
	Light mainLight = GetMainLight(shadowCoord);

	Direction = mainLight.direction;
	Color = mainLight.color;
	DistanceAttenuation = mainLight.distanceAttenuation;
	ShadowAttenuation = mainLight.shadowAttenuation;
#endif
}

void MainLight_half(half3 WorldPosition, out half3 Direction, out half3 Color, out half DistanceAttenuation, out half ShadowAttenuation)
{
#ifdef SHADERGRAPH_PREVIEW
	Direction = normalize(half3(0.5, 0.5, 0.25));
	Color = half3(1.0, 1.0, 1.0);
	DistanceAttenuation = 1.0;
	ShadowAttenuation = 1.0;
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPosition);
	Light mainLight = GetMainLight(shadowCoord);

	Direction = mainLight.direction;
	Color = mainLight.color;
	DistanceAttenuation = mainLight.distanceAttenuation;
	ShadowAttenuation = mainLight.shadowAttenuation;
#endif
}

void AdditionalLight_float(float3 WorldPosition, int Index, out float3 Direction, out float3 Color, out float DistanceAttenuation, out float ShadowAttenuation)
{
	Direction = normalize(float3(0.5, 0.5, 0.25));
	Color = float3(0.0, 0.0, 0.0);
	DistanceAttenuation = 0.0;
	ShadowAttenuation = 0.0;

#ifndef SHADERGRAPH_PREVIEW
	int pixelLightCount = GetAdditionalLightsCount();
	if(Index < pixelLightCount)
	{
		Light light = GetAdditionalLight(Index, WorldPosition);

		Direction = light.direction;
		Color = light.color;
		DistanceAttenuation = light.distanceAttenuation;
		ShadowAttenuation = light.shadowAttenuation;
	}
#endif
}

void AdditionalLight_half(half3 WorldPosition, int Index, out half3 Direction, out half3 Color, out half DistanceAttenuation, out half ShadowAttenuation)
{
	Direction = normalize(half3(0.5, 0.5, 0.25));
	Color = half3(0.0, 0.0, 0.0);
	DistanceAttenuation = 0.0;
	ShadowAttenuation = 0.0;

#ifndef SHADERGRAPH_PREVIEW
	int pixelLightCount = GetAdditionalLightsCount();
	if(Index < pixelLightCount)
	{
		Light light = GetAdditionalLight(Index, WorldPosition);

		Direction = light.direction;
		Color = light.color;
		DistanceAttenuation = light.distanceAttenuation;
		ShadowAttenuation = light.shadowAttenuation;
	}
#endif
}