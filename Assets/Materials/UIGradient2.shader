Shader "Hidden/UIGradient2"
{
    Properties
    {
        //_MainTex ("Sprite Texture", 2D) = "white" {}
        _Color1 ("First Color", Color) = (1,1,1,1)
        _Color2 ("Second Color", Color) = (1,1,1,1)
        _Color3 ("Third Color", Color) = (1,1,1,1)
        _Bar1 ("Bar1", Range(-1.0, 1.0)) = 0
        _Bar2 ("Bar2", Range(-1.0, 1.0)) = 0
        _Spread1 ("Spread1", Range(0, 2.0)) = 1
        _Spread2 ("Spread2", Range(0, 2.0)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjectors"="True" "RenderType"="Transparent" }
        LOD 100  
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color1;
    		float4 _Color2;
    		float4 _Color3;
    		float _Bar1;
    		float _Bar2;
    		float _Spread1;
    		float _Spread2;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                return o;
            }

            //sampler2D _MainTex;
            
            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                float bar1 = (i.uv.x - _Bar1) * _Spread1;
                bar1 = clamp(bar1,0,1);
                float bar2 = (i.uv.x - _Bar2) * _Spread2;
                bar2 = clamp(bar2,0,1);
                fixed4 gradient1 = lerp(_Color1, _Color2, bar1);
                fixed4 gradient2 = lerp(gradient1, _Color3, bar2);
                //col = col * gradient2;
                return gradient2;
            }
            ENDCG
        }
    }
}
