using System.Text;
using Inventory;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment")]
    public class EquipmentItem : InventoryItem
    {
        [Header("Consumable Data")] 
        [SerializeField] private string description = "Does something";
        
        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Rarity.name).AppendLine();
            builder.Append(description).AppendLine();
            builder.Append("Sell Price: ").Append(Price).Append(" Spirit Stones").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack);

            return builder.ToString();
        }
    }
}