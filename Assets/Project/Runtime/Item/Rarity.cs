using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "Rarity", menuName = "Items/Rarity")]
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