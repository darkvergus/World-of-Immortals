using System;
using Events;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private string name;

        [SerializeField, Min(0)] private float baseValue = 5;

        [SerializeField] private int maxValue = 9999999;

        public StatEvent OnStatAddEvent;

        public string Name 
        { 
            get => name;
            set => name = value;
        }
        
        public float BaseValue 
        { 
            get => Mathf.Clamp(baseValue, 0, maxValue);
            private set => baseValue = value;
        }
        
        public int MaxValue 
        { 
            get => maxValue;
            private set => maxValue = value;
        }

        public void AddStat(int value) => baseValue += value;
        
        public void AddStat(float value) => baseValue += value;

        public void RemoveStat(int value) => baseValue -= value;

        public void Reset() => baseValue = 5;
    }
}