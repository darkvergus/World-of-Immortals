using Events;
using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] 
        private string name = "";
        
        [SerializeField]
        [Min(0)]
        private float currentPoints = 5;

        [SerializeField] 
        private int maxPoints = 9999999;

        public string Name { get { return name; } set { name = value; } }
        public float CurrentPoints { get { return Mathf.Clamp(currentPoints, 0, maxPoints); } private set { currentPoints = value; } }
        public int MaxPoints { get { return maxPoints; } private set { maxPoints = value; } }

        public StatEvent OnStatAddEvent;

        public void AddStatPoints(int points) => CurrentPoints += points;

        public void AddStatPoints(float points) => CurrentPoints += points;

        public void RemoveStatPoints(int points) => CurrentPoints -= points;

        public void Reset() => CurrentPoints = 5;
    }
}