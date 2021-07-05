using InventorySystem;
using System;
using UnityEngine.Events;

namespace Events
{
    [Serializable] public class UnityItemContainerEvent : UnityEvent<IItemContainer> { }
}