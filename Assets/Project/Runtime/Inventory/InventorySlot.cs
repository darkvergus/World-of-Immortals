using Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [SerializeField]
        private InventoryBase inventory;

        [SerializeField]
        private TextMeshProUGUI itemQuantityText;

        public override ItemBase SlotItem => ItemSlot.item;

        public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);

        public override void UpdateSlotUI()
        {
            if (ItemSlot.item == null)
            {
                EnableSlotUI(false);
                return;
            }
            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.item.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null)
            {
                return;
            }

            if (itemDragHandler.ItemSlotUI as InventorySlot != null)
            {
                inventory.ItemContainer.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}