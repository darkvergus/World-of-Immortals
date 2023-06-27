using Item;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [SerializeField] private ItemDestroyer itemDestroyer;
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);

                if (eventData.hovered.Count == 0)
                {
                    InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                    if (thisSlot != null)
                    {
                        itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
                    }
                }
            }
        }
    }
}