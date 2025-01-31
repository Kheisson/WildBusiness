Shader "Custom/ScrollingBackground" {
    Properties {
        _MainTex ("Texture", 2D) = "white" { }
        _ScrollSpeed ("Scroll Speed", Float) = 0.1
        _CustomTime ("Custom Time", Float) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ScrollSpeed;
            float _CustomTime;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv.x += _CustomTime * _ScrollSpeed;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                return tex2D(_MainTex, frac(i.uv));
            }
            ENDCG
        }
    }
    Fallback "Transparent"
}