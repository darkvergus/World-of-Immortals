using System;
using Item;
using UnityEngine.Events;

namespace Events
{
    [Serializable] public class UnityItemBaseEvent : UnityEvent<ItemBase> { }
}