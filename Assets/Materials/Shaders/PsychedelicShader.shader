Shader "Custom/PsychedelicShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Frequency ("Frequency", Float) = 10.0
        _Speed ("Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float _Frequency;
        float _Speed;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;
            float pattern = sin(uv.x * _Frequency + _Time.y * _Speed) * sin(uv.y * _Frequency + _Time.y * _Speed);
            o.Albedo = pattern;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
