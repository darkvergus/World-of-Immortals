using Inventory;
using TMPro;
using UnityEngine;

namespace Item
{
    public class ItemDestroyer : MonoBehaviour
    {
        [SerializeField] private InventoryBase inventory;
        [SerializeField] private TextMeshProUGUI areYouSureText;

        private int slotIndex;

        private void OnDisable() => slotIndex = -1;

        public void Activate(ItemSlot itemSlot, int slotIndex)
        {
            this.slotIndex = slotIndex;

            areYouSureText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColoredName}?";

            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            inventory.ItemContainer.RemoveAt(slotIndex);
            
            gameObject.SetActive(false);
        }
    }
}