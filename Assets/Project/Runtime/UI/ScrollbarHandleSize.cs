using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI 
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollbarHandleSize : UIBehaviour
    {
        private float maxSize = 1.0f;
        private float minSize = 0.1f;

        public float MaxSize { get { return maxSize; } set { maxSize = value; } }
        public float MinSize { get { return minSize; } set { minSize = value; } }

        private ScrollRect  scrollRect;

        protected override void Awake()
        {
            base.Awake();
            scrollRect = GetComponent<ScrollRect>();
        }

        protected override void OnEnable() => scrollRect.onValueChanged.AddListener(OnValueChanged);

        protected override void OnDisable() => scrollRect.onValueChanged.RemoveListener(OnValueChanged);

        public void OnValueChanged( Vector2 value ) {

            Scrollbar hScrollbar = scrollRect.horizontalScrollbar;
            if (hScrollbar != null)
            {
                if (hScrollbar.size > maxSize)
                {
                    hScrollbar.size = maxSize;
                }
                else if (hScrollbar.size < minSize)
                {
                    hScrollbar.size = minSize;
                }
            }

            Scrollbar vScrollbar = scrollRect.verticalScrollbar;
            if (vScrollbar != null)
            {
                if (vScrollbar.size > maxSize)
                {
                    vScrollbar.size = maxSize;
                }
                else if( vScrollbar.size < minSize )
                {
                    vScrollbar.size = minSize;
                }
            }
        }
    }
}