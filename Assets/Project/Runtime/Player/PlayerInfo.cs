using Cultivation;
using Events;
using Realm;
using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerInfo : SingletonManager<PlayerInfo>
    {
        [Header("Basic Info")]
        [SerializeField]
        private new string name = "";

        [SerializeField]
        private int age = 0;

        [SerializeField]
        private int ageDuration = 0;

        [SerializeField]
        private Image avatar = null;

        public RealmType realm = RealmType.QICONDENSATION;

        public SubRealmType subRealm = SubRealmType.ONE;

        public BodyType body = BodyType.BONEREFINING;

        public SubRealmType subBodyRealm = SubRealmType.ONE;

        [SerializeField]
        private CultivationBase cultivation;

        [SerializeField]
        private CultivationUI cultivationUI;

        private float percentNeeded = 0;
        public float percentSuccess = 0;

        [SerializeField]
        [Min(0)]
        private double requiredExp = 0;

        [SerializeField]
        private List<Stat> stats;

        [SerializeField]
        private List<Stat> subStats;

        public IntEvent OnPercentUpdate;
        public RealmEvent OnRealmUpgrade;
        public RealmEvent OnRealmUpgradeFailure;
        public SubRealmEvent OnSubRealmUpgrade;
        public SubRealmEvent OnSubRealmUpgradeFailure;
        public IntEvent OnAgeIncrease;

        public string Name => name;
        public int Age => age;
        public Image Avatar => avatar;
        public CultivationBase Cultivation => cultivation;
        public float PercentNeeded => percentNeeded;
        public double RequiredExp => requiredExp;

        public List<Stat> Stats => stats;
        public List<Stat> SubStats => subStats;

        private float minCB;
        private float maxCB;

        private Timer cultivationTimer;

        private void Awake()
        {
            RequiredExpToBreakthrough();
            SetInternalForce();
            cultivationTimer = Timer.Register(cultivation.cultivationDuration, cultivation.GenerateCultivation, null, true, true, null);
            Timer.Register(ageDuration, IncreaseAge, null, true, true, null);
        }

        private void Update()
        {
            cultivationUI.CultivationTime.maxValue = cultivation.cultivationDuration;
            cultivationUI.CultivationTime.value = cultivationTimer.GetTimeRemaining();
            cultivationUI.CultivationTimeText.text = Mathf.Round(cultivationTimer.GetTimeRemaining()).ToString();

            percentNeeded = subRealm != SubRealmType.TEN ? (int)realm * 10 : ((int)(realm + 1) * 10);

            percentSuccess = 100 - percentNeeded;
            if(OnPercentUpdate != null)
                OnPercentUpdate.Raise((int)percentSuccess);
        }
        
        public float ReturnPercentage()
        {
            float percent = (float)Math.Round(percentNeeded / 1 * 0.01f, 2);
            return percent;
        }

        public void SetInternalForce()
        {
            for (int i = 0; i < SubStats.Count; i++)
            {
                if (SubStats[i].Name == "MinInternalForce")
                    minCB = SubStats[i].CurrentPoints;
                if (SubStats[i].Name == "MaxInternalForce")
                    maxCB = SubStats[i].CurrentPoints;
            }

            cultivation.SetMinMax(minCB, maxCB);
        }

        public void IncreaseAge() {
            if (OnAgeIncrease != null)
                OnAgeIncrease.Raise(age++);
        }

        public void AttemptBreakthrough()
        {
            if (realm == EnumUtils.Max<RealmType>() && subRealm == EnumUtils.Max<SubRealmType>())
                return;

            if (cultivation.cultivationBase >= requiredExp)
            {
                if (Random.value < ReturnPercentage())
                {
                    cultivation.cultivationBase -= (long)(cultivation.cultivationBase * 0.15);
                    if (subRealm == EnumUtils.Max<SubRealmType>())
                    {
                        RealmType nextRealm = EnumUtils.Next(realm);
                        OnRealmUpgradeFailure.Raise(nextRealm);
                    }
                    else
                    {
                        SubRealmType nextSubRealm = EnumUtils.Next(subRealm);
                        OnSubRealmUpgradeFailure.Raise(nextSubRealm);
                    }
                } 
                else
                {
                    cultivation.cultivationBase -= requiredExp;
                    if (subRealm == EnumUtils.Max<SubRealmType>())
                    {
                        if (OnRealmUpgrade != null)
                        {
                            ///!TODO: Animation when increasing your realm
                            OnRealmUpgrade.Raise(realm++);
                            subRealm = SubRealmType.ONE;
                            OnSubRealmUpgrade.Raise(subRealm);
                            for (int i = 0; i < stats.Count; i++)
                            {
                                stats[i].AddStatPoints(10);
                                stats[i].OnStatAddEvent.Raise(stats[i]);
                            }
                            minCB *= .35f;
                            maxCB *= .35f;
                            for (int i = 0; i < subStats.Count; i++)
                            {
                                if (subStats[i].Name == "MinInternalForce")
                                    subStats[i].AddStatPoints((float)Math.Round(minCB, 2));
                                if (subStats[i].Name == "MaxInternalForce")
                                    subStats[i].AddStatPoints((float)Math.Round(maxCB, 2));
                            }
                        }
                    }
                    else
                    {
                        if (OnSubRealmUpgrade != null)
                        {
                            OnSubRealmUpgrade.Raise(subRealm++);
                            for (int i = 0; i < stats.Count; i++)
                            {
                                stats[i].AddStatPoints(1);
                                stats[i].OnStatAddEvent.Raise(stats[i]);
                            }
                            minCB *= .23f;
                            maxCB *= .23f;
                            for (int i = 0; i < subStats.Count; i++)
                            {
                                if (subStats[i].Name == "MinInternalForce")
                                    subStats[i].AddStatPoints((float)Math.Round(minCB, 2));
                                if (subStats[i].Name == "MaxInternalForce")
                                    subStats[i].AddStatPoints((float)Math.Round(maxCB, 2));
                                
                            }
                        }
                    }
                    SetInternalForce();
                    RequiredExpToBreakthrough();
                }
            }
        }

        public double RequiredExpToBreakthrough()
        {
            int baseExp = 100 * ((int)realm + 1);
            long currentRealm = ((long)realm * 10) + (long)subRealm;
            requiredExp = (double)Math.Round(Mathf.Pow(currentRealm, 1.43f) * (baseExp * 10), 2);

            return requiredExp;
        }
    }
}
