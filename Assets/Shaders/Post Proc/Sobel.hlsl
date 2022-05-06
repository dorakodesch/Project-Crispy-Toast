#ifndef SOBEL_INCLUDED
#define SOBEL_INCLUDED

int width;
int height;

// sobel function
void Sobel_float(UnityTexture2D input, float width, float height, float2 UV, out float output)
{
	// sobel sample confirmed good greyscale values
	float2 sobel = float2(0, 0);

	//test matrix
	float greyscale[9];

	for (int i = 0; i < 9; i++)
	{
		float2 offset = float2(floor(i / 3) - 1, i % 3 - 1);
		float2 dims = float2(width, height);
		float3 rgb = input.Sample(sampler_MainTex, UV + offset / dims);
		greyscale[i] = (rgb.r + rgb.g + rgb.b) / 3;
	}

	sobel += greyscale[0] * float2(-1, -1);
	sobel += greyscale[1] * float2(0, -2);
	sobel += greyscale[2] * float2(1, -1);
	sobel += greyscale[3] * float2(-2, 0);
	sobel += greyscale[5] * float2(2, 0);
	sobel += greyscale[6] * float2(-1, 1);
	sobel += greyscale[7] * float2(0, 2);
	sobel += greyscale[8] * float2(1, 1);

	output = length(sobel);
}
#endif