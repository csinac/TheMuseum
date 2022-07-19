using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace RectangleTrainer.MOIB.Sensor
{
    public class CamBasedMovement : SensorBase
    {
        [SerializeField, Min(1)] private int camCount = 3;
        [SerializeField, Min(0.01f)] private float farPlane = 10f;
        [SerializeField, Min(0.01f)] private float nearPlane = 0.2f;
        [SerializeField] private Size size = Size._16x16;
        [SerializeField] private GraphicsFormat colorFormat = GraphicsFormat.R8_UInt;

        private Camera[] cams;
        private RenderTexture[] textures;

        private enum Size
        { _4x4, _8x8, _16x16, _32x32, _64x64, _128x128, _256x256 }

        private void Start() {
            Initialize();
        }
        
        private void Initialize() {
            if (camCount < 1)
                return;
            
            cams = new Camera[camCount];
            textures = new RenderTexture[camCount];
            
            int px = SizeConvert(size);
            for (int i = 0; i < camCount; i++) {
                float rad = Mathf.PI * 2 / camCount * i;
                
                float x = Mathf.Cos(rad);
                float z = Mathf.Sin(rad);

                GameObject camObj = new GameObject();
                camObj.transform.SetParent(transform);
                camObj.transform.localPosition = Vector3.zero;
                camObj.transform.LookAt(transform.position + new Vector3(x, 0, z));
                camObj.name = $"Cam.{i}";
                cams[i] = camObj.AddComponent<Camera>();
                cams[i].fieldOfView = 360 / camCount;
                cams[i].nearClipPlane = nearPlane;
                cams[i].farClipPlane = farPlane;

                textures[i] = new RenderTexture(px, px, 0, colorFormat);
                cams[i].targetTexture = textures[i];
            }
        }

        private int SizeConvert(Size size) {
            switch (size) {
                case Size._4x4: return 4;
                case Size._8x8: return 8;
                case Size._16x16: return 16;
                case Size._32x32: return 32;
                case Size._64x64: return 64;
                case Size._128x128: return 128;
            }

            return 256;
        }

        protected override void Update() {
            base.Update();
            
            for(int i = 0; i < cams.Length; i++)
                cams[i].Render();
        }

        protected override float[] Read() {
            return null;
        }
    }
}
