// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Progress" {

	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_Progress("Progress", Range(0.0,1.0)) = 0.0
		[Toggle] _isBending("RightCut", Float) = 0
	}

		SubShader{
			Tags {
			"Queue" = "Overlay+1"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
			Cull Off
			Lighting Off
			ZWrite Off
			ZTest Always
			Blend SrcAlpha OneMinusSrcAlpha
			Pass {

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile _ PIXELSNAP_ON
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform float4 _Color;
		uniform float _Progress;
		uniform bool _isBending;

		struct v2f {
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};

		v2f vert(appdata_base v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_UV(0);

			return o;
		}

		fixed4 SampleSpriteTexture(float2 uv)
		{
			fixed4 color = tex2D(_MainTex, uv);

			#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
			if (_AlphaSplitEnabled)
				color.a = tex2D(_AlphaTex, uv).r;
			#endif

			return color;
		}


		half4 frag(v2f i) : COLOR
		{
			half4 color = tex2D(_MainTex, i.uv);
			if (_isBending)
				color.a *= i.uv.x < _Progress;
			else color.a *= i.uv.y < _Progress;
			return color * _Color;
		}

		ENDCG

			}
		}

}