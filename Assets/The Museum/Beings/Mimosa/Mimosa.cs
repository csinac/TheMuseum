using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation
{
    public class Mimosa : InstallationBase
    {
        protected override float SenseMovement {
            get {
                return 0;
            }
        }

        protected override float SenseProximity {
            get {
                return 0;
            }
        }

        protected override float SenseSound {
            get {
                return 0;
            }
        }

        protected override void OnMovement(float value) {
            //TODO
        }

        protected override void OnProximity(float value) {
            //TODO
        }

        protected override void OnSound(float value) {
            //TODO
        }
    }
}
