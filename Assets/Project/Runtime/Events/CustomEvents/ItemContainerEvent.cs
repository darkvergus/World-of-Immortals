using InventorySystem;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "Item Container Event", menuName = "Game Events/Item Container Event")]
    public class ItemContainerEvent : BaseGameEvent<IItemContainer> { }
}
