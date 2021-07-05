using UnityEngine;

namespace Interactables
{
    public class Interactor : MonoBehaviour
    {
        private IInteractable currentInteractable = null;

        [SerializeField]
        private KeyCode interactionKey = KeyCode.None;

        private void Update() => CheckForInteraction();

        private void CheckForInteraction()
        {
            if (currentInteractable == null)
                return;

            if (Input.GetKeyDown(interactionKey))
                currentInteractable.Interact(transform.root.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
                return;

            currentInteractable = interactable;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
                return;

            if (interactable != currentInteractable)
                return;

            currentInteractable = null;
        }
    }
}