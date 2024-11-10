Shader "Unlit/DepthFX"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DepthTex ("Depth Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _DepthTex;
            float4 _MainTex_ST;

            float4 _MousePos;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the depth texture and take only the alpha channel
                float depthAlpha = tex2D(_DepthTex, i.uv).a;

                // Use the alpha as AAAA (swizzle to all channels)
                float4 depthSwizzled = float4(depthAlpha, depthAlpha, depthAlpha, depthAlpha);

                // Apply the depth swizzled value as a parallax multiplier
                float2 parallax = _MousePos.xy * depthSwizzled.r;
                fixed4 c = tex2D(_MainTex, i.uv + parallax);

                return c;
            }
            ENDCG
        }
    }
}
