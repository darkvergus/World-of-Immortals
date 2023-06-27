using System;
using Realm;
using UnityEngine.Events;

namespace Events
{
    [Serializable] public class UnitySubRealmEvent : UnityEvent<SubRealmType> { }
}