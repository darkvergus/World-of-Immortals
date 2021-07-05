using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "InventorySystem/Item/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField]
        private new string name = "Rarity";

        [SerializeField]
        private Color textColor = new Color(1f, 1f, 1f, 1f);

        public string Name => name;
        public Color TextColor => textColor;
    }
}