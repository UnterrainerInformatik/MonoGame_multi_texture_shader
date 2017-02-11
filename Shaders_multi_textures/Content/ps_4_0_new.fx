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

float4 displace(float4 pos : SV_POSITION, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float4 v = SecondTexture.Sample(SecondSampler, texCoord);
	float2 offset = float2(v.r, v.g);
	float4 c = FirstTexture.Sample(FirstSampler, texCoord + offset);
	return c * color;
}

technique displacement
{
	pass P0
	{
		PixelShader = compile ps_4_0 displace();
	}
}