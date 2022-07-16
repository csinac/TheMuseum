using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation
{
    public class Mimosa : InstallationBase
    {
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
            
            if(count > 0)
                sum /= count;
            else
                sum = Single.PositiveInfinity;

            Debug.Log(sum);
        }

        protected override void OnAudio(float[] values) {
            //TODO
        }
    }
}
