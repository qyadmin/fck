Shader "Hidden/Open"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Pass
		{
			SetTexture[_MainTex]{
combine Texture
}
		}
	}
}
