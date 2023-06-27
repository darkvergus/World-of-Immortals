using Realm;
using System;
using System.Collections.Generic;
using Events;
using Inventory;
using Player;
using Stats;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class PlayerManager : SingletonManager<PlayerManager>
    {
        [Header("Basic Info")] 
        [SerializeField] private new string name;

        [SerializeField] private Image avatar;

        [SerializeField] private RealmType realm = RealmType.QICONDENSATION;

        [SerializeField] private SubRealmType subRealm = SubRealmType.ONE;

        [SerializeField] private BodyType body = BodyType.BONEREFINING;

        [SerializeField] private SubRealmType subBodyRealm = SubRealmType.ONE;

        [SerializeField] private Cultivation cultivation;
        
        [SerializeField] [Min(0)] private double requiredCultivation;
        [SerializeField] [Min(0)] private double requiredBodyCultivation;

        [SerializeField] private InventoryBase inventory;
        
        [SerializeField] private List<Stat> stats; 
        [SerializeField] private List<Stat> subStats;

        private float percentNeededRealm;
        private Timer cultivationTimer;

        [SerializeField] private DoubleEvent OnRequiredCultivationToBreakthrough;
        [SerializeField] private RealmEvent OnRealmUpgrade;
        [SerializeField] private RealmEvent OnRealmUpgradeFailure;
        [SerializeField] private BodyEvent OnBodyRealmUpgrade;
        [SerializeField] private SubRealmEvent OnSubRealmUpgrade;
        [SerializeField] private SubRealmEvent OnSubRealmUpgradeFailure;
        [SerializeField] private IntEvent  OnPercentUpdate;

        private void Awake()
        {
            RequiredCultivationToBreakthrough();
            RequiredBodyCultivationToBreakthrough();
            SetInternalForce();
            cultivationTimer = Timer.Register(cultivation.cultivationDuration, GenerateCultivation, null, true, true, null);
        }

        private void Update()
        {
            percentNeededRealm = subRealm != SubRealmType.TEN ? (int)realm * 10 : (int)(realm + 1) * 10;

            float percentSuccess = 100 - percentNeededRealm;
            if (OnPercentUpdate != null)
            {
                OnPercentUpdate.Raise((int)percentSuccess);
            }
        }
        
        public float ReturnPercentage()
        {
            float percent = (float)Math.Round(percentNeededRealm / 1 * 0.01f, 2);
            return percent;
        }

        public void AttemptBreakthrough()
        {
            if (realm == EnumUtils.Max<RealmType>() && subRealm == EnumUtils.Max<SubRealmType>())
            {
                return;
            }

            // Realm
            if (cultivation.cultivationBase >= requiredCultivation)
            {
                if (Random.value < ReturnPercentage())
                {
                    cultivation.cultivationBase -= (long) (cultivation.cultivationBase * 0.15);
                    if (subRealm == EnumUtils.Max<SubRealmType>())
                    {
                        RealmType nextRealm = realm.Next();
                        OnRealmUpgradeFailure.Raise(nextRealm);
                    }
                    else
                    {
                        SubRealmType nextSubRealm = subRealm.Next();
                        OnSubRealmUpgradeFailure.Raise(nextSubRealm);
                    }
                }
                else
                {
                    cultivation.cultivationBase -= requiredCultivation;
                    if (subRealm == EnumUtils.Max<SubRealmType>())
                    {
                        if (OnRealmUpgrade != null)
                        {
                            ///!TODO: Animation when increasing your realm
                            OnRealmUpgrade.Raise(realm.Next());
                            subRealm = SubRealmType.ONE;
                            OnSubRealmUpgrade.Raise(subRealm);
                            for (int i = 0; i < stats.Count; i++)
                            {
                                stats[i].AddStat(10);
                                stats[i].OnStatAddEvent.Raise(stats[i]);
                            }

                            cultivation.minCultivationBase *= .35f;
                            cultivation.maxCultivationBase *= .35f;
                            RaiseSubStats();
                        }
                    }
                    else
                    {
                        if (OnSubRealmUpgrade != null)
                        {
                            OnSubRealmUpgrade.Raise(subRealm.Next());
                            for (int i = 0; i < stats.Count; i++)
                            {
                                stats[i].AddStat(1);
                                stats[i].OnStatAddEvent.Raise(stats[i]);
                            }

                            cultivation.minCultivationBase *= .23f;
                            cultivation.maxCultivationBase *= .23f;
                            RaiseSubStats();
                        }
                    }

                    SetInternalForce();
                    RequiredCultivationToBreakthrough();
                }
            }
            
            if (body == EnumUtils.Max<BodyType>() && subRealm == EnumUtils.Max<SubRealmType>())
            {
                return;
            }
            
            if (cultivation.cultivationBase >= requiredBodyCultivation)
            {
                cultivation.cultivationBase -= requiredBodyCultivation;
                if (subRealm == EnumUtils.Max<SubRealmType>())
                {
                    if (OnBodyRealmUpgrade != null)
                    {
                        ///!TODO: Animation when increasing your realm
                        OnBodyRealmUpgrade.Raise(body.Next());
                        subRealm = SubRealmType.ONE;
                        OnSubRealmUpgrade.Raise(subRealm);
                        for (int i = 0; i < stats.Count; i++)
                        {
                            stats[i].AddStat(1);
                            stats[i].OnStatAddEvent.Raise(stats[i]);
                        }

                        cultivation.minCultivationBase *= .35f;
                        cultivation.maxCultivationBase *= .35f;
                    }
                }
                else
                {
                    if (OnSubRealmUpgrade != null)
                    {
                        OnSubRealmUpgrade.Raise(subRealm.Next());
                        for (int i = 0; i < stats.Count; i++)
                        {
                            stats[i].AddStat(1);
                            stats[i].OnStatAddEvent.Raise(stats[i]);
                        }

                        cultivation.minCultivationBase *= .23f;
                        cultivation.maxCultivationBase *= .23f;
                    }
                }

                RequiredBodyCultivationToBreakthrough();
            }
        }

        public void SetInternalForce()
        {
            for (int i = 0; i < subStats.Count; i++)
            {
                if (subStats[i].Name == "MinInternalForce")
                {
                    cultivation.minCultivationBase = subStats[i].BaseValue;
                }

                if (subStats[i].Name == "MaxInternalForce")
                {
                    cultivation.maxCultivationBase = subStats[i].BaseValue;
                }
            }
        }

        public void RequiredCultivationToBreakthrough()
        {
            int baseExp = 100 * ((int) realm + 1);
            long currentRealm = (long) realm * 10 + (long) subRealm;
            requiredCultivation = Math.Round(Mathf.Pow(currentRealm, 1.43f) * (baseExp * 10), 2);
            OnRequiredCultivationToBreakthrough.Raise(requiredCultivation);
        }

        public void RequiredBodyCultivationToBreakthrough()
        {
            int baseExp = 10 * ((int) body + 1);
            long currentRealm = (long) body * 10 + (long) subBodyRealm;
            requiredBodyCultivation = Math.Round(Mathf.Pow(currentRealm, 1.43f) * (baseExp * 10), 2);
            OnRequiredCultivationToBreakthrough.Raise(requiredBodyCultivation);
        }
        
        private void GenerateCultivation()
        {
            int generateCB = (int) Random.Range(cultivation.minCultivationBase, cultivation.maxCultivationBase);

            if (generateCB != 0)
            {
                cultivation.cultivationBase += generateCB;
                if (cultivation.OnCultivationGain != null)
                {
                    cultivation.OnCultivationGain.Raise(generateCB);
                }
            }
        }
        
        private void RaiseSubStats()
        {
            for (int i = 0; i < subStats.Count; i++)
            {
                if (subStats[i].Name == "MinInternalForce")
                {
                    subStats[i].AddStat((float) Math.Round(cultivation.minCultivationBase, 2));
                }

                if (subStats[i].Name == "MaxInternalForce")
                {
                    subStats[i].AddStat((float) Math.Round(cultivation.maxCultivationBase, 2));
                }

                subStats[i].OnStatAddEvent.Raise(subStats[i]);
            }
        }
    }
}
