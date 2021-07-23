using Events;
using InventorySystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace AI
{
    public class VendorSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        [SerializeField]
        private Transform buttonHolderTransform;

        [SerializeField]
        private GameObject itemDataHolder;

        [Header("Item Data Display")]
        [SerializeField]
        private TextMeshProUGUI itemNameText;

        [SerializeField]
        private TextMeshProUGUI itemDescriptionText;

        [SerializeField]
        private TextMeshProUGUI itemInfoText;

        [Header("Quantity Display")]
        [SerializeField]
        private TextMeshProUGUI amountText;

        [SerializeField]
        private Slider amountSlider;

        private VendorData vendorData;
        private InventoryItem currentItem;

        private StringEvent onInsufficientSpiritStones;
        public StringEvent OnInsufficientSpiritStones { get { return onInsufficientSpiritStones; } set { onInsufficientSpiritStones = value; } }

        public void StartScenario(VendorData vendorData)
        {
            this.vendorData = vendorData;

            SetCurrentItemContainer(true);
            SetItem(vendorData.SellingItemContainer.GetSlotByIndex(0).Item);
        }

        public void SetCurrentItemContainer(bool isFirst)
        {
            ClearItemButtons();

            vendorData.IsFirstContainerBuying = isFirst;

            List<InventoryItem> items = vendorData.SellingItemContainer.GetAllUniqueItems();

            for (int i = 0; i < items.Count; i++)
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, buttonHolderTransform);
                buttonInstance.GetComponent<VendorItemButton>().Initialize(this, items[i], vendorData.SellingItemContainer.GetTotalAmount(items[i]));
            }
            itemDataHolder.SetActive(false);
        }

        public void SetItem(InventoryItem item)
        {
            currentItem = item;

            if (item == null)
            {
                itemNameText.text = string.Empty;
                itemDescriptionText.text = string.Empty;
                itemInfoText.text = string.Empty;
                return;
            }
            itemNameText.text = item.Name;
            itemDescriptionText.text = item.Description;
            itemInfoText.text = item.GetInfoDisplayText();

            int totalAmount = vendorData.SellingItemContainer.GetTotalAmount(item);

            amountText.text = $"0/{totalAmount}";
            amountSlider.maxValue = totalAmount;
            amountSlider.value = 0;

            itemDataHolder.SetActive(true);
        }

        public void UpdateSliderText(float amount)
        {
            int totalAmount = vendorData.SellingItemContainer.GetTotalAmount(currentItem);
            amountText.text = $"{amount}/{totalAmount}";
        }

        public void ConfirmButton()
        {
            if (amountSlider.value > 0)
            {
                int price = currentItem.Price * (int)amountSlider.value;

                if (vendorData.BuyingItemContainer.SpiritStones < price)
                {
                    string bodytext = $"You don't have enough SS to buy {RichTextUtils.Green(currentItem.Name)} x{(int)amountSlider.value}. It costs {currentItem.Price * (int)amountSlider.value} SS and you have {vendorData.BuyingItemContainer.SpiritStones} SS.";
                    OnInsufficientSpiritStones.Raise(bodytext);
                    return;
                }
            
                vendorData.BuyingItemContainer.SpiritStones -= price;
                vendorData.SellingItemContainer.SpiritStones += price;

                ItemSlot itemSlotSwap = new ItemSlot(currentItem, (int)amountSlider.value);
                bool soldAll = (int)amountSlider.value == vendorData.SellingItemContainer.GetTotalAmount(currentItem);

                if (soldAll)
                {
                    itemDataHolder.SetActive(false);
                }

                vendorData.BuyingItemContainer.AddItem(itemSlotSwap);
                vendorData.SellingItemContainer.RemoveItem(itemSlotSwap);

                SetCurrentItemContainer(vendorData.IsFirstContainerBuying);

                if (!soldAll)
                {
                    SetItem(currentItem);
                }
            }
        }

        private void ClearItemButtons()
        {
            foreach(Transform child in buttonHolderTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
