// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MM/Holo"
{
	Properties
	{
		_BackgroundStripe("Background Stripe", Color) = (0.6586773,0.5896226,1,1)
		_BackgroundStripeSpeed("Background Stripe Speed", Range( 0 , 0.3)) = 0.1
		_BorderWidth("Border Width", Range( 0 , 1)) = 0.0836124
		_ColorIntensity("Color Intensity", Range( 1 , 3)) = 1
		_Dots("Dots", 2D) = "black" {}
		_Dotcolor("Dot color", Color) = (0.4150943,0.4150943,0.4150943,1)
		_TextTexture("Text Texture", 2D) = "black" {}
		_Details("Details", 2D) = "black" {}
		_DetailsColor("Details Color", Color) = (0.2783019,0.5532672,1,0.3686275)
		_Icon("Icon", 2D) = "black" {}
		_IconTiling("Icon Tiling", Vector) = (6,1,0,0)
		_IconOffsetLeft("Icon Offset Left", Vector) = (-0.1,0,0,0)
		_IconOffsetRight("Icon Offset Right", Vector) = (-0.1,0,0,0)
		_Iconscrollingspeed("Icon scrolling speed", Range( 0 , 0.3)) = 0.1
		_Displacement("Displacement", 2D) = "black" {}
		_Glitchspeed("Glitch speed", Range( 0 , 0.03)) = 0.01
		[Toggle]_TextAnimation("Text Animation", Float) = 0
		_TextAnimationSpeed("Text Animation Speed", Range( 1 , 10)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend One One , SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
		#endif//ASE Sampling Macros

		#pragma surface surf Unlit keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float3 worldPos;
		};

		UNITY_DECLARE_TEX2D_NOSAMPLER(_Displacement);
		uniform float4 _Displacement_ST;
		SamplerState sampler_Displacement;
		uniform float _Glitchspeed;
		uniform float4 _Dotcolor;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Dots);
		uniform float4 _Dots_ST;
		SamplerState sampler_Dots;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Details);
		uniform float4 _Details_ST;
		SamplerState sampler_Details;
		uniform float4 _DetailsColor;
		uniform float4 _BackgroundStripe;
		uniform float _BackgroundStripeSpeed;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_TextTexture);
		uniform float4 _TextTexture_ST;
		SamplerState sampler_TextTexture;
		uniform float _TextAnimationSpeed;
		uniform float _TextAnimation;
		uniform float _BorderWidth;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Icon);
		uniform float _Iconscrollingspeed;
		uniform float2 _IconTiling;
		uniform float2 _IconOffsetLeft;
		SamplerState sampler_Icon;
		uniform float2 _IconOffsetRight;
		uniform float _ColorIntensity;


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 uv_Displacement = v.texcoord.xy * _Displacement_ST.xy + _Displacement_ST.zw;
			float3 ase_vertexNormal = v.normal.xyz;
			float dotResult4_g11 = dot( float2( 0,6 ) , float2( 12.9898,78.233 ) );
			float lerpResult10_g11 = lerp( 0.0 , 6.0 , frac( ( sin( dotResult4_g11 ) * 43758.55 ) ));
			float temp_output_103_0 = lerpResult10_g11;
			float simplePerlin3D112 = snoise( ( ase_vertexNormal + ( ( temp_output_103_0 * _Time.y ) / 2.0 ) )*3.0 );
			float4 appendResult118 = (float4((float)0 , (float)0 , ( SAMPLE_TEXTURE2D_LOD( _Displacement, sampler_Displacement, uv_Displacement, 0.0 ) * _Glitchspeed * simplePerlin3D112 ).r , (float)0));
			float4 Offset121 = appendResult118;
			v.vertex.xyz += Offset121.xyz;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Dots = i.uv_texcoord * _Dots_ST.xy + _Dots_ST.zw;
			float2 panner86 = ( _Time.y * float2( 0,0.2 ) + uv_Dots);
			float4 Dots79 = ( _Dotcolor * _Dotcolor.a * SAMPLE_TEXTURE2D( _Dots, sampler_Dots, panner86 ) );
			float2 uv_Details = i.uv_texcoord * _Details_ST.xy + _Details_ST.zw;
			float2 panner129 = ( _Time.y * float2( 0.1,0 ) + uv_Details);
			float4 Details135 = ( SAMPLE_TEXTURE2D( _Details, sampler_Details, panner129 ) * _DetailsColor * i.vertexColor.a );
			float2 break19_g12 = float2( -0.5,-0.5 );
			float3 ase_worldPos = i.worldPos;
			float temp_output_1_0_g12 = ( ( ( _Time.y * _BackgroundStripeSpeed ) + ase_worldPos.y ) * 160.0 );
			float sinIn7_g12 = sin( temp_output_1_0_g12 );
			float sinInOffset6_g12 = sin( ( temp_output_1_0_g12 + 1.0 ) );
			float lerpResult20_g12 = lerp( break19_g12.x , break19_g12.y , frac( ( sin( ( ( sinIn7_g12 - sinInOffset6_g12 ) * 91.2228 ) ) * 43758.55 ) ));
			float4 Base168 = ( _BackgroundStripe * _BackgroundStripe.a * ( saturate( ( lerpResult20_g12 + sinIn7_g12 ) ) + saturate( ( pow( frac( ( ( ( _Time.y * ( _BackgroundStripeSpeed + 0.01 ) ) + ase_worldPos.y ) * 5.0 ) ) , 0.48 ) + -0.5 ) ) ) );
			float2 uv_TextTexture = i.uv_texcoord * _TextTexture_ST.xy + _TextTexture_ST.zw;
			float4 temp_cast_0 = (( ( ( 1.0 - abs( _CosTime.w ) ) * _TextAnimationSpeed ) + (( _TextAnimation )?( 0.0 ):( 100.0 )) )).xxxx;
			float4 blendOpSrc184 = SAMPLE_TEXTURE2D( _TextTexture, sampler_TextTexture, uv_TextTexture );
			float4 blendOpDest184 = temp_cast_0;
			float2 uv_TexCoord92 = i.uv_texcoord * float2( 21.7,4 );
			float2 panner125 = ( _Time.y * float2( 0,0.4 ) + uv_TexCoord92);
			float cos95 = cos( 0.0 * _Time.y );
			float sin95 = sin( 0.0 * _Time.y );
			float2 rotator95 = mul( panner125 - float2( 0,0 ) , float2x2( cos95 , -sin95 , sin95 , cos95 )) + float2( 0,0 );
			float cos101 = cos( 4.0 );
			float sin101 = sin( 4.0 );
			float2 rotator101 = mul( rotator95 - float2( 0,0 ) , float2x2( cos101 , -sin101 , sin101 , cos101 )) + float2( 0,0 );
			float2 appendResult10_g9 = (float2(10.64 , 0.24));
			float2 temp_output_11_0_g9 = ( abs( (frac( rotator101 )*2.0 + -1.0) ) - appendResult10_g9 );
			float2 break16_g9 = ( 1.0 - ( temp_output_11_0_g9 / fwidth( temp_output_11_0_g9 ) ) );
			float2 appendResult10_g10 = (float2(1.0 , ( 1.0 - _BorderWidth )));
			float2 temp_output_11_0_g10 = ( abs( (i.uv_texcoord*2.0 + -1.0) ) - appendResult10_g10 );
			float2 break16_g10 = ( 1.0 - ( temp_output_11_0_g10 / fwidth( temp_output_11_0_g10 ) ) );
			float TopBottomLines50 = ( ( 1.0 - saturate( min( break16_g9.x , break16_g9.y ) ) ) * ( 1.0 - saturate( min( break16_g10.x , break16_g10.y ) ) ) );
			float4 Text154 = ( ( ( saturate( min( blendOpSrc184 , blendOpDest184 ) )) + TopBottomLines50 ) * i.vertexColor * i.vertexColor.a );
			float4 appendResult139 = (float4(0.0 , _Iconscrollingspeed , 0.0 , 0.0));
			float2 uv_TexCoord142 = i.uv_texcoord * _IconTiling + _IconOffsetLeft;
			float2 panner144 = ( _Time.y * appendResult139.xy + uv_TexCoord142);
			float2 uv_TexCoord140 = i.uv_texcoord * _IconTiling + _IconOffsetRight;
			float2 panner143 = ( _Time.y * appendResult139.xy + uv_TexCoord140);
			float4 Icon151 = ( ( SAMPLE_TEXTURE2D( _Icon, sampler_Icon, panner144 ) + SAMPLE_TEXTURE2D( _Icon, sampler_Icon, panner143 ) ) * i.vertexColor * i.vertexColor.a );
			o.Emission = ( Dots79 + Details135 + ( ( Base168 + Text154 + Icon151 ) * _ColorIntensity ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18400
343;177;1316;852;1921.429;105.9269;1.238919;True;False
Node;AmplifyShaderEditor.CommentaryNode;163;-1361.752,-1580.581;Inherit;False;1595.02;355.6091;Borders;16;71;176;50;98;94;72;93;45;97;101;102;95;125;124;92;178;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;169;-1359.139,-2142.576;Inherit;False;1719.705;504.1368;Base;20;168;167;11;12;31;30;29;27;7;6;17;16;15;21;5;1;14;2;13;174;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;92;-1342.485,-1532.154;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;21.7,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;124;-1292.893,-1403.241;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1288.657,-1983.23;Inherit;False;Property;_BackgroundStripeSpeed;Background Stripe Speed;2;0;Create;True;0;0;False;0;False;0.1;0;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;125;-1126.456,-1531.79;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.4;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;2;-1188.467,-1907.451;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-1037.635,-1761.222;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;102;-901.1583,-1409.309;Inherit;False;Constant;_Angle;Angle;6;0;Create;True;0;0;False;0;False;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;95;-938.7332,-1529.766;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;166;-1355.693,-1197.512;Inherit;False;1688.772;429.722;Text;16;200;196;191;197;195;184;68;69;34;35;154;36;32;33;199;198;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;176;-958.307,-1313.301;Inherit;False;Property;_BorderWidth;Border Width;3;0;Create;True;0;0;False;0;False;0.0836124;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1;-924.4861,-1915.089;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-901.3208,-1760.679;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;101;-746.9208,-1526.516;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-755.6856,-1761.381;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;178;-663.2,-1289.009;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosTime;198;-1082.309,-972.0498;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FractNode;97;-572.9481,-1528.087;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;45;-662.3185,-1399.787;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;71;-449.4033,-1398.973;Inherit;False;Rectangle;-1;;10;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT;0.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-643.0421,-1755.593;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;158;-1363.688,1389.196;Inherit;False;1564.601;660.7722;Icon;18;148;151;150;149;138;137;140;141;139;156;147;145;143;144;142;175;203;204;;1,1,1,1;0;0
Node;AmplifyShaderEditor.AbsOpNode;199;-939.2003,-962.5795;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;93;-456.9322,-1526.994;Inherit;False;Rectangle;-1;;9;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;10.64;False;3;FLOAT;0.24;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-886.5894,-2071.888;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-748.386,-2067.526;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;204;-1347.524,1902.133;Inherit;False;Property;_IconOffsetRight;Icon Offset Right;13;0;Create;True;0;0;False;0;False;-0.1,0;-0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.OneMinusNode;200;-827.166,-965.687;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;72;-274.0048,-1401.599;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;165;-1368.714,-245.137;Inherit;False;1669.338;904.5105;Displacement;18;121;118;117;116;115;109;112;113;110;107;105;108;111;106;114;119;103;104;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;175;-1357.081,1609.387;Inherit;False;Property;_IconTiling;Icon Tiling;11;0;Create;True;0;0;False;0;False;6,1;6,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;137;-1218.921,1651.563;Inherit;False;Constant;_Scrollingspeed0;Scrolling speed 0;12;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;94;-286.2305,-1527.502;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;196;-928.4615,-849.8943;Inherit;False;Property;_TextAnimationSpeed;Text Animation Speed;18;0;Create;True;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;16;-518.8412,-1754.053;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;138;-1353.16,1735.726;Inherit;False;Property;_Iconscrollingspeed;Icon scrolling speed;14;0;Create;True;0;0;False;0;False;0.1;0;0;0.3;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;203;-1335.524,1454.133;Inherit;False;Property;_IconOffsetLeft;Icon Offset Left;12;0;Create;True;0;0;False;0;False;-0.1,0;-0.1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;-103.473,-1472.371;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;33;-1323.161,-959.6053;Inherit;False;0;32;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-629.2952,-2069.401;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;160;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;140;-1107.734,1904.972;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;6,1;False;1;FLOAT2;-4.9,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;191;-622.871,-873.7591;Inherit;False;Property;_TextAnimation;Text Animation;17;0;Create;True;0;0;False;0;False;0;2;0;FLOAT;100;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;139;-1032.607,1658.495;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleTimeNode;104;-1319.701,477.0537;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;141;-1068.673,1810.565;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;142;-1140.906,1444.09;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;6,1;False;1;FLOAT2;-0.1,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;32;-1338.122,-1153.711;Inherit;True;Property;_TextTexture;Text Texture;7;0;Create;True;0;0;False;0;False;52de902dc70e74c8a91527de967b73ae;c4d9cae8e4caf430382d2d5932f601b4;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;197;-632.1357,-1070.315;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;17;-394.1878,-1753.799;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0.48;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;103;-1323.584,360.3975;Inherit;False;Random Range;-1;;11;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,6;False;2;FLOAT;0;False;3;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;144;-832.8223,1652.732;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;143;-830.7512,1868.758;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;7;-499.669,-2069.812;Inherit;False;Noise Sine Wave;-1;;12;a6eff29f739ced848846e3b648af87bd;0;2;1;FLOAT;0;False;2;FLOAT2;-0.5,-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;36;-1110.005,-1154.23;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;105;-1139.993,456.6847;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;195;-493.7484,-1069.954;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-251.1777,-1752.012;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;35.65659,-1471.202;Inherit;False;TopBottomLines;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;145;-840.5029,1451.351;Inherit;True;Property;_Icon;Icon;10;0;Create;True;0;0;False;0;False;b2f0d410e71b143d882f958158712f86;None;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleDivideOpNode;108;-1017.069,453.6696;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;161;-1368.35,-751.0166;Inherit;False;1194.767;449.3293;Dots;9;79;91;89;90;85;83;84;86;87;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;159;-1379.244,712.2579;Inherit;False;1171.795;613.0923;Stripes;10;135;134;133;131;132;129;127;126;130;128;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;31;-137.9065,-1750.585;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;184;-353.3199,-1140.918;Inherit;False;Darken;True;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.NormalVertexDataNode;107;-1129.34,306.4524;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;156;-593.7813,1794.964;Inherit;True;Property;_TextureSample3;Texture Sample 3;13;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;147;-588.6454,1587.189;Inherit;True;Property;_TextureSample4;Texture Sample 4;5;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;68;-356.2712,-1015.954;Inherit;False;50;TopBottomLines;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;11;-306.2892,-2063.807;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;69;-145.9797,-1138.999;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;149;-266.9574,1733.171;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-1156.492,565.5883;Inherit;False;Constant;_Noisesize;Noise size;7;0;Create;True;0;0;False;0;False;3;0.49;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;34;-187.1301,-938.5326;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;128;-1348.009,965.1766;Inherit;False;0;130;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;148;-286.2002,1866.214;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;167;22.28112,-1915.768;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;111;-1339.115,34.42706;Inherit;False;0;119;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;127;-1302.259,1218.074;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;84;-1330.241,-674.5857;Inherit;False;0;87;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;12;-17.60949,-2092.768;Inherit;False;Property;_BackgroundStripe;Background Stripe;1;0;Create;True;0;0;False;0;False;0.6586773,0.5896226,1,1;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;126;-1300.465,1091.349;Inherit;False;Constant;_DetailsSpeed;Details Speed;5;0;Create;True;0;0;False;0;False;0.1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;83;-1277.691,-541.1548;Inherit;False;Constant;_DotPanner;Dot Panner;5;0;Create;True;0;0;False;0;False;0,0.2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;110;-885.1055,373.8164;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;119;-1336.826,-171.0882;Inherit;True;Property;_Displacement;Displacement;15;0;Create;True;0;0;False;0;False;6824678ccb0ef400ea97cf66d7bf0b56;6824678ccb0ef400ea97cf66d7bf0b56;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleTimeNode;85;-1287.961,-412.7817;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;150;-130.3637,1730.164;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;114;-1021.069,-73.58514;Inherit;True;Property;_TextureSample8;Texture Sample 8;9;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;20.41278,-1035.076;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;112;-752.355,377.2831;Inherit;False;Simplex3D;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;86;-1061.161,-480.3372;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;87;-1081.151,-686.1769;Inherit;True;Property;_Dots;Dots;5;0;Create;True;0;0;False;0;False;c08a999c2bb384a1793f9225a3154e61;None;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;174;152.148,-1919.066;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-1000.439,137.374;Inherit;False;Property;_Glitchspeed;Glitch speed;16;0;Create;True;0;0;False;0;False;0.01;0.0041;0;0.03;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;129;-1102.2,964.1644;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;130;-1347.827,767.4884;Inherit;True;Property;_Details;Details;8;0;Create;True;0;0;False;0;False;b0cc20c9266a34c57a3dd0670464741e;None;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RegisterLocalVarNode;151;0.3497772,1731.586;Inherit;False;Icon;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;168;278.0981,-1918.382;Inherit;False;Base;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;132;-890.9706,773.6871;Inherit;True;Property;_TextureSample5;Texture Sample 5;4;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;133;-838.8959,1153.716;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;89;-820.8947,-681.3796;Inherit;False;Property;_Dotcolor;Dot color;6;0;Create;True;0;0;False;0;False;0.4150943,0.4150943,0.4150943,1;0,0,0,0.3254902;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;-534.2635,132.0768;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;90;-818.4024,-515.1262;Inherit;True;Property;_TextureSample2;Texture Sample 2;3;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;131;-889.6925,978.1385;Inherit;False;Property;_DetailsColor;Details Color;9;0;Create;True;0;0;False;0;False;0.2783019,0.5532672,1,0.3686275;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;154;147.5965,-1034.395;Inherit;False;Text;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;712.7593,82.03372;Inherit;False;154;Text;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-505.207,-589.5801;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;153;708.6627,166.7827;Inherit;False;151;Icon;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.IntNode;117;-302.0128,278.6221;Inherit;False;Constant;_Zero;Zero;3;0;Create;True;0;0;False;0;False;0;0;0;1;INT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;116;-396.5826,128.5038;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;170;720.9639,-5.825749;Inherit;False;168;Base;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;134;-523.0745,964.6239;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;79;-376.8478,-590.8865;Inherit;False;Dots;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;135;-398.8033,964.0526;Inherit;False;Details;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;173;978.2274,-41.41175;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;172;921.0773,190.6272;Inherit;False;Property;_ColorIntensity;Color Intensity;4;0;Create;True;0;0;False;0;False;1;1;1;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;118;-114.5874,135.1616;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;82;1011.853,-270.7116;Inherit;False;79;Dots;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;1206.62,-13.55981;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;121;101.0853,138.4702;Inherit;False;Offset;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;136;1010.413,-191.2656;Inherit;False;135;Details;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;122;1310.605,55.56502;Inherit;False;121;Offset;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;106;-1314.738,191.6907;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;1323.224,-265.5966;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1558.404,-266.1772;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;MM/Holo;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;125;0;92;0
WireConnection;125;1;124;0
WireConnection;29;0;13;0
WireConnection;95;0;125;0
WireConnection;27;0;2;0
WireConnection;27;1;29;0
WireConnection;101;0;95;0
WireConnection;101;2;102;0
WireConnection;21;0;27;0
WireConnection;21;1;1;2
WireConnection;178;0;176;0
WireConnection;97;0;101;0
WireConnection;71;1;45;0
WireConnection;71;3;178;0
WireConnection;15;0;21;0
WireConnection;199;0;198;4
WireConnection;93;1;97;0
WireConnection;14;0;2;0
WireConnection;14;1;13;0
WireConnection;5;0;14;0
WireConnection;5;1;1;2
WireConnection;200;0;199;0
WireConnection;72;0;71;0
WireConnection;94;0;93;0
WireConnection;16;0;15;0
WireConnection;98;0;94;0
WireConnection;98;1;72;0
WireConnection;6;0;5;0
WireConnection;140;0;175;0
WireConnection;140;1;204;0
WireConnection;139;0;137;0
WireConnection;139;1;138;0
WireConnection;142;0;175;0
WireConnection;142;1;203;0
WireConnection;197;0;200;0
WireConnection;197;1;196;0
WireConnection;17;0;16;0
WireConnection;144;0;142;0
WireConnection;144;2;139;0
WireConnection;144;1;141;0
WireConnection;143;0;140;0
WireConnection;143;2;139;0
WireConnection;143;1;141;0
WireConnection;7;1;6;0
WireConnection;36;0;32;0
WireConnection;36;1;33;0
WireConnection;105;0;103;0
WireConnection;105;1;104;0
WireConnection;195;0;197;0
WireConnection;195;1;191;0
WireConnection;30;0;17;0
WireConnection;50;0;98;0
WireConnection;108;0;105;0
WireConnection;31;0;30;0
WireConnection;184;0;36;0
WireConnection;184;1;195;0
WireConnection;156;0;145;0
WireConnection;156;1;143;0
WireConnection;147;0;145;0
WireConnection;147;1;144;0
WireConnection;11;0;7;0
WireConnection;69;0;184;0
WireConnection;69;1;68;0
WireConnection;149;0;147;0
WireConnection;149;1;156;0
WireConnection;167;0;11;0
WireConnection;167;1;31;0
WireConnection;111;1;106;0
WireConnection;110;0;107;0
WireConnection;110;1;108;0
WireConnection;150;0;149;0
WireConnection;150;1;148;0
WireConnection;150;2;148;4
WireConnection;114;0;119;0
WireConnection;114;1;111;0
WireConnection;35;0;69;0
WireConnection;35;1;34;0
WireConnection;35;2;34;4
WireConnection;112;0;110;0
WireConnection;112;1;109;0
WireConnection;86;0;84;0
WireConnection;86;2;83;0
WireConnection;86;1;85;0
WireConnection;174;0;12;0
WireConnection;174;1;12;4
WireConnection;174;2;167;0
WireConnection;129;0;128;0
WireConnection;129;2;126;0
WireConnection;129;1;127;0
WireConnection;151;0;150;0
WireConnection;168;0;174;0
WireConnection;132;0;130;0
WireConnection;132;1;129;0
WireConnection;115;0;114;0
WireConnection;115;1;113;0
WireConnection;115;2;112;0
WireConnection;90;0;87;0
WireConnection;90;1;86;0
WireConnection;154;0;35;0
WireConnection;91;0;89;0
WireConnection;91;1;89;4
WireConnection;91;2;90;0
WireConnection;116;0;115;0
WireConnection;134;0;132;0
WireConnection;134;1;131;0
WireConnection;134;2;133;4
WireConnection;79;0;91;0
WireConnection;135;0;134;0
WireConnection;173;0;170;0
WireConnection;173;1;155;0
WireConnection;173;2;153;0
WireConnection;118;0;117;0
WireConnection;118;1;117;0
WireConnection;118;2;116;0
WireConnection;118;3;117;0
WireConnection;171;0;173;0
WireConnection;171;1;172;0
WireConnection;121;0;118;0
WireConnection;106;0;103;0
WireConnection;18;0;82;0
WireConnection;18;1;136;0
WireConnection;18;2;171;0
WireConnection;0;2;18;0
WireConnection;0;11;122;0
ASEEND*/
//CHKSM=3AFD9E7E9A6730BABF89FA8B432BE7B21773C71F