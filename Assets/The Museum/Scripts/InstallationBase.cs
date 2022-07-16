using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation {
    public abstract class InstallationBase : MonoBehaviour
    {
        [SerializeField] private InstallationInfo info;

        protected float movement;
        protected float proximity;
        protected float sound;

        protected abstract float SenseMovement { get; }
        protected abstract float SenseProximity { get; }
        protected abstract float SenseSound { get; }

        protected abstract void OnMovement(float value);
        protected abstract void OnProximity(float value);
        protected abstract void OnSound(float value);

        protected virtual void Start() {
            
        }

        protected virtual void Update() {
            movement = SenseMovement;
            proximity = SenseProximity;
            sound = SenseSound;
            
            OnMovement(movement);
            OnProximity(proximity);
            OnSound(sound);
        }
    }
}