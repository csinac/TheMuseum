using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public class AudioSensor : SensorBase
    {
        public override float[] CurrentValue {
            get {
                return new float[] { 0 };
            }
        }
    }
}
