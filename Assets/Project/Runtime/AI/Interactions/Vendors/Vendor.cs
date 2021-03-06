using Events;
using InventorySystem;
using UnityEngine;

namespace AI
{
    public class Vendor : MonoBehaviour, IInteraction
    {
        [SerializeField]
        private VendorDataEvent OnStartVendor;

        public string Name => "Trader";

        private IItemContainer itemContainer;

        private void Start() => itemContainer = GetComponent<IItemContainer>();

        public void Trigger(GameObject other)
        {
            IItemContainer otherItemContainer = other.GetComponent<IItemContainer>();

            if (otherItemContainer == null)
            {
                return;
            }

            VendorData vendorData = new VendorData(otherItemContainer, itemContainer);

            OnStartVendor.Raise(vendorData);
        }
    }
}