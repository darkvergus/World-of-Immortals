using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GUIHeader : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private Sprite iconImage;

        [SerializeField]
        private TextMeshProUGUI headerText;

        [SerializeField]
        private string text;

        private void Awake()
        {
            icon.sprite = iconImage;
            headerText.text = text;
        }
    }
}
