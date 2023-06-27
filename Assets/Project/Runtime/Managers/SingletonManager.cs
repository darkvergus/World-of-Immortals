using UnityEngine;

namespace Managers 
{
    public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool shuttingDown;
        private static readonly object @lock = new();
        private static T instance;

        [SerializeField]
        private bool dontDestroyOnLoad = true;
        
        public static T Instance
        {
            get
            {
                if (shuttingDown)
                {
                    return null;
                }

                lock (@lock)
                {
                    if (instance == null)
                    {
                        instance = (T)FindFirstObjectByType(typeof(T));
                        if (instance == null)
                        {
                            GameObject singletonObject = new();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T) + " (Singleton)";

                            //DontDestroyOnLoad(singletonObject);
                        }
                    }
                    return instance;
                }
            }
        }

        private void OnEnable()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnApplicationQuit() => shuttingDown = true;

        private void OnDestroy() => shuttingDown = true;
    }
}
