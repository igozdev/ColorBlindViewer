Shader "Hidden/ColorBlindFiltering"
{
    Properties
    {
        _MainTex ("", 2D) = "white" {}
        _Red ("", Color) = (1, 0, 0)
        _Green ("", Color) = (0, 1, 0)
        _Blue ("", Color) = (0, 0, 1)
    }
    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always
        Lighting Off
        Fog { Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Red;
            float4 _Green;
            float4 _Blue;

            fixed4 frag(v2f_img i) : COLOR
            {
                fixed3 col = tex2D(_MainTex, i.uv);
                
                return fixed4(
                    col.r * _Red.r + col.g * _Red.g + col.b * _Red.b,
                    col.r * _Green.r + col.g * _Green.g + col.b * _Green.b,
                    col.r * _Blue.r + col.g * _Blue.g + col.b * _Blue.b,
                    1);
            }
            ENDCG
        }
    }
}