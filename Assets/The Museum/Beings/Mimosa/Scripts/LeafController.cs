using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation.Mimosa
{
    public class LeafController : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private List<Transform> stem;
        [SerializeField] private List<Transform> leavesL;
        [SerializeField] private List<Transform> leavesR;

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
    }
}
