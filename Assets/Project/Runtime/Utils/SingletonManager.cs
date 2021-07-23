using UnityEngine;

namespace Utils
{
    public class SingletonManager<T> : MonoBehaviour where T : SingletonManager<T>
	{
        private static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        instance = obj.AddComponent<T>();
					}
				}
				return instance;
			}
		}

		protected virtual void Awake()
		{
			if (instance == null)
			{
                instance = (T)this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}