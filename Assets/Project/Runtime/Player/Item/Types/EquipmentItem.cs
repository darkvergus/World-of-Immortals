using System.Text;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Equipment Item", menuName = "InventorySystem/Item/Equipment")]
    public class EquipmentItem : InventoryItem
    {
        public override string GetInfoDisplayText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Rarity.name).AppendLine();
            sb.Append(Description).AppendLine();
            sb.Append("Sell Price: ").Append(Price).Append(" Spirit Stones").AppendLine();
            sb.Append("Max Stack: ").Append(MaxStack);

            return sb.ToString();
        }
    }
}