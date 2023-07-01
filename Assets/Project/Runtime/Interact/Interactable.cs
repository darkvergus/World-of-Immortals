using System;
using UI;
using UnityEngine;

namespace Interact
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private Transform source;
        [SerializeField] private float radius;
        [SerializeField] private ContactFilter2D mask;
        [SerializeField] private InteractionPromptUI ui;

        [SerializeField] private KeyCode key;

        private readonly Collider2D[] colliders = new Collider2D[3];
        [SerializeField] private int numFound;

        private IInteractable interactable;

        private void Update()
        {
            numFound = Physics2D.OverlapCircle(source.position, radius, mask, colliders);

            if (numFound > 0)
            {
                interactable = colliders[0].GetComponent<IInteractable>();

                if (interactable != null) 
                {
                    if (!ui.isDisplayed)
                    {
                        ui.Setup(interactable.Prompt);
                        if (Input.GetKeyDown(key))
                        {
                            interactable.Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactable != null)
                {
                    interactable = null;
                }

                if (ui.isDisplayed)
                {
                    ui.Close();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(source.position, radius);
        }
    }
}