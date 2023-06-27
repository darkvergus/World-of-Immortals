using Realm;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Realm Event", menuName = "Events/Realm Event")]
    public class RealmEvent : BaseGameEvent<RealmType> { }
}