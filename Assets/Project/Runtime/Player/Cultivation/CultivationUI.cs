using Realm;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Cultivation
{
    public class CultivationUI : MonoBehaviour
    {
        //!TODO: Breakthrough has to be handle in a different place and not just a button on the screen
        [SerializeField]
        private TextMeshProUGUI breakthroughText;

        [SerializeField]
        private TextMeshProUGUI cultivationText;

        [SerializeField]
        private TextMeshProUGUI realmText;

        [SerializeField]
        private TextMeshProUGUI subRealmText;

        [SerializeField]
        private Slider cultivationTime;

        [SerializeField]
        private TextMeshProUGUI cultivationTimeText;

        public Slider CultivationTime => cultivationTime;
        public TextMeshProUGUI CultivationTimeText => cultivationTimeText;

        [SerializeField]
        private float animationTime = 1.2f;

        public void SetBreakthrough(int percent) => breakthroughText.text = $"Attempt Breakthrough: {percent} %";

        public void GetRealmFailure(RealmType type)
        {
            if(realmText)
            {
                realmText.gameObject.SetActive(true);
                realmText.text = RichTextUtils.Red($"Failed to increase realm to {RealmUtils.GetRealm(type)}");
                StartCoroutine(ShowAnimation(animationTime, realmText.gameObject));
            }
        }

        public void GetSubRealmFailure(SubRealmType type)
        {
            if (subRealmText)
            {
                subRealmText.gameObject.SetActive(true);
                subRealmText.text = RichTextUtils.Red($"Failed to increase subrealm to {RealmUtils.GetSubRealm(type)}");
                StartCoroutine(ShowAnimation(animationTime, subRealmText.gameObject));
            }
        }

        public void GetInfoDisplayText(int generateCB)
        {
            if (cultivationText)
            {
                cultivationText.gameObject.SetActive(true);
                cultivationText.text = RichTextUtils.Green(generateCB);
                StartCoroutine(ShowAnimation(animationTime, cultivationText.gameObject));
            }
        }

        IEnumerator ShowAnimation(float seconds, GameObject obj)
        {
            yield return new WaitForSeconds(seconds);
            obj.SetActive(false);
        }
    }
}