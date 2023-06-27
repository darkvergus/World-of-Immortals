using Realm;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/New item")]
    public abstract class ItemBase : ScriptableObject
    {
        [Header("Info")] [SerializeField] private new string name = "New Item";
        [SerializeField] public Sprite icon;
        
        [SerializeField] public RealmType realmRequired;

        public abstract string ColoredName { get; }
        public string Name => name;
        public Sprite Icon => icon;

        public RealmType RealmRequired => realmRequired;

        public abstract string GetInfoDisplayText();
    }
}