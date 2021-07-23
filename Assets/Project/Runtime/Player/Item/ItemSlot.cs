using System;

namespace InventorySystem
{
    [Serializable]
    public struct ItemSlot : IEquatable<ItemSlot>
    {
        public InventoryItem item;
        public int amount;

        public ItemSlot(InventoryItem item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public bool Equals(ItemSlot other) => other is ItemSlot otherS && Equals(otherS);
    }
}