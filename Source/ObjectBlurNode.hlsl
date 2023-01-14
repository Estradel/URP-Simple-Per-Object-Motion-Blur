#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"

void MyFunction_float(UnityTexture2D colorTex,float maxSamples, float2 UV,float2 velocity, out float4 result)
{
    // float speed = length(velocity * (1.0 / float2(1080,1920)));
    // float nSamples = clamp(int(speed), 1, maxSamples);
    float nSamples = maxSamples;
    
    // result = SAMPLE_TEXTURE2D(_BlitTexture , sampler_LinearClamp ,UV);
    result = SAMPLE_TEXTURE2D(colorTex.tex , colorTex.samplerstate ,UV);
    
    [unroll(100)] for (int i = 1; i < nSamples; ++i) {
        float2 offset = velocity * (float(i) / float(nSamples - 1) - 0.5);
        // result += SAMPLE_TEXTURE2D(_BlitTexture ,sampler_LinearClamp,UV+offset);
        result += SAMPLE_TEXTURE2D(colorTex.tex , colorTex.samplerstate,UV+offset);
        
    }
    result /= float(nSamples);
    // result = float4(result.rgb,1);
}
#endif
