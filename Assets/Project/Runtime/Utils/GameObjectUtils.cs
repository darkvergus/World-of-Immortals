using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class GameObjectUtils
    {
        public static GameObject AddChild(GameObject parent, string name)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            GameObject gameObject = new(name)
            {
                layer = parent.layer
            };

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            RectTransform parentRectTransform = parent.GetComponent<RectTransform>();
            rectTransform.SetParent(parentRectTransform, false);

            return gameObject;
        }

        public static GameObject AddChild(GameObject parent, GameObject prefab)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            GameObject gameObject = Object.Instantiate(prefab);
            gameObject.layer = parent.layer;

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            RectTransform parentRectTransform = parent.GetComponent<RectTransform>();
            rectTransform.SetParent(parentRectTransform, false);

            return gameObject;
        }
        
        public static void SetLayer(this GameObject self, int layer, bool includeChildren)
        {
            self.layer = layer;
            if (includeChildren)
            {
                Transform[] children = self.transform.GetComponentsInChildren<Transform>(true);
                foreach (Transform transform in children)
                {
                    transform.gameObject.layer = layer;
                }
            }
        }
    }
}