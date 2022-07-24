using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RectangleTrainer.MOIB.Installation.Mimosa
{
    public class LeafController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationCurve trustMapping = AnimationCurve.Linear(0, 0, 1, 1);
        private float trust = 0.5f;

        private static readonly int trustParamID = Animator.StringToHash("trust");
        private int jiggleLayer;
        
        private void Start() {
            jiggleLayer = animator.GetLayerIndex("jigglejiggle");
            SetTrust(0.5f);
        }

        public void SetTrust(float trust) {
            this.trust = Mathf.Clamp(trust, 0f, 1f);
            
            animator.SetLayerWeight(jiggleLayer, Mathf.Clamp(this.trust - .25f, 0f, 1f) / 0.75f);
            animator.SetFloat(trustParamID, 1 - this.trust);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                SetTrust(trust + 0.05f);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                SetTrust(trust - 0.05f);
            }
        }
    }
}
