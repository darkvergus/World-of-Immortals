using Realm;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "Realm Event", menuName = "Game Events/Realm Event")]
    public class RealmEvent : BaseGameEvent<RealmType> { }
}
