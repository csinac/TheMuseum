using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation {
    public abstract class InstallationBase : MonoBehaviour
    {
        [SerializeField] private InstallationInfo info;

        protected abstract void SenseMovement();
        protected abstract void SenseProximity();
        protected abstract void SenseAudio();

        protected virtual void Start() {
            
        }

        protected virtual void Update() {
            SenseMovement();
            SenseProximity();
            SenseAudio();
        }
    }
}