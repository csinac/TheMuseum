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

        protected float[] movement;
        protected float[] proximity;
        protected float[] audio;
        
        protected abstract void OnMovement(float[] values);
        protected abstract void OnProximity(float[] values);
        protected abstract void OnAudio(float[] values);

        protected virtual void Start() {
            
        }

        protected virtual void Update() {
            movement = motionSensor.CurrentValues;
            proximity = proximitySensor.CurrentValues;
            audio = audioSensor.CurrentValues;
            
            OnMovement(movement);
            OnProximity(proximity);
            OnAudio(audio);
        }

        protected void Log(object message, string color = null) {
            if(color == null)
                Debug.Log($"{Title}: {message}");
            else
                Debug.Log($"<color={color}>{Title}: {message}</color>");
        }
    }
}