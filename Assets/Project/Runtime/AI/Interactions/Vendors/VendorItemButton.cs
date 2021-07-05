using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    public class VendorItemButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI itemNameText = null;

        [SerializeField]
        private Image itemIconImage = null;

        private VendorSystem vendorSystem = null;
        private InventoryItem item = null;

        public void Initialize(VendorSystem vendorSystem, InventoryItem item, int amount)
        {
            this.vendorSystem = vendorSystem;
            this.item = item;
            itemNameText.text = $"{item.Name} ({amount})";
            itemIconImage.sprite = item.Icon;
        }

        public void SelectItem() => vendorSystem.SetItem(item);
    }
}