using UnityEngine;

namespace InventorySystem
{
    public abstract class InventoryItem : Item
    {
        [Header("Item Data")]
        [SerializeField]
        private Rarity rarity;

        [SerializeField]
        [Min(0)]
        private int price = 1;

        [SerializeField]
        [Min(1)]
        private int maxStack = 1;

        public override string ColoredName
        {
            get
            {
                string hexColor = ColorUtility.ToHtmlStringRGB(rarity.TextColor);
                return $"<color=#{hexColor}>{Name}</color>";
            }
        }

        public int Price => price;
        public int MaxStack => maxStack;
        public Rarity Rarity => rarity;
    }
}