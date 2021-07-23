using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] 
        private int spiritStones = 0;

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
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].amount;

                        if (itemSlot.amount <= slotRemainingSpace)
                        {
                            itemSlots[i].amount += itemSlot.amount;

                            itemSlot.amount = 0;

                            onInventoryItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].amount += slotRemainingSpace;

                            itemSlot.amount -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    if (itemSlot.amount <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.amount = 0;

                        onInventoryItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                        itemSlot.amount -= itemSlot.item.MaxStack;
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
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        if (itemSlots[i].amount < itemSlot.amount)
                        {
                            itemSlot.amount -= itemSlots[i].amount;

                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            itemSlots[i].amount -= itemSlot.amount;

                            if (itemSlots[i].amount == 0)
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
                if (itemSlots[i].item == null)
                {
                    continue;
                }

                if (items.Contains(itemSlots[i].item))
                {
                    continue;
                }

                items.Add(itemSlots[i].item);
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

            if (secondSlot.item != null)
            {
                if (firstSlot.item == secondSlot.item)
                {
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.amount;

                    if (firstSlot.amount <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].amount += firstSlot.amount;

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
                if (itemSlot.item == null || itemSlot.item != item)
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
                if (itemSlot.item == null || itemSlot.item != item)
                {
                    continue;
                }

                totalCount += itemSlot.amount;
            }

            return totalCount;
        }
    }
}