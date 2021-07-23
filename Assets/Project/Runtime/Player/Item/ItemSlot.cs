using System;

namespace InventorySystem
{
    [Serializable]
    public struct ItemSlot : IEquatable<ItemSlot>
    {
        private InventoryItem item;
        private int amount;

        public InventoryItem Item { get { return item; } set { item = value; } }
        public int Amount { get { return amount; } set { amount = value; } }

        public ItemSlot(InventoryItem item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public bool Equals(ItemSlot other) => other is ItemSlot otherS && Equals(otherS);
    }
}