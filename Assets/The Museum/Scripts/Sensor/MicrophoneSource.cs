using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneSource : MonoBehaviour
    {
        private AudioSource source;
        private float Volume;
        private int len = 10;
        private float t = 0;

        void Start() {
            if (Microphone.devices.Length == 0) {
                Destroy(this);
                return;
            }
            
            source = GetComponent<AudioSource>();
        }

        void Update() {
            float[] data = new float[64];
            source.GetOutputData(data, 0);
            List<float> ordered = new List<float>();
            foreach (float f in data) {
                ordered.Add(Mathf.Abs(f));
            }

            ordered.Sort();
            Volume = ordered[32];
//            if(Volume > 0.05f)
                Debug.Log(Volume * 1000);
            
            if (t <= 0) {
                t = len;
                StartCoroutine(RestartMic());
            }

            t -= Time.deltaTime;
        }

        IEnumerator RestartMic() {
            source.clip = Microphone.Start(null, false, len, AudioSettings.outputSampleRate);
            source.Play();
            Debug.Log("start mic");
            yield return null;
        }
    }
}