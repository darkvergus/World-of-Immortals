using System;
using Events;

namespace Stats
{
    [Serializable]
    public class StatModifier
    {
        public Stat target;
        public OperationType operationType;
        public ModifierType modifierType;
        
        public int value;
        public bool stackable;

        public StatEvent OnModifierAdded;
        public StatEvent OnModifierRemoved;
    }
}