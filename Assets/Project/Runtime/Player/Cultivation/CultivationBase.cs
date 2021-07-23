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
        public double cultivationBase;

        private int generateCB;
        
        [Header("Controls")]
        public int cultivationDuration;

        public IntEvent OnCultivationGain;

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