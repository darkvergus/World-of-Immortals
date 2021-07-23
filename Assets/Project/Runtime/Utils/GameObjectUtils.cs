using UnityEngine;

namespace Utils
{
    public static class GameObjectUtils
    {
        public static void SetLayer(this GameObject self, int layer, bool includeChildren = true)
        {
            self.layer = layer;
            if (includeChildren)
            {
                Transform[] children = self.transform.GetComponentsInChildren<Transform>(true);
                for (int c = 0; c < children.Length; ++c)
                {
                    children[c].gameObject.layer = layer;
                }
            }
        }
    }
}