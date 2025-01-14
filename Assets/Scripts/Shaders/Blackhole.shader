Shader "Unlit/Blackhole" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskCenter ("Mask Center", Vector) = (0.5, 0.5, 0, 0)
        _MaskSize ("Mask Size", Vector) = (0.1, 0.1, 0, 0)
        _OverlayColor ("Overlay Color", Color) = (0, 0, 0, 0.5)
    }
    SubShader {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }
        Pass {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _OverlayColor;
            float4 _MaskCenter;
            float4 _MaskSize;

            fixed4 frag(v2f_img i) : SV_Target {
                float2 uv = i.uv;
                float2 maskHalfSize = _MaskSize.xy * 0.5;
                
                fixed4 col = _OverlayColor;

                // Check if pixel is inside the mask rectangular area
                if (uv.x > _MaskCenter.x - maskHalfSize.x &&
                    uv.x < _MaskCenter.x + maskHalfSize.x &&
                    uv.y > _MaskCenter.y - maskHalfSize.y &&
                    uv.y < _MaskCenter.y + maskHalfSize.y)
                {
                    col.a = 0.0;
                }

                return col;
            }
            ENDCG
        }
    }
}
