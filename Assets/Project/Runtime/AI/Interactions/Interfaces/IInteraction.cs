using UnityEngine;

namespace AI
{
    public interface IInteraction
    {
        string Name { get; }
        void Trigger(GameObject other);
    }
}