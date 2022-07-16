using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace RectangleTrainer.MOIB.Installation
{
    public class Mimosa : InstallationBase
    {
        [SerializeField] private float movementThreshold = 0.3f;
        [SerializeField] private Transform mesh;

        private float _trust;
        private float Trust {
            get => _trust;
            set => _trust = Mathf.Clamp(value, -100, 100);
        }
        
        private float previousProximity;
        private float currentProximity;
        private float proximityDelta;

        private bool movementDetected = false;
        
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
            UpdateTrust();
            VisualizeTrust();
        }

        private void UpdateTrust() {
            if (movementDetected) {
                Log("Too fast", "#ffaa00");
                movementDetected = false;
                Trust = Mathf.Clamp(Trust - proximityDelta / Mathf.Max(0.01f, currentProximity),-100, 100);
            }

            if (Trust < 0 && currentProximity > 1) {
                Trust += Time.deltaTime;
            }
            
            Log(Trust);
        }

        private void VisualizeTrust() {
            mesh.localScale = Vector3.one * (Trust + 110) / 200;
        }

        private void ProximityControl() {
            if (currentProximity == Single.PositiveInfinity || previousProximity == Single.PositiveInfinity)
                return;
            
            proximityDelta = (previousProximity - currentProximity) / Time.deltaTime;
            movementDetected = proximityDelta > movementThreshold;
        }
    }
}
