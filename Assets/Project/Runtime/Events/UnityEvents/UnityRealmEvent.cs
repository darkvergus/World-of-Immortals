using Realm;
using System;
using UnityEngine.Events;

namespace Events
{
    [Serializable] public class UnityRealmEvent : UnityEvent<RealmType> { }
}