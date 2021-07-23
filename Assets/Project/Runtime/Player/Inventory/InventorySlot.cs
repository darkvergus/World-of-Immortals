using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [SerializeField]
        private Inventory inventory;

        [SerializeField]
        private TextMeshProUGUI itemQuantityText;

        public override Item SlotItem
        {
            get => ItemSlot.Item;
            set { }
        }

        public ItemSlot ItemSlot => inventory.GetSlotByIndex(SlotIndex);

        public override void UpdateSlotUI()
        {
            if (ItemSlot.Item == null)
            {
                EnableSlotUI(false);
                return;
            }
            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.Item.Icon;
            itemQuantityText.text = ItemSlot.Amount > 1 ? ItemSlot.Amount.ToString() : "";
        }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null)
            {
                return;
            }

            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                inventory.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}