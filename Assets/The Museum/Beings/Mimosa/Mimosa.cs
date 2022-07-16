using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation
{
    public class Mimosa : InstallationBase
    {
        [SerializeField] private float movementThreshold = 0.3f;
        
        private float previousProximity;
        private float currentProximity;
        
        protected override void OnMovement(float[] values) {
            //TODO
        }

        protected override void OnProximity(float[] values) {
            int count = 0;
            float sum = 0;
            
            for (int i = 0; i < values.Length; i++) {
                if (values[i] <= 1) {
                    sum += values[i];
                    count++;
                }
            }

            previousProximity = currentProximity;
            
            if(count > 0)
                currentProximity = sum / count;
            else
                currentProximity = Single.PositiveInfinity;
        }

        protected override void OnAudio(float[] values) {
            //TODO
        }

        protected override void Update() {
            base.Update();
            
            ProximityControl();
        }

        private void ProximityControl() {
            if (currentProximity == Single.PositiveInfinity || previousProximity == Single.PositiveInfinity)
                return;
            
            float pDelta = (previousProximity - currentProximity) / Time.deltaTime;
            if(pDelta > movementThreshold)
                Log("#ffaa00", "Too fast");
        }
    }
}
