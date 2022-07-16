using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public abstract class SensorBase: MonoBehaviour
    {
        public abstract float CurrentValue { get; }
    }
}
