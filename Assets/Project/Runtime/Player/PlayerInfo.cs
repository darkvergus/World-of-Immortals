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
        private new string name;

        [SerializeField]
        private Image avatar;

        private RealmType realm = RealmType.QICONDENSATION;

        private SubRealmType subRealm = SubRealmType.ONE;

        private BodyType body = BodyType.BONEREFINING;

        private SubRealmType subBodyRealm = SubRealmType.ONE;

        public RealmType Realm { get { return realm; } set { realm = value; } }
        public BodyType Body { get { return body; } set { body = value; } }
        public SubRealmType SubRealm { get { return subRealm; } set { subRealm = value; } }
        public SubRealmType SubBodyRealm { get { return subBodyRealm; } set { subBodyRealm = value; } }

        [SerializeField]
        private CultivationBase cultivation;

        [SerializeField]
        private CultivationUI cultivationUI;

        private float percentNeeded;

        [SerializeField]
        [Min(0)]
        private double requiredExp;

        [SerializeField]
        private List<Stat> stats;

        [SerializeField]
        private List<Stat> subStats;

        private IntEvent onPercentUpdate;
        private DoubleEvent onRequiredExpUpdate;
        private RealmEvent onRealmUpgrade;
        private RealmEvent onRealmUpgradeFailure;
        private SubRealmEvent onSubRealmUpgrade;
        private SubRealmEvent onSubRealmUpgradeFailure;

        public IntEvent OnPercentUpdate { get { return onPercentUpdate; }set { onPercentUpdate = value; } }
        public DoubleEvent OnRequiredExpUpdate { get { return onRequiredExpUpdate; } set { onRequiredExpUpdate = value; } }
        public RealmEvent OnRealmUpgrade { get { return onRealmUpgrade; } set { onRealmUpgrade = value; } }
        public RealmEvent OnRealmUpgradeFailure { get { return onRealmUpgradeFailure; } set { onRealmUpgradeFailure = value; } }
        public SubRealmEvent OnSubRealmUpgrade { get { return onSubRealmUpgrade; } set { onSubRealmUpgrade = value; } }
        public SubRealmEvent OnSubRealmUpgradeFailure { get { return onSubRealmUpgradeFailure; } set { onSubRealmUpgradeFailure = value; } }

        public string Name => name;
        public Image Avatar => avatar;
        public CultivationBase Cultivation => cultivation;
        public float PercentNeeded => percentNeeded;
        public double RequiredExp => requiredExp;

        public List<Stat> Stats => stats;
        public List<Stat> SubStats => subStats;

        private float minCB;
        private float maxCB;

        public float MinCB => minCB;
        public float MaxCB => maxCB;

        private Timer cultivationTimer;

        private void Awake()
        {
            RequiredExpToBreakthrough();
            SetInternalForce();
            cultivationTimer = Timer.Register(cultivation.CultivationDuration, cultivation.GenerateCultivation, null, true, true, null);
        }

        private void Update()
        {
            cultivationUI.CultivationTime.maxValue = cultivation.CultivationDuration;
            cultivationUI.CultivationTime.value = cultivationTimer.GetTimeRemaining();
            cultivationUI.CultivationTimeText.text = Mathf.Round(cultivationTimer.GetTimeRemaining()).ToString();

            percentNeeded = subRealm != SubRealmType.TEN ? (int)realm * 10 : ((int)(realm + 1) * 10);

            float percentSuccess;

            percentSuccess = 100 - percentNeeded;
            if (OnPercentUpdate != null)
            {
                OnPercentUpdate.Raise((int)percentSuccess);
            }
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
                {
                    minCB = SubStats[i].CurrentPoints;
                }

                if (SubStats[i].Name == "MaxInternalForce")
                {
                    maxCB = SubStats[i].CurrentPoints;
                }
            }

            cultivation.SetMinMax(minCB, maxCB);
        }

        public void AttemptBreakthrough()
        {
            if (realm == EnumUtils.Max<RealmType>() && subRealm == EnumUtils.Max<SubRealmType>())
            {
                return;
            }

            if (cultivation.CB >= requiredExp)
            {
                if (Random.value < ReturnPercentage())
                {
                    cultivation.CB -= (long)(cultivation.CB * 0.15);
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
                    cultivation.CB -= requiredExp;
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
                            RaiseSubStats();
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
                            RaiseSubStats();
                        }
                    }
                    SetInternalForce();
                    RequiredExpToBreakthrough();
                }
            }
        }

        private void RaiseSubStats()
        {
            for (int i = 0; i < subStats.Count; i++)
            {

                if (subStats[i].Name == "MinInternalForce")
                {
                    subStats[i].AddStatPoints((float)Math.Round(minCB, 2));
                }

                if (subStats[i].Name == "MaxInternalForce")
                {
                    subStats[i].AddStatPoints((float)Math.Round(maxCB, 2));
                }

                subStats[i].OnStatAddEvent.Raise(subStats[i]);
            }
        }

        public double RequiredExpToBreakthrough()
        {
            int baseExp = 100 * ((int)realm + 1);
            long currentRealm = ((long)realm * 10) + (long)subRealm;
            requiredExp = Math.Round(Mathf.Pow(currentRealm, 1.43f) * (baseExp * 10), 2);
            OnRequiredExpUpdate.Raise(requiredExp);

            return requiredExp;
        }
    }
}
