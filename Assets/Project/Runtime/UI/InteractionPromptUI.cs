using TMPro;
using UnityEngine;

namespace UI
{
    public class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Camera mainCamera;
        
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private GameObject panel;

        public bool isDisplayed;

        private void Start()
        {
            mainCamera = Camera.main; 
            panel.SetActive(false);
        }

        private void Update()
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

            transform.position = screenPos;
        }

        public void Setup(string text)
        {
            promptText.text = text;
            panel.SetActive(true);
            isDisplayed = true;
        }

        public void Close()
        {
            isDisplayed = false;
            panel.SetActive(false);
        }
    }
}