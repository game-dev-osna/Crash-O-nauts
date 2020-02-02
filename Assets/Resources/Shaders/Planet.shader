Shader "Unlit/Planet"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _NoiseTex("Noise", 2D) = "white" {}
        _Noise2Tex("Noise2", 2D) = "white" {}
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvNoise : TEXCOORD1;
                float2 uvNoise2 : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;
            sampler2D _Noise2Tex;
            float4 _Noise2Tex_ST;

            fixed4 _ColorCore;
            fixed4 _ColorLava;
            fixed4 _ColorDirt;
            fixed4 _ColorStone;
            fixed4 _ColorGrass;

            float _RangeCore;
            float _RangeLava;
            float _RangeDirt;
            float _RangeStone;
            float _RangeGrass;

            float _FadeRange;
            float _Apocalypse;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uvNoise = TRANSFORM_TEX(v.uv, _NoiseTex);
                o.uvNoise2 = TRANSFORM_TEX(mul(unity_ObjectToWorld, v.vertex).yz, _Noise2Tex);
                return o;
            }

            fixed4 mixColors(fixed4 col1, fixed4 col2, float d)
            {
                return col1 * (1.0f - d) + col2 * d;
            }

            fixed4 mixRange(float pos, float rangeA, float rangeB, fixed4 colorA, fixed4 colorB)
            {
                float d = clamp((pos - rangeA) / (rangeB - rangeA), 0.0f, 1.0f);
                return mixColors(colorA, colorB, d);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float noise = tex2D(_NoiseTex, i.uvNoise - float2(_Apocalypse, _Apocalypse)).r;
                float worldNoise = tex2D(_Noise2Tex, i.uvNoise2).r;
                fixed4 col = fixed4(0, 0, 0, 0);
                float pos = max(i.uv.x - _Apocalypse, 0.0f);
                float fade = _Apocalypse * _Apocalypse + _FadeRange;

                col = mixRange(pos, _RangeCore, _RangeCore + fade, _ColorCore, _ColorLava);
                col = mixRange(pos, _RangeLava, _RangeLava + fade, col, _ColorDirt);
                col = mixRange(pos, _RangeDirt, _RangeGrass + fade, col, _ColorGrass);
                col = mixRange(pos, _RangeGrass, _RangeGrass + worldNoise, col, _ColorGrass);
                //col += mixRange(pos, _RangeGrass, _RangeStone, _ColorCore, _ColorLava);

                col = mixColors(col, _ColorStone * 0.1f, noise);
                col = mixColors(col, _ColorStone * 1.0f, worldNoise);

                //col = normalizeColor(col);

                col.a = 1.0f;

                return col;
            }
            ENDCG
        }
    }
}
