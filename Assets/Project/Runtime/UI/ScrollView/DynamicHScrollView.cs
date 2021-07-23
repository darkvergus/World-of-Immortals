using UnityEngine;

namespace UI
{
    [AddComponentMenu("UI/Dynamic H Scroll View")]
    public class DynamicHScrollView : DynamicScrollView
    {
        protected override float ContentAnchoredPosition { get { return contentRect.anchoredPosition.x; } set { contentRect.anchoredPosition = new Vector2(value, contentRect.anchoredPosition.y); } }
        protected override float ContentSize => contentRect.rect.width;
        protected override float ViewportSize => viewportRect.rect.width;
        protected override float ItemSize => ItemPrototype.rect.width;

        public override void Init()
        {
            direction = Direction.HORIZONTAL;
            base.Init();
        }

        protected override void Awake()
        {
            base.Awake();
            direction = Direction.HORIZONTAL;
        }

        protected override void Start() => base.Start();
    }
}