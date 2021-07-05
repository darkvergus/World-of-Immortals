using System.Text;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Consumable Item", menuName = "InventorySystem/Item/Consumable")]
    public class ConsumableItem : InventoryItem
    {
        [Header("Consumable Data")]
        [SerializeField]
        private string useText = "Does something";

        public override string GetInfoDisplayText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Rarity.name).AppendLine();
            sb.Append("<color=green>").Append(useText).Append("</color>").AppendLine();
            sb.Append("Max Stack: ").Append(MaxStack).AppendLine();
            sb.Append("Sell Price: ").Append(Price).Append(" Spirit Stones");

            return sb.ToString();
        }
    }
}