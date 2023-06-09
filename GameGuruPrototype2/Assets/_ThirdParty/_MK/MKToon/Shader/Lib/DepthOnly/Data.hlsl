﻿//////////////////////////////////////////////////////
// MK Toon DepthOnly Data				       		//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2020 All rights reserved.            //
//////////////////////////////////////////////////////

#ifndef MK_TOON_DEPTH_ONLY_IO
	#define MK_TOON_DEPTH_ONLY_IO

	#include "../Core.hlsl"
	
	/////////////////////////////////////////////////////////////////////////////////////////////
	// INPUT
	/////////////////////////////////////////////////////////////////////////////////////////////
	struct VertexInputDepthOnly
	{
		float4 vertex : POSITION;
		#if defined(MK_VERTEX_ANIMATION_PULSE) || defined(MK_VERTEX_ANIMATION_NOISE) || defined(MK_PARALLAX)
			half3 normal : NORMAL;
		#endif
		#if defined(MK_VERTCLR) || defined(MK_POLYBRUSH)
			autoLP4 color : COLOR0;
		#endif
		#ifdef MK_TCM
			float2 texcoord0 : TEXCOORD0;
		#endif
		#if defined(MK_PARALLAX)
			half4 tangent : TANGENT;
		#endif

		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	/////////////////////////////////////////////////////////////////////////////////////////////
	// OUTPUT
	/////////////////////////////////////////////////////////////////////////////////////////////
	struct VertexOutputDepthOnly
	{
		float4 svPositionClip : SV_POSITION;
		#if defined(MK_VERTCLR) || defined(MK_POLYBRUSH)
			autoLP4 color : COLOR0;
		#endif
		#ifdef MK_TCM
			float2 uv : TEXCOORD0;
		#endif

		#if defined(MK_PARALLAX)
			half3 viewTangent : TEXCOORD1;
		#endif

		UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
	};
#endif