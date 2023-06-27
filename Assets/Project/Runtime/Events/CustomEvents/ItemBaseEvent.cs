using Item;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New ItemBase Event", menuName = "Events/ItemBase Event")]
    public class ItemBaseEvent : BaseGameEvent<ItemBase> { }
}