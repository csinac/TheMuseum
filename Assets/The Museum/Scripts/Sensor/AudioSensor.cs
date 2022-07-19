using System;
using UnityEditor;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public class AudioSensor : SensorBase
    {
        [SerializeField] private float maxDistance = 2;
        [SerializeField] private AnimationCurve attenuation = AnimationCurve.Linear(0, 1, 1, 0);
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
                float distance = (transform.position - sources[i].transform.position).magnitude;
                if (distance < maxDistance) {
                    float normalized = distance / maxDistance;
                    float contribution = attenuation.Evaluate(normalized);
                    Debug.Log($"{normalized} {attenuation.Evaluate(normalized)}");
                    volumes[i + 1] = sources[i].Volume * contribution;
                    sum += volumes[i + 1];
                }
            }

            volumes[0] = sum / sources.Length;
            
            return volumes;
        }

        private void OnDrawGizmos() {
            Gizmos.color = new Color(0.67f, 0.53f, 1f);
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }
}
