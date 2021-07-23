using TMPro;
using UnityEngine;

namespace UI
{
    public class GUIHeader : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI headerText;

        [SerializeField]
        private string text;

        private void Awake() => headerText.text = text;
    }
}
