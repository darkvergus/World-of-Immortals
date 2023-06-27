using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New String Event", menuName = "Events/String Event")]
    public class StringEvent : BaseGameEvent<string> { }
}