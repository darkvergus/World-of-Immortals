using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] 
        private int spiritStones;

        [SerializeField]
        private UnityEvent onInventoryItemsUpdated;
        
        [SerializeField] 
        private ItemSlot[] itemSlots = new ItemSlot[0];
        public int SpiritStones { get { return spiritStones; } set { spiritStones = value; } }

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item != null)
                {
                    if (itemSlots[i].Item == itemSlot.Item)
                    {
                        int slotRemainingSpace = itemSlots[i].Item.MaxStack - itemSlots[i].Amount;

                        if (itemSlot.Amount <= slotRemainingSpace)
                        {
                            itemSlots[i].Amount += itemSlot.Amount;

                            itemSlot.Amount = 0;

                            onInventoryItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].Amount += slotRemainingSpace;

                            itemSlot.Amount -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    if (itemSlot.Amount <= itemSlot.Item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.Amount = 0;

                        onInventoryItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.Item, itemSlot.Item.MaxStack);

                        itemSlot.Amount -= itemSlot.Item.MaxStack;
                    }
                }
            }

            onInventoryItemsUpdated.Invoke();

            return itemSlot;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item != null)
                {
                    if (itemSlots[i].Item == itemSlot.Item)
                    {
                        if (itemSlots[i].Amount < itemSlot.Amount)
                        {
                            itemSlot.Amount -= itemSlots[i].Amount;

                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            itemSlots[i].Amount -= itemSlot.Amount;

                            if (itemSlots[i].Amount == 0)
                            {
                                itemSlots[i] = new ItemSlot();

                                onInventoryItemsUpdated.Invoke();

                                return;
                            }
                        }
                    }
                }
            }
        }

        public List<InventoryItem> GetAllUniqueItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    continue;
                }

                if (items.Contains(itemSlots[i].Item))
                {
                    continue;
                }

                items.Add(itemSlots[i].Item);
            }

            return items;
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > itemSlots.Length - 1)
            {
                return; 
            }

            itemSlots[slotIndex] = new ItemSlot();

            onInventoryItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot.Equals(secondSlot))
            {
                return;
            }

            if (secondSlot.Item != null)
            {
                if (firstSlot.Item == secondSlot.Item)
                {
                    int secondSlotRemainingSpace = secondSlot.Item.MaxStack - secondSlot.Amount;

                    if (firstSlot.Amount <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].Amount += firstSlot.Amount;

                        itemSlots[indexOne] = new ItemSlot();

                        onInventoryItemsUpdated.Invoke();

                        return;
                    }
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            onInventoryItemsUpdated.Invoke();
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Item == null || itemSlot.Item != item)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public int GetTotalAmount(InventoryItem item)
        {
            int totalCount = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.Item == null || itemSlot.Item != item)
                {
                    continue;
                }

                totalCount += itemSlot.Amount;
            }

            return totalCount;
        }
    }
}