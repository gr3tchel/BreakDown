Shader "Custom/ASCII Camera"
{
    Properties 
    {
        _MainTex("Source Image", 2D) = "" {}
        _Color("Color Tint", Color) = (1, 1, 1, 1)
        _Alpha("Alpha Blending", Float) = 1
        _Scale("Char Size", Float) = 1
    }

CGINCLUDE

#include "UnityCG.cginc"

sampler2D _MainTex;
float4 _MainTex_TexelSize;
float4 _Color;
float _Scale;
float _Alpha;

struct v2f
{
    float4 position : SV_POSITION;
    float2 texcoord : TEXCOORD0;
};   

float character(float n, float2 p)
{
    #ifdef UNITY_HALF_TEXEL_OFFSET
        float2 offs = float2(2.5f, 2.5f);
    #else
        float2 offs = float2(2, 2);
    #endif
    p = floor(p * float2(4, -4) + offs);
    if (clamp(p.x, 0, 4) == p.x && clamp(p.y, 0, 4) == p.y)
    {
        float c = fmod(n / exp2(p.x + 5 * p.y), 2);
        if (int(c) == 1) return 1;
    }   
    return 0;
}


float4 frag(v2f i) : SV_Target
{
    float2 texel = _MainTex_TexelSize.xy * _Scale;
    float2 uv = i.texcoord.xy / texel;
    float3 c = tex2D(_MainTex, floor(uv / 8) * 8 * texel).rgb;

    float gray = (c.r + c.g + c.b) / 3;
    float num_of_chars = 16. ;
    float n12, n34, n56, n78, n910, n1112, n1314, n1516;
    float n1234, n5678, n9101112, n13141516;
    float n12345678, n910111213141516;
    float n;

    if (gray < (1/num_of_chars )) {n12=0;                    } else {n12=4194304;               } //   or .
	if (gray < (3/num_of_chars )) {n34=131200;               } else {n34=324;                   } // : or ^
    if (gray < (5/num_of_chars )) {n56=330;                  } else {n56=283712;                } // " or ~
    if (gray < (7/num_of_chars )) {n78=12650880;             } else {n78=4532768;               } // c or v
    if (gray < (9/num_of_chars )) {n910=13191552;            } else {n910=10648704;             } // o or *
    if (gray < (11/num_of_chars)) {n1112=11195936;           } else {n1112=15218734;            } // w or S
    if (gray < (13/num_of_chars)) {n1314=15255086;           } else {n1314=15252014;            } // O or 8 
    if (gray < (15/num_of_chars)) {n1516=15324974;           } else {n1516=11512810;            } // 0 or # //forgot about Q
    if (gray < (2/num_of_chars )) {n1234=n12;                } else {n1234=n34;                 }
    if (gray < (6/num_of_chars )) {n5678=n56;                } else {n5678=n78;                 }
    if (gray < (10/num_of_chars)) {n9101112=n910;            } else {n9101112=n1112;            }
    if (gray < (14/num_of_chars)) {n13141516=n1314;          } else {n13141516=n1516;           }   
    if (gray < (4/num_of_chars )) {n12345678=n1234;          } else {n5678;                     }
    if (gray < (12/num_of_chars)) {n910111213141516=n9101112;} else {n910111213141516=n13141516;}
    if (gray < (8/num_of_chars )) {n=n12345678;              } else {n=n910111213141516;        }

    float2 p = fmod(uv / 4, 2) - 1;
    c *= character(n, p);

    float4 src = tex2D(_MainTex, i.texcoord.xy);
    return lerp(src, float4(c * _Color.rgb, _Color.a), _Alpha);
}

ENDCG

    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode off }
            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}