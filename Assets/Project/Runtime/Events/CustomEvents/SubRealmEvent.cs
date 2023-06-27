using Realm;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New SubRealm Event", menuName = "Events/SubRealm Event")]
    public class SubRealmEvent : BaseGameEvent<SubRealmType> { }
}