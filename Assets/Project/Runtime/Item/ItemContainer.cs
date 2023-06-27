using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;

namespace Item
{
    [Serializable]
    public class ItemContainer : IItemContainer
    {
        public Action OnItemsUpdated;
        
        private ItemSlot[] itemSlots = new ItemSlot[0];

        public ItemContainer(int size) => itemSlots = new ItemSlot[size];

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot AddItem(ItemSlot slot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == slot.item)
                    {
                        int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                        if (slot.quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].quantity += slot.quantity;

                            slot.quantity = 0;

                            OnItemsUpdated.Invoke();

                            return slot;
                        }

                        if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].quantity += slotRemainingSpace;

                            slot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }
            
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    if (slot.quantity <= slot.item.MaxStack)
                    {
                        itemSlots[i] = slot;

                        slot.quantity = 0;

                        OnItemsUpdated.Invoke();

                        return slot;
                    }

                    itemSlots[i] = new ItemSlot(slot.item, slot.item.MaxStack);

                    slot.quantity -= slot.item.MaxStack;
                }
            }

            OnItemsUpdated.Invoke();

            return slot;
        }

        public void RemoveItem(ItemSlot slot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == slot.item)
                    {
                        if (itemSlots[i].quantity < slot.quantity)
                        {
                            slot.quantity -= itemSlots[i].quantity;

                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            itemSlots[i].quantity -= slot.quantity;

                            if (itemSlots[i].quantity == 0)
                            {
                                itemSlots[i] = new ItemSlot();

                                OnItemsUpdated.Invoke();

                                return;
                            }
                        }
                    }
                }
            }
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > itemSlots.Length - 1)
            {
                return; 
            }

            itemSlots[slotIndex] = new ItemSlot();

            OnItemsUpdated.Invoke();
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
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].quantity += firstSlot.quantity;

                        itemSlots[indexOne] = new ItemSlot();

                        OnItemsUpdated.Invoke();

                        return;
                    }
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            OnItemsUpdated.Invoke();
        }

        public bool HasItem(InventoryItem item) => itemSlots.Any(itemSlot => itemSlot.item != null && itemSlot.item == item);

        public int GetTotalQuantity(InventoryItem item) => itemSlots.Where(itemSlot => itemSlot.item != null && itemSlot.item == item).Sum(itemSlot => itemSlot.quantity);
        
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
    }
}