using Events;
using Item;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
    public class InventoryBase : ScriptableObject
    {
        [SerializeField] private VoidEvent OnInventoryItemsUpdated;
        [SerializeField] private ItemSlot testItemSlot = new ItemSlot();

        public ItemContainer ItemContainer { get; } = new ItemContainer(24);

        public void OnEnable() => ItemContainer.OnItemsUpdated += OnInventoryItemsUpdated.Raise;

        public void OnDisable() => ItemContainer.OnItemsUpdated -= OnInventoryItemsUpdated.Raise;

        [ContextMenu("Test Add")]
        public void TestAdd() => ItemContainer.AddItem(testItemSlot);
    }
}