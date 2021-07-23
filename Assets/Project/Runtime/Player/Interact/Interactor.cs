using UnityEngine;

namespace Interactables
{
    public class Interactor : MonoBehaviour
    {
        private IInteractable currentInteractable;

        [SerializeField]
        private KeyCode interactionKey = KeyCode.None;

        private bool isInteracting;

        private void Update() => CheckForInteraction();

        private void CheckForInteraction()
        {
            if (currentInteractable == null)
            {
                return;
            }

            if (Input.GetKeyDown(interactionKey) && !isInteracting)
            {
                isInteracting = true;
                currentInteractable.Interact(transform.root.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            currentInteractable = interactable;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            if (interactable != currentInteractable)
            {
                return;
            }

            StopInteracting();

            currentInteractable = null;
        }

        public void StopInteracting() => isInteracting = false;
    }
}