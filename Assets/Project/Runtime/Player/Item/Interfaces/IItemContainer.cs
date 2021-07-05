using System.Collections.Generic;

namespace InventorySystem
{
    public interface IItemContainer
    {
        int SpiritStones { get; set; }

        ItemSlot GetSlotByIndex(int index);

        ItemSlot AddItem(ItemSlot item);

        List<InventoryItem> GetAllUniqueItems();

        void RemoveItem(ItemSlot item);

        void RemoveAt(int index);

        void Swap(int indexOne, int indexTwo);

        bool HasItem(InventoryItem item);

        int GetTotalAmount(InventoryItem item);
    }
}