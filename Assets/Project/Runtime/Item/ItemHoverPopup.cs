﻿using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class ItemHoverPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvasObject;

        [SerializeField] private RectTransform popupObject;

        [SerializeField] private TextMeshProUGUI infoText;

        [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
        
        [SerializeField] private float padding = 25f;
        
        private Canvas popupCanvas;
        
        private void Start() => popupCanvas = popupCanvasObject.GetComponent<Canvas>();

        private void Update() => FollowCursor();
        
        public void HideInfo() => popupCanvasObject.SetActive(false);
        
        private void FollowCursor()
        {
            if (!popupCanvasObject.activeSelf)
            {
                return;
            }

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;

            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;

            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }

            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;

            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }

            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;

            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }

            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(ItemBase item)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<size=35>").Append(item.ColoredName).Append("</size>\n");

            builder.Append(item.GetInfoDisplayText());
            
            infoText.text = builder.ToString();

            popupCanvasObject.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }
    }
}