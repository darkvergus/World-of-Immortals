using TMPro;
using UnityEngine;

namespace UI
{
    public class ModalDialogUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas modalCanvas;

        [SerializeField]
        private TextMeshProUGUI dialogText;


        public void SetDialog(string dialog)
        {
            modalCanvas.gameObject.SetActive(true);
            dialogText.text = dialog;
        }
    }
}
