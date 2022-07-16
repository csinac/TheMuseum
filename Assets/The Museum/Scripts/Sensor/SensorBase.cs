using System;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public abstract class SensorBase: MonoBehaviour
    {
        [SerializeField] private bool autoUpdate = true;
        protected float[] readings;

        public float[] CurrentValues => readings;

        protected virtual void Update() {
            if(autoUpdate)
                Read();
        }

        public abstract void Read();
    }
}
