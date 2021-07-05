using TMPro;
using UnityEngine;

namespace AI
{
    public class NPCUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI npcNameText = null;

        [SerializeField]
        private TextMeshProUGUI npcGreetingText = null;

        [SerializeField]
        private Transform interactionButtonParent = null;

        [SerializeField]
        private GameObject interactionButtonPrefab = null;

        public void SetNPC(NPC npc)
        {
            npcNameText.text = npc.Name;
            npcGreetingText.text = npc.GreetingText;

            foreach (Transform child in interactionButtonParent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < npc.Interactions.Length; i++)
            {
                GameObject buttonInstance = Instantiate(interactionButtonPrefab, interactionButtonParent);
                buttonInstance.GetComponent<InteractionButton>().Initialize(npc.Interactions[i], npc.OtherInteractor);
            }
        }
    }
}