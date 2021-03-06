sampler2D FirstSampler : register(s0);
sampler2D SecondSampler : register(s1);

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
		PixelShader = compile ps_2_0 displace();
	}
}