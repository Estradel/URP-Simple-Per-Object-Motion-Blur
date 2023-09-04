#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"

void DoMotionBlur_float(UnityTexture2D colorTex,float maxSamples, float2 UV,float2 velocity, out float4 result)
{
    float nSamples = maxSamples;
    result = SAMPLE_TEXTURE2D(colorTex.tex , sampler_LinearClamp ,UV);
    
    [unroll(100)] for (int i = 1; i < nSamples; ++i) {
        float2 offset = velocity * (float(i) / float(nSamples - 1) - 0.5);
        result += SAMPLE_TEXTURE2D(colorTex.tex , sampler_LinearClamp,UV + offset);
        
    }
    result /= float(nSamples);
}