using UnityEngine;

namespace InventorySystem
{
    public abstract class Item : ScriptableObject
    {
        [Header("Info")]
        [SerializeField]
        private new string name = "Item Name";

        [SerializeField] 
        private string description = "Item Description";

        [SerializeField]
        private Sprite icon;

        public abstract string ColoredName { get; }
        public string Description => description;
        public string Name => name;
        public Sprite Icon => icon;

        public abstract string GetInfoDisplayText();
    }
}