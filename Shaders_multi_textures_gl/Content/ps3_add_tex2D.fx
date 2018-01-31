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

float4 add(float4 pos : SV_POSITION, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 c = tex2D(FirstSampler, texCoord);
	float4 v = tex2D(SecondSampler, texCoord);
	return c/2.0f + v/2.0f;
}

technique additive
{
	pass P0
	{
		PixelShader = compile ps_3_0 add();
	}
}