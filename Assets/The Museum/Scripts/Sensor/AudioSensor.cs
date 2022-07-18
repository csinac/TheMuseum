using System;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public class AudioSensor : SensorBase
    {
        private AudioSourceBase[] sources;
        private float[] volumes;

        private void Start() {
            FindAudioSources();
        }

        public void FindAudioSources() {
            sources = FindObjectsOfType<AudioSourceBase>();
            if (sources.Length == 0)
                volumes = new float[] {0};
            else
                volumes = new float[sources.Length + 1];
        }
        
        protected override float[] Read() {
            float sum = 0;
            for (int i = 0; i < sources.Length; i++) {
                volumes[i + 1] = sources[i].Volume;
                sum += sources[i].Volume;
            }

            volumes[0] = sum / sources.Length;
            
            return volumes;
        }
    }
}
