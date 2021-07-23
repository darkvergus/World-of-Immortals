using UnityEngine;

namespace UI
{
    [AddComponentMenu("UI/Dynamic V Scroll View")]
    public class DynamicVScrollView : DynamicScrollView
    {
        protected override float ContentAnchoredPosition { get { return -contentRect.anchoredPosition.y; } set { contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, -value); } }
        protected override float ContentSize => contentRect.rect.height;
        protected override float ViewportSize => viewportRect.rect.height;
        protected override float ItemSize => itemPrototype.rect.height;

        public override void Init()
        {
            direction = Direction.VERTICAL;
            base.Init();
        }

        protected override void Awake()
        {
            base.Awake();
            direction = Direction.VERTICAL;
        }

        protected override void Start() => base.Start();
    }
}