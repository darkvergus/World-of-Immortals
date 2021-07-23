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
        private TextMeshProUGUI characterName;

        [SerializeField]
        private Image characterAvatar;

        [SerializeField]
        private TextMeshProUGUI characterRealm;

        [SerializeField]
        private TextMeshProUGUI characterStrength;

        [SerializeField]
        private TextMeshProUGUI characterAgility;

        [SerializeField]
        private TextMeshProUGUI characterDexterity;

        [SerializeField]
        private TextMeshProUGUI characterVitality;

        [SerializeField]
        private TextMeshProUGUI characterMinInternalForce;

        [SerializeField]
        private TextMeshProUGUI characterMaxInternalForce;

        [SerializeField]
        private TextMeshProUGUI characterMinPerception;

        [SerializeField]
        private TextMeshProUGUI characterMaxPerception;

        [SerializeField]
        private TextMeshProUGUI characterCultivatonBase;

        [SerializeField]
        private TextMeshProUGUI characterRequiredCultivationBase;

        private void Awake()
        {
            SetInfo(PlayerInfo.Instance);
            SetStats(PlayerInfo.Instance);
            SetSubStats(PlayerInfo.Instance);
        }

        public void UpdateInfo() => SetInfo(PlayerInfo.Instance);

        public void UpdateStats()
        {
            for (int i = 0; i < PlayerInfo.Instance.Stats.Count; i++)
            {
                SetStats(PlayerInfo.Instance);
            }
            for (int i = 0; i < PlayerInfo.Instance.SubStats.Count; i++)
            {
                SetSubStats(PlayerInfo.Instance);
            }
        }

        public void SetInfo(PlayerInfo player)
        {
            characterName.text = player.Name;
            characterRealm.text = RealmUtils.GetCurrentRealm(player.realm, player.SubRealm);
            characterAvatar.sprite = player.Avatar.sprite;
            characterCultivatonBase.text = player.Cultivation.CB.ToString();
            characterRequiredCultivationBase.text = player.RequiredExp.ToString();
        }

        public void SetSubStats(PlayerInfo player)
        {
            for (int i = 0; i < player.SubStats.Count; i++)
            {
                string current = player.SubStats[i].CurrentPoints.ToString();
                if (player.SubStats[i].Name == "MinInternalForce")
                {
                    characterMinInternalForce.text = current;
                }

                if (player.SubStats[i].Name == "MaxInternalForce")
                {
                    characterMaxInternalForce.text = current;
                }

                if (player.SubStats[i].Name == "MinPerception")
                {
                    characterMinPerception.text = current;
                }

                if (player.SubStats[i].Name == "MaxPerception")
                {
                    characterMaxPerception.text = current;
                }
            }
        }

        public void SetStats(PlayerInfo player)
        {
            for (int i = 0; i < player.Stats.Count; i++)
            {
                string current = player.Stats[i].CurrentPoints.ToString();
                if (player.Stats[i].Name == "Strength")
                {
                    characterStrength.text = current;
                }

                if (player.Stats[i].Name == "Agility")
                {
                    characterAgility.text = current;
                }

                if (player.Stats[i].Name == "Dexterity")
                {
                    characterDexterity.text = current;
                }

                if (player.Stats[i].Name == "Vitality")
                {
                    characterVitality.text = current;
                }
            }
        }
    }
}