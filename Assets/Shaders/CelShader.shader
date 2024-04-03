Shader "Unlit/ToonShader"
{
    Properties
    {
        _Brightness("Brightness", Range(0,1)) = 0.3
        _Strength("Strength", Range(0,1)) = 0.5
        _Color("Color", COLOR) = (1,1,1,1)
        _Detail("Detail", Range(0,1)) = 0.3
        _Alpha("Alpha", Range(0, 1)) = 1
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            //Pass for Toon shader
            Pass
            {
                Name "CelShading"
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase

                #include "UnityCG.cginc"
                #include "Lighting.cginc"
                #include "AutoLight.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    SHADOW_COORDS(1)
                    fixed3 diff : COLOR0;
                    fixed3 ambient : COLOR1;
                    float4 vertex : SV_POSITION;
                    half3 worldNormal: NORMAL;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Brightness;
                float _Strength;
                float4 _Color;
                float _Detail;

                float Toon(float3 normal, float3 lightDir) {
                    float NdotL = max(0.0,dot(normalize(normal), normalize(lightDir)));

                    return floor(NdotL / _Detail);
                }

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed shadow = SHADOW_ATTENUATION(i);
                    fixed3 lighting = i.diff * shadow + i.ambient;
                    col *= Toon(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness;
                    return col;
                }
                ENDCG
            }
                    Pass
        {
            Name "SHADOWS"

            Tags
            {
                "Queue" = "Geometry+1"
            }

            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                SHADOW_COORDS(0)
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW(o)
                return o;
            }

            float _Alpha;

            float4 frag(v2f i) : SV_Target
            {
                float shadow = SHADOW_ATTENUATION(i);

                return float4(0, 0, 0, (1 - shadow) * _Alpha);
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        }

}