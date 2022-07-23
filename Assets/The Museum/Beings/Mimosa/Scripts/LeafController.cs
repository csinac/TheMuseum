using System.Collections.Generic;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation.Mimosa
{
    public class LeafController : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private List<Transform> stem;
        [SerializeField] private List<Transform> leavesL;
        [SerializeField] private List<Transform> leavesR;
        [Space]
        [SerializeField] private float undulationRange = 10;

        private float[] leafOffsetL;
        private float[] leafOffsetR;

        private Vector3[] leafInitL;
        private Vector3[] leafInitR;

        [ContextMenu("Populate Transforms")]
        private void PopulateTransforms() {
            stem = new List<Transform>();
            leavesL = new List<Transform>();
            leavesR = new List<Transform>();

            Transform[] transforms = GetComponentsInChildren<Transform>();
            foreach (var t in transforms) {
                if(t.name.Contains("LeafL"))
                    leavesL.Add(t);
                else if(t.name.Contains("LeafR"))
                    leavesR.Add(t);
                else if(t.name.Contains("Stem"))
                    stem.Add(t);
            }
        }

        private void Start() {
            InitializeWaveTimes();
        }

        private void InitializeWaveTimes() {
            leafOffsetL = new float[leavesL.Count];
            leafOffsetR = new float[leavesL.Count];

            leafInitL = new Vector3[leavesL.Count];
            leafInitR = new Vector3[leavesL.Count];

            for (int i = 0; i < leavesL.Count; i++) {
                leafOffsetL[i] = Random.Range(-Mathf.PI, Mathf.PI);
                leafOffsetR[i] = Random.Range(-Mathf.PI, Mathf.PI);

                leafInitL[i] = leavesL[i].localEulerAngles;
                leafInitR[i] = leavesR[i].localEulerAngles;
            }
        }

        private void LateUpdate() {
            RotateLeaves();
        }

        private void RotateLeaves() {
            for (int i = 0; i < leavesL.Count; i++) {
                leavesL[i].localEulerAngles = leafInitL[i] + new Vector3(0, Mathf.Cos(leafOffsetL[i] + Time.time) * undulationRange, 0);
                leavesR[i].localEulerAngles = leafInitR[i] + new Vector3(0, Mathf.Cos(leafOffsetR[i] + Time.time) * undulationRange, 0);
            }
        }
    }
}
