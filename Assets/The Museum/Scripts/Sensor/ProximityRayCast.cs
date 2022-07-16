using System;
using UnityEngine;

namespace RectangleTrainer.MOIB.Sensor
{
    public class ProximityRayCast : SensorBase
    {
        [SerializeField, Min(1)] private int rayCount = 8;
        [SerializeField, Min(0.01f)] private float detectionDistance = 2;

        private Ray[] rays;

        public void Start() {
            InitializeRays();
        }

        private void OnValidate() {
            InitializeRays();
        }

        private void InitializeRays() {
            if (rayCount < 1)
                return;
            
            rays = new Ray[rayCount];
            for (int i = 0; i < rayCount; i++) {
                float rad = Mathf.PI * 2 / rayCount * i;
                
                float x = Mathf.Cos(rad);
                float z = Mathf.Sin(rad);

                rays[i] = new Ray(transform.position, new Vector3(x, 0, z));
            }
        }

        public override float CurrentValue {
            get {
                return 0;
            }
        }

        private void OnDrawGizmos() {
            if(rays == null)
                InitializeRays();
            
            Gizmos.color = Color.red;

            for (int i = 0; i < rayCount; i++) {
                Gizmos.DrawRay(rays[i].origin, rays[i].direction * detectionDistance);
            }
        }
    }
}
