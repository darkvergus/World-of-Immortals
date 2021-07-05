using Interactables;
using UnityEngine;

namespace InventorySystem
{
    public class ItemPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private ItemSlot itemSlot;

        public void Interact(GameObject other)
        {
            IItemContainer itemContainer = other.GetComponent<IItemContainer>();

            if (itemContainer == null) 
                return;

            if (itemContainer.AddItem(itemSlot).amount == 0)
                Destroy(gameObject);
        }
    }
}