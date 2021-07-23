using TMPro;
using UnityEngine;

namespace AI
{
    public class InteractionButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI interactionNameText;

        private IInteraction interaction;
        private GameObject other;

        public void Initialize(IInteraction interaction, GameObject other)
        {
            this.interaction = interaction;
            this.other = other;
        }

        public void TriggerInteraction() => interaction.Trigger(other);
    }
}