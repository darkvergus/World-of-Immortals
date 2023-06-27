using System;
using Inventory;
using UnityEngine;

namespace Item
{
    [Serializable]
    public struct ItemSlot
    {
        public InventoryItem item;
        [Min(1)] public int quantity;

        public ItemSlot(InventoryItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
        
        public static bool operator ==(ItemSlot a, ItemSlot b) => a.Equals(b);
        public static bool operator !=(ItemSlot a, ItemSlot b) => !a.Equals(b);
    }
}