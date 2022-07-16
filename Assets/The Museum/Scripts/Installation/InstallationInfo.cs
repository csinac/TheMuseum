using UnityEngine;

namespace RectangleTrainer.MOIB.Installation
{
    [CreateAssetMenu(fileName = "InstallationInfo", menuName = "The Museum/Installation Info")]
    public class InstallationInfo : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField, Multiline] private string info;

        public string Title => title;
        public string Info => info;
    }
}
