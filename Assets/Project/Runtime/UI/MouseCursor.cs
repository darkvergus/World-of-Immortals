using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MouseCursor : MonoBehaviour
    {
        [SerializeField]
        private Texture2D cursorGraphic;

        [SerializeField]
        private Texture2D cursorClicked;

        private void Awake()
        {
            ChangeCursor(cursorGraphic);
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void ChangeCursor(Texture2D cursorType)
        {
            Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
            Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
        }

        private void OnMouseDown() => ChangeCursor(cursorClicked);
    }
}
