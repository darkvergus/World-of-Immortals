using UnityEngine;

namespace UI
{
    public class GUIWindowUtil : MonoBehaviour
    {
        public void CloseWindow() => gameObject.SetActive(false);

        public void OpenWindow() => gameObject.SetActive(true);
    }
}