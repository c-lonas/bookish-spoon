Shader "Custom/ElevationBasedColor" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Elevation ("Elevation", Range(-10, 50)) = 0
        _Color1 ("Color1", Color) = (0, 0, 1, 0.5)
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
            float4 selectedColor;

            if (_Elevation <= 0) {
                selectedColor = _Color1;
            }
            else if (_Elevation > 0 && _Elevation <= 5) {
                selectedColor = _Color2;
            }
            else if (_Elevation > 5 && _Elevation <= 20) {
                selectedColor = _Color3;
            }
            else if (_Elevation > 20 && _Elevation <= 40) {
                selectedColor = _Color4;
            }
            else {
                selectedColor = _Color5;
            }
            
            o.Albedo = selectedColor.rgb;
            o.Alpha = selectedColor.a;
            o.Specular = 0.5;
            o.Gloss = 0.5;
        }
        ENDCG
    }
    FallBack "Diffuse"
}