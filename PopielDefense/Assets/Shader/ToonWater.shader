Shader "Unlit/ToonWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Depth("Depth", Float) = 1.0
        _Strength("Strength", Range(0.0,2.0)) = 1.0
        _ShallowColor("Shallow Color", Color) = (0.70,0.92,1.00)
        _DeepColor("Deep Color", Color) = (0.00,0.45,0.62)
        _MainWaveNormal("Main Wave Normal", 2D) = "bump" {}
        _SubWaveNormal("Sub Wave Normal", 2D) = "bump" {}
        _MainWaveSpeed("Main Wave Speed", Float) = 50
        _SubWaveSpeed("Sub Wave Speed", Float) = -25
        _WaveStrength("Wave Strength", Float) = 1.0
        _Glossiness("Glossiness", Float) = 1.0
        _WaveNoise("Wave Noise", 2D) = "white" {}
        _WaveNoiseSpeed("Wave Speed", Float) = 20
        _WaveNoiseDisplacement("Wave Displacement", Float) = 1.0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 lightDir : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
                float4 screenPos : TEXCOORD4;
                float3 worldNormal : NORMAL;
                UNITY_FOG_COORDS(3)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _CameraDepthTexture;
            float4 _CameraDepthTexture_ST;

            float _Depth;
            float _Strength;

            fixed4 _ShallowColor;
            fixed4 _DeepColor;

            sampler2D _MainWaveNormal;
            float4 _MainWaveNormal_ST;
            sampler2D _SubWaveNormal;
            float4 _SubWaveNormal_ST;

            float _MainWaveSpeed;
            float _SubWaveSpeed;

            float _WaveStrength;

            float _Glossiness;

            sampler2D _WaveNoise;
            float4 _WaveNoise_ST;

            float _WaveNoiseSpeed;
            float _WaveNoiseDisplacement;

            v2f vert (appdata v)
            {
                v2f o;

                float2 noiseUV = TRANSFORM_TEX(v.uv, _WaveNoise);
                noiseUV += _Time / _WaveNoiseSpeed;
                float3 displacement = tex2Dlod(_WaveNoise, float4(noiseUV, 0,0));
                displacement *= _WaveNoiseDisplacement;
                v.vertex.g = displacement.g;


                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.vertex);
               // UNITY_TRANSFER_FOG(o,o.vertex);

                //calc tangents

                fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
                fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;

                float3x3 worldToTangent = float3x3(worldTangent, worldBinormal, worldNormal);

                o.lightDir = mul(worldToTangent, WorldSpaceLightDir(v.vertex));
                o.viewDir = mul(worldToTangent, WorldSpaceViewDir(v.vertex));
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }


            float sampleSceneDepth(float4 uv)
            {
                float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv);
                return Linear01Depth(rawDepth);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 c;
                float d = sampleSceneDepth(i.screenPos/i.screenPos.w);
                d *= _ProjectionParams.z;

                float screenPos = i.screenPos.a + _Depth;
                d -= screenPos;
                d *= _Strength;
                d = clamp(d, 0.0, 1.0);

                //set result, so we can achieve good looking things
                
                //lerp depth result to color values, creating mix of colors and transparency.
                fixed4 color = lerp(_ShallowColor, _DeepColor, d);

                //Calculate normals, seems to be off for some reason.
                float2 mainUV = TRANSFORM_TEX(i.uv, _MainWaveNormal);
                mainUV += _Time / _MainWaveSpeed;
                // Or mark the texture as "Normal map", and use the built-in funciton
                fixed3 tangentNormalMain = UnpackNormal(tex2D(_MainWaveNormal, mainUV));

                float2 subUV = TRANSFORM_TEX(i.uv, _SubWaveNormal);
                subUV += _Time / _SubWaveSpeed;
                fixed3 tangentNormalSub = UnpackNormal(tex2D(_SubWaveNormal, subUV));

                float3 tangentNormal = normalize(tangentNormalMain + tangentNormalSub);

                //Calculate normalStrength, Consider adding shallow wave strength.
                float normalStrength = lerp(0.0, _WaveStrength, d);

                tangentNormal.rg *= normalStrength;
                tangentNormal.b = lerp(1, tangentNormal.b, saturate(normalStrength));

                
                fixed3 tangentLightDir = normalize(i.lightDir);

                //Calculate diffuse part of light.
                float3 diffuse = color.rgb * max(dot(tangentLightDir, tangentNormal), 0.0);

                //Calculate specular part of lightning
                float3 tangentViewDir = normalize(i.viewDir);
                float3 reflectDir = reflect(-tangentLightDir, tangentNormal);
                float3 halfwayDir = normalize(tangentLightDir + tangentViewDir);
                float spec = pow(max(dot(tangentNormal, halfwayDir), 0.0), _Glossiness);

                float3 specular = float3(1.0, 1.0, 1.0) * spec;

                c = fixed4(diffuse + specular + ShadeSH9(half4(i.worldNormal, 1)), color.a);

                UNITY_APPLY_FOG(i.fogCoord, c);
                //UNITY_OPAQUE_ALPHA(c.a);

                return c;
            }
            ENDCG
        }
    }
}
