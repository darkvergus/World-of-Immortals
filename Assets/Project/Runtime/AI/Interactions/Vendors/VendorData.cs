using InventorySystem;

namespace AI
{
    public class VendorData
    {
        public IItemContainer BuyingItemContainer => IsFirstContainerBuying ? itemContainers[0] : itemContainers[1];
        public IItemContainer SellingItemContainer => IsFirstContainerBuying ? itemContainers[1] : itemContainers[0];

        private readonly IItemContainer[] itemContainers = new IItemContainer[2];

        public bool IsFirstContainerBuying { get; set; }

        public VendorData(IItemContainer buyingItemContainer, IItemContainer sellingItemContainer)
        {
            itemContainers[0] = buyingItemContainer;
            itemContainers[1] = sellingItemContainer;
        }
    }
}