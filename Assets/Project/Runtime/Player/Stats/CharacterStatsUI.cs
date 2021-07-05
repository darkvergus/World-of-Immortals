using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Stats
{
    public class CharacterStatsUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI characterName = null;

        [SerializeField]
        private TextMeshProUGUI characterAge = null;

        [SerializeField]
        private Image characterAvatar = null;

        [SerializeField]
        private TextMeshProUGUI characterRealm = null;

        [SerializeField]
        private TextMeshProUGUI characterStrength = null;

        [SerializeField]
        private TextMeshProUGUI characterAgility = null;

        [SerializeField]
        private TextMeshProUGUI characterDexterity = null;

        [SerializeField]
        private TextMeshProUGUI characterVitality = null;

        private void Awake()
        {
            SetInfo(PlayerInfo.Instance);
            SetStats(PlayerInfo.Instance);
        }

        public void UpdateInfo() => SetInfo(PlayerInfo.Instance);

        public void UpdateStats()
        {
            for (int i = 0; i < PlayerInfo.Instance.Stats.Count; i++)
            {
                SetStats(PlayerInfo.Instance);
            }
        }

        public void SetInfo(PlayerInfo player)
        {
            characterName.text = player.Name;
            characterAge.text = $"Age: {player.Age}";
            characterRealm.text = RealmUtils.GetCurrentRealm(player.realm, player.subRealm);
            characterAvatar.sprite = player.Avatar.sprite;
        }

        public void SetStats(PlayerInfo player)
        {
            for (int i = 0; i < player.Stats.Count; i++)
            {
                if (player.Stats[i].Name == "Strength")
                    characterStrength.text = player.Stats[i].CurrentPoints.ToString();

                if (player.Stats[i].Name == "Agility")
                    characterAgility.text = player.Stats[i].CurrentPoints.ToString();

                if (player.Stats[i].Name == "Dexterity")
                    characterDexterity.text = player.Stats[i].CurrentPoints.ToString();

                if (player.Stats[i].Name == "Vitality")
                    characterVitality.text = player.Stats[i].CurrentPoints.ToString();
            }
        }
    }
}