namespace Interact
{
    public interface IInteractable
    {
        public string Prompt { get; }

        bool Interact(Interactable interactable);
    }
}