using System;
using TMPro;
using UnityEngine;

namespace RectangleTrainer.MOIB.Installation
{
    public class Pedestal : MonoBehaviour
    {
        [SerializeField] private InstallationBase installation;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI infoText;

        private void Start() {
            titleText.text = installation.Title;
            infoText.text = installation.Info;
        }
    }
}
