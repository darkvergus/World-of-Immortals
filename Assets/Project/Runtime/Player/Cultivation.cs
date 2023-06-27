using System;
using Events;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class Cultivation
    {
        [field: Min(0)] public double cultivationBase;

        [field: Header("Controls")] public int cultivationDuration;

        public float minCultivationBase;
        public float maxCultivationBase;
        
        public IntEvent OnCultivationGain;
    }
}