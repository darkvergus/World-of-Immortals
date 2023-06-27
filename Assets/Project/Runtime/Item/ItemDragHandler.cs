using Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Item
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ItemDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        protected ItemSlotUI itemSlotUI;

        [SerializeField]
        protected ItemBaseEvent OnMouseStartHoverItem;

        [SerializeField]
        protected VoidEvent OnMouseEndHoverItem;

        private CanvasGroup canvasGroup;
        private Transform originalParent;
        private bool isHovering;
        
        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private void Start() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnDisable()
        {
            if (isHovering)
            {
                OnMouseEndHoverItem.Raise();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnMouseEndHoverItem.Raise();

                originalParent = transform.parent;

                transform.SetParent(transform.parent.parent);

                canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseStartHoverItem.Raise(itemSlotUI.SlotItem);
            isHovering = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseEndHoverItem.Raise();
            isHovering = false;
        }
    }
}