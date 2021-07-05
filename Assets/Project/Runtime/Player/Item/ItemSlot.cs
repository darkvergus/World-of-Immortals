using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public struct ItemSlot
    {
        public InventoryItem item;
        public int amount;

        public ItemSlot(InventoryItem item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }
}