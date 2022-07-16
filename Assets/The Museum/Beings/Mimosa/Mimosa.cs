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
            Debug.Log(string.Join(", ", values));
        }

        protected override void OnAudio(float[] values) {
            //TODO
        }
    }
}
