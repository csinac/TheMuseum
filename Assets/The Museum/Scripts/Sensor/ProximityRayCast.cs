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
            Initialize();
        }

        private void OnValidate() {
            Initialize();
        }

        private void Initialize() {
            if (rayCount < 1)
                return;
            
            rays = new Ray[rayCount];
            readings = new float[rayCount];
            
            for (int i = 0; i < rayCount; i++) {
                float rad = Mathf.PI * 2 / rayCount * i;
                
                float x = Mathf.Cos(rad);
                float z = Mathf.Sin(rad);

                rays[i] = new Ray(transform.position, new Vector3(x, 0, z));
                readings[i] = float.PositiveInfinity;
            }
        }

        protected override float[] Read() {
            float[] values = new float[rayCount];
            
            for (int i = 0; i < rayCount; i++) {
                if (Physics.Raycast(rays[i], out RaycastHit hit, detectionDistance)) {
                    values[i] = Vector3.Distance(hit.point, transform.position);
                }
                else {
                    values[i] = float.PositiveInfinity;
                }
            }

            return values;
        }

        private void OnDrawGizmos() {
            if(rays == null)
                Initialize();
            
            Gizmos.color = Color.red;

            for (int i = 0; i < rayCount; i++) {
                Gizmos.DrawRay(rays[i].origin, rays[i].direction * detectionDistance);
                
                if(readings[i] <= detectionDistance)
                    Gizmos.DrawSphere(rays[i].origin + rays[i].direction * readings[i], 0.05f);
            }
        }
    }
}
