using System;
using System.Collections;
using System.Collections.Generic;
using RectangleTrainer.MOIB.Sensor;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation {
    public abstract class InstallationBase : MonoBehaviour
    {
        [SerializeField] private InstallationInfo info;
        [Space]
        [SerializeField] private SensorBase motionSensor;
        [SerializeField] private SensorBase audioSensor;
        [SerializeField] private SensorBase proximitySensor;
        public string Title => info.Title;
        public string Info => info.Info;

        protected float movement;
        protected float proximity;
        protected float audio;
        
        protected abstract void OnMovement(float value);
        protected abstract void OnProximity(float value);
        protected abstract void OnAudio(float value);

        protected virtual void Start() {
            
        }

        protected virtual void Update() {
            movement = motionSensor.CurrentValue;
            proximity = proximitySensor.CurrentValue;
            audio = audioSensor.CurrentValue;
            
            OnMovement(movement);
            OnProximity(proximity);
            OnAudio(audio);
        }
    }
}