using TMPro;
using UnityEngine;

namespace AI
{
    public class InteractionButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI interactionNameText = null;

        private IInteraction interaction = null;
        private GameObject other = null;

        public void Initialize(IInteraction interaction, GameObject other)
        {
            this.interaction = interaction;
            this.other = other;
        }

        public void TriggerInteraction() => interaction.Trigger(other);
    }
}