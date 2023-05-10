Shader "Custom/ElevationBasedColor" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Elevation ("Elevation", Range(-10, 50)) = 0
        _Color1 ("Color1", Color) = (0, 0, 1, 0.3)
        _Color2 ("Color2", Color) = (1, 1, 0, 1)
        _Color3 ("Color3", Color) = (0, 1, 0, 1)
        _Color4 ("Color4", Color) = (0.5, 0.5, 0.5, 1)
        _Color5 ("Color5", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _Elevation;
        float4 _Color1;
        float4 _Color2;
        float4 _Color3;
        float4 _Color4;
        float4 _Color5;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);

            float t1 = _Elevation > 0 ? 1 : 0;
            float t2 = smoothstep(3, 22, _Elevation);
            float t3 = smoothstep(18, 42, _Elevation);
            float t4 = smoothstep(38, 50, _Elevation);

            float4 color = lerp(_Color1, _Color2, t1);
            color = lerp(color, _Color3, t2);
            color = lerp(color, _Color4, t3);
            color = lerp(color, _Color5, t4);

            o.Albedo = color.rgb;
            o.Alpha = color.a;
            o.Specular = 0.5;
            o.Gloss = 0.5;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
