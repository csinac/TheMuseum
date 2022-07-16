using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public class ProximityRayCast : SensorBase
    {
        public override float[] CurrentValue {
            get {
                return new float[] { 0 };
            }
        }
    }
}
