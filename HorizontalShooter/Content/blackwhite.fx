sampler s0;
float param1;

float4 PixelShaderFunction(float4 pos : SV_POSITION, float4 color1 : Color0, float2 coords: TEXCOORD0) : SV_TARGET0
{
	float4 color = tex2D(s0, coords);
	if (color.a > 0)
	{
		if (coords.y < param1)
			color.rgb = 0;
		else
			color.rgb = 1;
	}
	/*color.rgb = 0;*/
		
	return color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}
