using UnityEngine;

namespace Utils
{
    public class ToDoListConfig : ScriptableObject
    {
        private Texture2D iconDone;
        private Texture2D iconScript;

        public Texture2D IconDone { get{ return iconDone; } set { iconDone = value; } }
        public Texture2D IconScript { get { return iconScript; } set { iconScript = value; } }
    }
}