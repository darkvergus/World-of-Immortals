using System.Collections.Generic;
using Inventory;

namespace Item
{
    public interface IItemContainer
    {
        ItemSlot AddItem(ItemSlot slot);
        void RemoveItem(ItemSlot slot);
        void RemoveAt(int slotIndex);
        void Swap(int indexOne, int indexTwo);
        bool HasItem(InventoryItem item);
        int GetTotalQuantity(InventoryItem item);
        List<InventoryItem> GetAllUniqueItems();
    }
}