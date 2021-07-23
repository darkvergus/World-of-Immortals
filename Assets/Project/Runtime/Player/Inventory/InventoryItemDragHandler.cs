using UnityEngine.EventSystems;

namespace InventorySystem
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);
            }
        }
    }
}