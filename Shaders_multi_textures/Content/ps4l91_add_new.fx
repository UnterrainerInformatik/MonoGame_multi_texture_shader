Texture2D FirstTexture;
SamplerState FirstSampler
{
    Texture = <FirstTexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
};

Texture2D SecondTexture;
SamplerState SecondSampler
{
	Texture = <SecondTexture>; // Muss normalerweise nicht rein, aber Monogame hat sonst ein paar bugs.
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

float4 add(float4 pos : SV_POSITION, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 c = FirstTexture.Sample(FirstSampler, texCoord);
	float4 v = SecondTexture.Sample(SecondSampler, texCoord);
	return c/2.0f + v/2.0f;
}

technique additive
{
	pass P0
	{
		PixelShader = compile ps_4_0_level_9_1 add();
	}
}