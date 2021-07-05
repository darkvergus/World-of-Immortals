using InventorySystem;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "Item Event", menuName = "Game Events/Item Event")]
    public class ItemEvent : BaseGameEvent<Item> { }
}
