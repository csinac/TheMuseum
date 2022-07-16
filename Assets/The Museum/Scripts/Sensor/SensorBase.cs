using System;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public abstract class SensorBase: MonoBehaviour
    {
        [SerializeField] private bool autoUpdate = true;
        protected float[] readings;

        public float[] CurrentValues {
            get {
                if (!autoUpdate)
                    UpdateReadings();

                return readings;
            }
        }

        protected virtual void Update() {
            if(autoUpdate)
                UpdateReadings();
        }

        private void UpdateReadings() {
            readings = Read();
        }

        protected abstract float[] Read();
    }
}
