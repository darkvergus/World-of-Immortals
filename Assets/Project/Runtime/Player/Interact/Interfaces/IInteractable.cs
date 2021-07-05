using UnityEngine;

namespace Interactables
{
    public interface IInteractable
    {
        void Interact(GameObject other);
    }
}