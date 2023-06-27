using Realm;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "Body Event", menuName = "Events/Body Event")]
    public class BodyEvent : BaseGameEvent<BodyType> { }
}