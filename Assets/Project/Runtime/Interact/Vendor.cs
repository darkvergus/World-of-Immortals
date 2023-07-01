using UnityEngine;

namespace Interact
{
    public class Vendor : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt;

        public string Prompt => prompt;
        
        public bool Interact(Interactable interactable)
        {
            Debug.Log(prompt);
            return true;
        }
    }
}