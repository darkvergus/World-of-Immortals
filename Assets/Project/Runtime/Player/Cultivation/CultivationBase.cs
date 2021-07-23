using Events;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cultivation
{
    [Serializable]
    public class CultivationBase
    {
        [Min(0)]
        private double cultivationBase;

        private int generateCB;
        
        [Header("Controls")]
        private int cultivationDuration;

        public int CultivationDuration { get { return cultivationDuration; } set { cultivationDuration = value; } } 

        public double CB { get { return cultivationBase; } set{ cultivationBase = value; } }

        private IntEvent onCultivationGain;
        public IntEvent OnCultivationGain { get { return onCultivationGain; } set { onCultivationGain = value; } }

        private float minCB;
        private float maxCB;

        public void SetMinMax(float min, float max)
        {
            minCB = min;
            maxCB = max;
        }

        public void GenerateCultivation()
        {
            generateCB = (int)Random.Range(minCB, maxCB);

            if (generateCB != 0)
            {
                cultivationBase += generateCB;
                if (OnCultivationGain != null)
                {
                    OnCultivationGain.Raise(generateCB);
                }
            }
        }  
    }
}