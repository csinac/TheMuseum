using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneSource : AudioSourceBase
    {
        private AudioSource source;
        private float volume;
        private float t = 0;

        private string device;
        
        public override float Volume => volume;
        
        void Start() {
            if (Microphone.devices.Length == 0) {
                Destroy(this);
                return;
            }

            device = Microphone.devices[0];
            Debug.Log(device);
            source = GetComponent<AudioSource>();
            source.clip = Microphone.Start(device, true, 1, AudioSettings.outputSampleRate);
            source.loop = true;
            while(!(Microphone.GetPosition(device) > 0)) {}
            source.Play();
        }

        void Update() {
            float[] data = new float[64];
            source.GetOutputData(data, 0);
            List<float> ordered = new List<float>();
            foreach (float f in data) {
                ordered.Add(Mathf.Abs(f));
            }

            ordered.Sort();
            volume = ordered[32];
        }
    }
}