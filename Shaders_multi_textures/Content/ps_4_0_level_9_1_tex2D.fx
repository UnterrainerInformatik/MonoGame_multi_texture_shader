texture FirstTexture;
sampler FirstSampler = sampler_state
{
	Texture = <FirstTexture>;
};

texture SecondTexture;
sampler SecondSampler = sampler_state
{
	Texture = <SecondTexture>;
};

float4 displace(float4 pos : SV_POSITION, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 v = tex2D(SecondSampler, texCoord);
	float2 offset = float2(v.r, v.g);
	float4 c = tex2D(FirstSampler, texCoord + offset);
	return c * color;
}

technique displacement
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_1 displace();
	}
}