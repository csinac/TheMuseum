using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace RectangleTrainer.MOIB.Installation
{
    public class Mimosa : InstallationBase
    {
        [SerializeField] private float proximityChangeThreshold = .8f;
        [SerializeField] private Transform mesh;
        [SerializeField] private float trustCooldown = 10;
        [SerializeField] private float audioThreshold = .03f;
        [SerializeField] private float audioLevelMultiplier = 10;
        [SerializeField] private float motionThreshold = .005f;

        private float trustCooldownTime;
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
            if (values[0] > motionThreshold)
                Log("Things are moving too fast.", "#77ff88");
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
            
            if (currentProximity <= 1 && previousProximity > 1)
                trustCooldownTime = trustCooldown;
        }

        protected override void OnAudio(float[] values) {
            if (values[0] > audioThreshold) {
                float impact = Mathf.Clamp(values[0] * audioLevelMultiplier, 0, 100);
                Trust -= impact;
                Log($"Too loud! {impact}", "#aa88ff");
            }
        }

        protected override void Update() {
            base.Update();
            
            ProximityControl();
            UpdateTrust();
            VisualizeTrust();
        }

        private void UpdateTrust() {
            if (trustCooldownTime > 0 && currentProximity <= 1 && Trust >= 0)
                trustCooldownTime -= Time.deltaTime;
            
            if (movementDetected) {
                Log("Something's approaching too fast", "#ffaa00");
                movementDetected = false;
                trustCooldownTime = trustCooldown;
                Trust = Mathf.Clamp(Trust - proximityDelta / Mathf.Max(0.01f, currentProximity),-100, 100);
            }

            if (Trust < 0) {
                if(currentProximity > 1)
                    Trust += Time.deltaTime;
            }
            else {
                if (currentProximity < 1 && trustCooldownTime <= 0)
                    Trust += Time.deltaTime;
            }
        }

        private void VisualizeTrust() {
            mesh.localScale = Vector3.one * (Trust + 110) / 200;
        }

        private void ProximityControl() {
            if (currentProximity == Single.PositiveInfinity || previousProximity == Single.PositiveInfinity)
                return;
            
            proximityDelta = (previousProximity - currentProximity) / Time.deltaTime;
            movementDetected = proximityDelta > proximityChangeThreshold;
        }
    }
}
