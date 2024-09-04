Shader "Custom/SimpleOutline"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,1)
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness("Outline Thickness", Range(0.01, 0.1)) = 0.03
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            Name "BASE"
            Cull Back
            ZWrite Off  // 투명도를 위해 ZWrite를 끔
            Blend SrcAlpha OneMinusSrcAlpha  // 알파 블렌딩 적용

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }

        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Cull Front
            ZWrite Off  // 투명도를 위해 ZWrite를 끔
            Blend SrcAlpha OneMinusSrcAlpha  // 알파 블렌딩 적용
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            uniform float _OutlineThickness;
            uniform float4 _OutlineColor;

            v2f vert(appdata v)
            {
                v.vertex.xyz += v.normal * _OutlineThickness;
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }

        Fallback "Transparent/Cutout/VertexLit"
}
