using Events;
using Interactables;
using UnityEngine;

namespace AI
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private NPCEvent OnStartInteraction;

        [SerializeField]
        private new string name = "NPC name";

        [SerializeField]
        private string greetingText = "Greeting text";

        public string Name => name;
        public string GreetingText => greetingText;
        public GameObject OtherInteractor { get; private set; }
        public IInteraction[] Interactions { get; private set; } = new IInteraction[0];

        private void Start() => Interactions = GetComponents<IInteraction>();

        public void Interact(GameObject other)
        {
            OtherInteractor = other;

            OnStartInteraction.Raise(this);
        }
    }
}