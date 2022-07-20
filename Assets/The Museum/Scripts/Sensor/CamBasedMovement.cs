using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace RectangleTrainer.MOIB.Sensor
{
    public class CamBasedMovement : SensorBase
    {
        [SerializeField, Min(1)] private int camCount = 3;
        [SerializeField, Range(1, 179)] private int fov = 120;
        [SerializeField, Min(0.01f)] private float farPlane = 10f;
        [SerializeField, Min(0.01f)] private float nearPlane = 0.2f;
        [SerializeField] private Size size = Size._16x16;
        [SerializeField] private GraphicsFormat colorFormat = GraphicsFormat.R8_UInt;

        private int pxSize;
        private int pxLen;

        private Camera[] cams;
        private RenderTexture[] textures;

        private float[,] previousPixels;
        private float[,] currentPixels;
        
        private enum Size
        { _4x4, _8x8, _16x16, _32x32, _64x64, _128x128, _256x256 }

        private void Start() {
            Initialize();
            Read();
        }
        
        private void Initialize() {
            if (camCount < 1)
                return;
            
            pxSize = SizeConvert(size);
            pxLen = pxSize * pxSize;

            cams = new Camera[camCount];
            textures = new RenderTexture[camCount];
            currentPixels = new float[camCount, pxLen];
            previousPixels = new float[camCount, pxLen];
            readings = new float[camCount + 1];
            
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
                cams[i].fieldOfView = fov;
                cams[i].nearClipPlane = nearPlane;
                cams[i].farClipPlane = farPlane;
                cams[i].clearFlags = CameraClearFlags.SolidColor;
                cams[i].backgroundColor = Color.black;
                cams[i].enabled = false;

                textures[i] = new RenderTexture(pxSize, pxSize, 0, colorFormat);
                cams[i].targetTexture = textures[i];
            }
        }
        
        #if TESTING
        void OnGUI()
        {
            if (Application.isPlaying) {
                for (int i = 0; i < textures.Length; i++) {
                    GUI.DrawTexture(
                        new Rect(10 + 110 * i, 10, 100, 100),
                        textures[i],
                        ScaleMode.ScaleToFit, 
                        false,
                        1f);
                }
            }
        }
        #endif

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

        protected override float[] Read() {
            readings[0] = 0;
            
            for (int i = 0; i < cams.Length; i++) {
                cams[i].Render();
                var tex = new Texture2D(pxSize, pxSize);
                Rect rect = new Rect(0, 0, pxSize, pxSize);
                RenderTexture.active = textures[i];
                tex.ReadPixels(rect, 0, 0);
                tex.Apply();
                var pixels = tex.GetPixels(0, 0, pxSize, pxSize);
                readings[i + 1] = 0;

                for (int j = 0; j < pixels.Length; j++) {
                    previousPixels[i, j] = currentPixels[i, j];
                    currentPixels[i, j] = pixels[j].r;
                    if(currentPixels[i, j] != previousPixels[i, j])
                        readings[i + 1] ++;
                }

                readings[i + 1] /= pxLen;
                readings[0] += readings[i + 1];
            }

            readings[0] /= camCount;
            return readings;
        }
    }
}
