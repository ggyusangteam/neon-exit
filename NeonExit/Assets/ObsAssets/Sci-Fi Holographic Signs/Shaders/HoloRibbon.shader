// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "MM/HoloRibbon"
{
	Properties
	{
		_ColorIntensity("Color Intensity", Range( 1 , 3)) = 1
		_Glitchspeed("Glitch speed", Range( 0 , 0.03)) = 0.01
		_Displacement("Displacement", 2D) = "black" {}
		_StripeTiling("Stripe Tiling", Vector) = (21,0,0,0)
		_AnimationSpeed("Animation Speed", Range( -2 , 4)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend One One , SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
		#endif//ASE Sampling Macros

		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		UNITY_DECLARE_TEX2D_NOSAMPLER(_Displacement);
		SamplerState sampler_Displacement;
		uniform float _Glitchspeed;
		uniform float _AnimationSpeed;
		uniform float2 _StripeTiling;
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
			float dotResult4_g9 = dot( float2( 0,10 ) , float2( 12.9898,78.233 ) );
			float lerpResult10_g9 = lerp( 0.0 , 2.0 , frac( ( sin( dotResult4_g9 ) * 43758.55 ) ));
			float temp_output_49_0 = lerpResult10_g9;
			float4 appendResult66 = (float4(temp_output_49_0 , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord57 = v.texcoord.xy + appendResult66.xy;
			float3 ase_vertexNormal = v.normal.xyz;
			float simplePerlin3D60 = snoise( ( ase_vertexNormal + ( ( temp_output_49_0 * _Time.y ) / 2.0 ) )*3.0 );
			float4 appendResult64 = (float4((float)0 , (float)0 , ( SAMPLE_TEXTURE2D_LOD( _Displacement, sampler_Displacement, uv_TexCoord57, 0.0 ) * _Glitchspeed * simplePerlin3D60 ).r , (float)0));
			float4 Offset65 = appendResult64;
			v.vertex.xyz += Offset65.xyz;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 temp_cast_0 = (_AnimationSpeed).xx;
			float2 uv_TexCoord34 = i.uv_texcoord * _StripeTiling;
			float2 panner36 = ( _Time.y * temp_cast_0 + uv_TexCoord34);
			float cos37 = cos( 0.0 * _Time.y );
			float sin37 = sin( 0.0 * _Time.y );
			float2 rotator37 = mul( panner36 - float2( 0,0 ) , float2x2( cos37 , -sin37 , sin37 , cos37 )) + float2( 0,0 );
			float cos39 = cos( _StripeTiling.y );
			float sin39 = sin( _StripeTiling.y );
			float2 rotator39 = mul( rotator37 - float2( 0,0 ) , float2x2( cos39 , -sin39 , sin39 , cos39 )) + float2( 0,0 );
			float2 appendResult10_g10 = (float2(10.64 , 0.24));
			float2 temp_output_11_0_g10 = ( abs( (frac( rotator39 )*2.0 + -1.0) ) - appendResult10_g10 );
			float2 break16_g10 = ( 1.0 - ( temp_output_11_0_g10 / fwidth( temp_output_11_0_g10 ) ) );
			float4 Stripes46 = ( ( 1.0 - saturate( min( break16_g10.x , break16_g10.y ) ) ) * _ColorIntensity * i.vertexColor * i.vertexColor.a );
			o.Emission = Stripes46.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18400
471;54;1182;585;2095.744;188.6077;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;33;-1771.37,-43.05639;Inherit;False;1612.29;315.101;Borders;15;47;46;45;42;41;39;38;37;36;35;34;68;69;72;73;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;72;-1758.744,149.3923;Inherit;False;Property;_StripeTiling;Stripe Tiling;4;0;Create;True;0;0;False;0;False;21,0;21,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;48;-1816.261,341.3106;Inherit;False;1669.338;904.5105;Displacement;18;66;65;64;63;62;61;60;59;58;57;56;55;54;53;52;51;50;49;;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;49;-1771.131,946.8458;Inherit;False;Random Range;-1;;9;7b754edb8aebbfb4a9ace907af661cfc;0;3;1;FLOAT2;0,10;False;2;FLOAT;0;False;3;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;50;-1767.249,1063.501;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-1576.744,198.3923;Inherit;False;Property;_AnimationSpeed;Animation Speed;5;0;Create;True;0;0;False;0;False;0;0;-2;4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;35;-1702.511,134.2837;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-1752.104,5.370605;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;21.7,4;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-1587.541,1043.132;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;36;-1527.075,6.734619;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.4;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;66;-1762.286,778.1389;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RotatorNode;37;-1348.352,9.112675;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;53;-1576.887,892.9007;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;52;-1464.616,1040.117;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-1332.653,960.2646;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RotatorNode;39;-1156.539,11.00867;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-1787.984,620.8752;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;54;-1604.039,1152.036;Inherit;False;Constant;_Noisesize;Noise size;7;0;Create;True;0;0;False;0;False;3;0.49;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;55;-1784.374,415.3595;Inherit;True;Property;_Displacement;Displacement;3;0;Create;True;0;0;False;0;False;6824678ccb0ef400ea97cf66d7bf0b56;6824678ccb0ef400ea97cf66d7bf0b56;False;black;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SamplerNode;58;-1468.616,512.8631;Inherit;True;Property;_TextureSample8;Texture Sample 8;9;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;60;-1199.902,963.7314;Inherit;False;Simplex3D;False;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-1447.986,723.8223;Inherit;False;Property;_Glitchspeed;Glitch speed;2;0;Create;True;0;0;False;0;False;0.01;0.01;0;0.03;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;41;-982.5663,9.437626;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;42;-865.1958,10.53064;Inherit;False;Rectangle;-1;;10;6b23e0c975270fb4084c354b2c83366a;0;3;1;FLOAT2;0,0;False;2;FLOAT;10.64;False;3;FLOAT;0.24;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-981.8107,718.525;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.IntNode;62;-749.5602,865.0704;Inherit;False;Constant;_Zero;Zero;3;0;Create;True;0;0;False;0;False;0;0;0;1;INT;0
Node;AmplifyShaderEditor.VertexColorNode;69;-539.3307,96.11858;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;45;-695.8487,10.02271;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-847.3443,131.9044;Inherit;False;Property;_ColorIntensity;Color Intensity;1;0;Create;True;0;0;False;0;False;1;0;1;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;63;-844.13,714.952;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-396.7874,10.1969;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;64;-562.1348,721.6099;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;46;-354.7909,163.4555;Inherit;False;Stripes;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;65;-346.4622,724.9185;Inherit;False;Offset;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;70;316.9724,27.10309;Inherit;False;46;Stripes;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;67;313.1387,240.5397;Inherit;False;65;Offset;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1309.777,128.2157;Inherit;False;Constant;_Angle;Angle;4;0;Create;True;0;0;False;0;False;0;4;-2;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;524.7985,-2.30103;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;MM/HoloRibbon;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;4;1;False;-1;1;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;34;0;72;0
WireConnection;51;0;49;0
WireConnection;51;1;50;0
WireConnection;36;0;34;0
WireConnection;36;2;73;0
WireConnection;36;1;35;0
WireConnection;66;0;49;0
WireConnection;37;0;36;0
WireConnection;52;0;51;0
WireConnection;56;0;53;0
WireConnection;56;1;52;0
WireConnection;39;0;37;0
WireConnection;39;2;72;2
WireConnection;57;1;66;0
WireConnection;58;0;55;0
WireConnection;58;1;57;0
WireConnection;60;0;56;0
WireConnection;60;1;54;0
WireConnection;41;0;39;0
WireConnection;42;1;41;0
WireConnection;61;0;58;0
WireConnection;61;1;59;0
WireConnection;61;2;60;0
WireConnection;45;0;42;0
WireConnection;63;0;61;0
WireConnection;47;0;45;0
WireConnection;47;1;68;0
WireConnection;47;2;69;0
WireConnection;47;3;69;4
WireConnection;64;0;62;0
WireConnection;64;1;62;0
WireConnection;64;2;63;0
WireConnection;64;3;62;0
WireConnection;46;0;47;0
WireConnection;65;0;64;0
WireConnection;0;2;70;0
WireConnection;0;11;67;0
ASEEND*/
//CHKSM=4B05737A1E38B0D22C6555254C1F0CF763C159BE