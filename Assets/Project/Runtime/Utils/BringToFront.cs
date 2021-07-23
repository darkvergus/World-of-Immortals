using UnityEngine;

namespace Utils
{
    public class BringToFront : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;

        private void Start() => rectTransform.SetAsLastSibling();
    }
}
