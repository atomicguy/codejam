Shader "Unlit/NeedlesGeometryShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Needle Color", Color) = (1,1,1,1) 
		_Length ("Needle Length", float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma geometry geo
			#pragma fragment frag

			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Length;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			[maxvertexcount(2)]
			void geo(triangle v2f p[3], inout LineStream<v2f> lines)
			{
				v2f start, end;
				start.normal = end.normal = float3(0,0,0);
				start.uv = end.uv = p[0].uv;

				start.vertex = (p[0].vertex + p[1].vertex + p[2].vertex) / 3;
				float3 needle = (p[0].normal + p[1].normal + p[2].normal) * _Length / 3;
				end.vertex = float4(start.vertex.xyz + needle, 1);


				lines.Append(start);
				lines.Append(end);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 modifier = normalize(i.vertex);
//				fixed4 col = tex2D(_MainTex, i.uv);
				return abs(_Color - modifier);
			}
			ENDCG
		}
	}
}
