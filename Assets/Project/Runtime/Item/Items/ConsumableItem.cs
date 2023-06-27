using System.Text;
using Inventory;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable item")]
    public class ConsumableItem : InventoryItem
    {
        [Header("Consumable Data")] 
        [SerializeField] private string useText = "Does something";
        
        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Rarity.Name).AppendLine();
            builder.Append("<color=green>Description: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(Price).Append(" Spirit Stones");
            
            return builder.ToString();
        }
    }
}