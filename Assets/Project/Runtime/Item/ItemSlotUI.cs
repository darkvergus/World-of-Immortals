using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Item
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        protected Image itemIconImage;

        public int SlotIndex { get; private set; }

        public abstract ItemBase SlotItem { get; }

        private void OnEnable() => UpdateSlotUI();

        protected virtual void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }

        public abstract void UpdateSlotUI();

        public abstract void OnDrop(PointerEventData eventData);

        protected virtual void EnableSlotUI(bool enable) => itemIconImage.enabled = enable;
    }
}