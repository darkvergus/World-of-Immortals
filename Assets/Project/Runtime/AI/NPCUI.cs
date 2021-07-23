using TMPro;
using UnityEngine;

namespace AI
{
    public class NPCUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI npcNameText;

        [SerializeField]
        private TextMeshProUGUI npcGreetingText;

        [SerializeField]
        private Transform interactionButtonParent;

        [SerializeField]
        private GameObject interactionButtonPrefab;

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