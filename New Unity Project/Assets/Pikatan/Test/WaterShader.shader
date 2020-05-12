Shader "Custom/WaterShader"
{
	Properties
	{
	  _Refract("法線マップ", 2D) = "" {}
	  _WaterLight("カラー（日向）", Color) = (0.2,0.8,0.9,1)
	  _WaterDark("カラー（暗部）", Color) = (0.4,0.6,0.7,1)
	  _HighColor("カラー（反射）", Color) = (1,1,1,1)
	  _High("反射強度", Range(0,10)) = 5
	  _Depth("ぼかし深度", Range(1,5)) = 2
	  _DepthMax("不透明度", Range(0,1)) = 0.9
	  _Distort("ディストーション", Range(0,1)) = 1
	  _WaveShift("スピード", Range(0,3)) = 1
	}

		SubShader
	  {
		Tags { "Queue" = "Transparent" }
		GrabPass { "_background" }
		Pass
		{
			Cull Off
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _CameraDepthTexture;
			sampler2D _background;
			sampler2D _Refract;
			float4 _HighColor;
			float4 _WaterLight;
			float4 _WaterDark;
			float _Depth;
			float _DepthMax;
			float _Distort;
			float _WaveShift;
			float _High;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 screenpos : TEXCOORD1;
				float2 uv : TEXCOORD0;
				float3 viewdir : FLOAT;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.screenpos = ComputeScreenPos(o.pos);
				o.uv = v.uv;

				o.viewdir = normalize(WorldSpaceViewDir(v.vertex));

				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				// スピード
				float2 waveshift = float2(_WaveShift, 0) * _Time;

				// ノーマルマップから屈折度を計算
				float3 n1 = UnpackNormal(tex2D(_Refract, i.uv + waveshift));
				float3 n2 = UnpackNormal(tex2D(_Refract, i.uv - waveshift));
				float3 normals = (n1 + n2) / 2;
				float2 refr = normals.xy * 0.2 * _Distort;

				// フレネル反射を計算
				float3 reflective = reflect(_WorldSpaceLightPos0, normals);
				float fresnel = -dot(i.viewdir, reflective) / 2 + 0.5;

				// スクリーン座標を計算（offset）
				float4 screen = float4(i.screenpos.xy + refr, i.screenpos.zw);

				// 深度計算（frag）
				float sceneZ = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(screen)));
				float fragZ = screen.z;

				// 水面下のオブジェクトをマスクアウト
				float mask = step(fragZ, sceneZ);
				float2 refrmasked = refr * mask;

				// スクリーン座標を計算（mask）
				float4 screen_masked = float4(i.screenpos.xy + refrmasked, i.screenpos.zw);

				// 深度計算（mask）
				float sceneZ_masked = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(screen_masked)));

				float depth = (sceneZ_masked - fragZ) / _Depth;
				if (depth > _DepthMax) depth = _DepthMax;

				// 屈折
				half4 background = tex2Dproj(_background, UNITY_PROJ_COORD(screen_masked));
				// カラー
				half4 watercolor = lerp(_WaterDark, _WaterLight, fresnel);
				// 深度
				half4 waterdepth = lerp(background, watercolor, depth);
				// 反射
				half4 water = lerp(waterdepth, _HighColor, pow(fresnel, 10 - _High)*_High);

				return water;
			}
			ENDCG
			}
	  }
}
