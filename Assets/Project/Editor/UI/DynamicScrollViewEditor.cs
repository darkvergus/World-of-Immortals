using UnityEditor;
using UnityEngine;

namespace UI
{
    public class DynamicScrollViewEditor
    {
        [MenuItem("GameObject/UI/Dynamic H Scroll View")]
        public static void CreateHorizontal()
        {
            GameObject go = new GameObject("Horizontal Scroll View", typeof(RectTransform));
            go.transform.SetParent(Selection.activeTransform, false);
            go.AddComponent<DynamicHScrollView>().Init();
        }

        [MenuItem("GameObject/UI/Dynamic V Scroll View")]
        public static void CreateVertical()
        {
            GameObject go = new GameObject("Vertical Scroll View", typeof(RectTransform));
            go.transform.SetParent(Selection.activeTransform, false);
            go.AddComponent<DynamicVScrollView>().Init();
        }
    }
}