using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class DynamicScrollView : UIBehaviour
    {
        private int totalItemCount = 99;
        private RectTransform itemPrototype;
        protected abstract float ContentAnchoredPosition { get; set; }
        protected abstract float ContentSize { get; }
        protected abstract float ViewportSize { get; }
        protected abstract float ItemSize { get; }

        protected Direction direction = Direction.VERTICAL;
        protected LinkedList<RectTransform> containers = new LinkedList<RectTransform>();
        protected float prevAnchoredPosition;
        protected int nextInsertItemNo; // item index from left-top of viewport at next insert
        protected int prevTotalItemCount = 99;
        protected ScrollRect scrollRect;
        protected RectTransform viewportRect;
        protected RectTransform contentRect;

        public int TotalItemCount { get { return totalItemCount; } set { totalItemCount = value; } }

        public RectTransform ItemPrototype { get { return itemPrototype; } set { itemPrototype = value; } }

        public enum Direction
        {
            VERTICAL,
            HORIZONTAL,
        }

        public void ScrollToLastPos()
        {
            ContentAnchoredPosition = ViewportSize - ContentSize;
            Refresh();
        }

        public void ScrollByItemIndex(int itemIndex)
        {
            float totalLen = ContentSize;
            float itemLen = totalLen / totalItemCount;
            float pos = itemLen * itemIndex;
            ContentAnchoredPosition = -pos;
        }

        public void Refresh()
        {
            int index = 0;
            if (ContentAnchoredPosition != 0)
            {
                index = (int)(-ContentAnchoredPosition / ItemSize);
            }

            foreach (RectTransform itemRect in containers)
            {
                float pos = ItemSize * index;
                itemRect.anchoredPosition = (direction == Direction.VERTICAL) ? new Vector2(0, -pos) : new Vector2(pos, 0);

                UpdateItem(index, itemRect.gameObject);

                index++;
            }

            nextInsertItemNo = index - containers.Count;
            prevAnchoredPosition = (int)(ContentAnchoredPosition / ItemSize) * ItemSize;
        }

        protected override void Awake()
        {
            if (itemPrototype == null)
            {
                return;
            }

            base.Awake();

            scrollRect = GetComponent<ScrollRect>();
            viewportRect = scrollRect.viewport;
            contentRect = scrollRect.content;
        }

        protected override void Start()
        {
            prevTotalItemCount = totalItemCount;

            StartCoroutine(OnSeedData());
        }

        protected virtual IEnumerator OnSeedData()
        {
            yield return null;

            itemPrototype.gameObject.SetActive(false);

            int itemCount = (int)(ViewportSize / ItemSize) + 3;

            for (int i = 0; i < itemCount; ++i)
            {
                RectTransform itemRect = Instantiate(itemPrototype);
                itemRect.SetParent(contentRect, false);
                itemRect.name = i.ToString();
                itemRect.anchoredPosition = direction == Direction.VERTICAL ? new Vector2(0, -ItemSize * i) : new Vector2(ItemSize * i, 0);
                containers.AddLast(itemRect);

                itemRect.gameObject.SetActive(true);

                UpdateItem(i, itemRect.gameObject);
            }
            ResizeContent();
        }

        private void Update()
        {
            if (totalItemCount != prevTotalItemCount)
            {
                prevTotalItemCount = totalItemCount;

                bool isBottom = false;
                if (ViewportSize - ContentAnchoredPosition >= ContentSize - ItemSize * 0.5f)
                {
                    isBottom = true;
                }

                ResizeContent();

                if (isBottom)
                {
                    ContentAnchoredPosition = ViewportSize - ContentSize;
                }

                Refresh();
            }

            while (ContentAnchoredPosition - prevAnchoredPosition < -ItemSize * 2)
            {
                prevAnchoredPosition -= ItemSize;

                LinkedListNode<RectTransform> first = containers.First;
                if (first == null)
                {
                    break;
                }

                RectTransform item = first.Value;
                containers.RemoveFirst();
                containers.AddLast(item);

                float pos = ItemSize * (containers.Count + nextInsertItemNo);
                item.anchoredPosition = (direction == Direction.VERTICAL) ? new Vector2(0, -pos) : new Vector2(pos, 0);

                UpdateItem(containers.Count + nextInsertItemNo, item.gameObject);

                nextInsertItemNo++;
            }

            while (ContentAnchoredPosition - prevAnchoredPosition > 0)
            {
                prevAnchoredPosition += ItemSize;

                LinkedListNode<RectTransform> last = containers.Last;
                if (last == null)
                {
                    break;
                }

                RectTransform item = last.Value;
                containers.RemoveLast();
                containers.AddFirst(item);

                nextInsertItemNo--;

                float pos = ItemSize * nextInsertItemNo;
                item.anchoredPosition = (direction == Direction.VERTICAL) ? new Vector2(0, -pos) : new Vector2(pos, 0);

                UpdateItem(nextInsertItemNo, item.gameObject);
            }
        }

        private void ResizeContent()
        {
            Vector2 size = contentRect.GetSize();
            if (direction == Direction.VERTICAL)
            {
                size.y = ItemSize * totalItemCount;
            }
            else
            {
                size.x = ItemSize * totalItemCount;
            }

            contentRect.SetSize(size);
        }

        private void UpdateItem(int index, GameObject itemObj)
        {
            if (index < 0 || index >= totalItemCount)
            {
                itemObj.SetActive(false);
            }
            else
            {
                itemObj.SetActive(true);

                IDynamicScrollViewItem item = itemObj.GetComponent<IDynamicScrollViewItem>();
                if (item != null)
                {
                    item.OnUpdateItem(index);
                }
            }
        }

        [ContextMenu("Initialize")]
        public virtual void Init()
        {
            Clear();

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.SetFullSize();

            ScrollRect scrollRect = GetComponent<ScrollRect>();
            scrollRect.horizontal = direction == Direction.HORIZONTAL;
            scrollRect.vertical = direction == Direction.VERTICAL;
            scrollRect.scrollSensitivity = 15f;

            RectTransform viewportRect = new GameObject("Viewport", typeof(RectTransform), typeof(Mask), typeof(Image)).GetComponent<RectTransform>();
            viewportRect.SetParent(scrollRect.transform, false);
            viewportRect.SetFullSize();
            viewportRect.offsetMin = new Vector2(10f, 10f);
            viewportRect.offsetMax = new Vector2(-10f, -10f);
            Image viewportImage = viewportRect.GetComponent<Image>();
            viewportImage.type = Image.Type.Sliced;
            Mask viewportMask = viewportRect.GetComponent<Mask>();
            viewportMask.showMaskGraphic = false;
            scrollRect.viewport = viewportRect;

            RectTransform contentRect = new GameObject("Content", typeof(RectTransform)).GetComponent<RectTransform>();
            contentRect.SetParent(viewportRect, false);

            if (direction == Direction.HORIZONTAL)
            {
                contentRect.SetSizeFromLeft(1.0f);
            }
            else
            {
                contentRect.SetSizeFromTop(1.0f);
            }

            Vector2 contentRectSize = contentRect.GetSize();
            contentRect.SetSize(contentRectSize - contentRectSize * 0.06f);
            scrollRect.content = contentRect;

            ResetPrototypeItem(contentRect);

            string scrollbarName = direction == Direction.HORIZONTAL ? "Scrollbar Horizontal" : "Scrollbar Vertical";
            RectTransform scrollbarRect = new GameObject(scrollbarName, typeof(Scrollbar), typeof(Image)).GetComponent<RectTransform>();
            scrollbarRect.SetParent(viewportRect, false);
            if (direction == Direction.HORIZONTAL)
            {
                scrollbarRect.SetSizeFromBottom(0.05f);
            }
            else
            {
                scrollbarRect.SetSizeFromRight(0.05f);
            }

            scrollbarRect.SetParent(scrollRect.transform, true);

            Scrollbar scrollbar = scrollbarRect.GetComponent<Scrollbar>();
            scrollbar.direction = (direction == Direction.HORIZONTAL) ? Scrollbar.Direction.LeftToRight : Scrollbar.Direction.BottomToTop;
            if (direction == Direction.HORIZONTAL)
            {
                scrollRect.horizontalScrollbar = scrollbar;
            }
            else
            {
                scrollRect.verticalScrollbar = scrollbar;
            }

            Image scrollbarImage = scrollbarRect.GetComponent<Image>();
#if UNITY_EDITOR
            scrollbarImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
#endif
            scrollbarImage.color = new Color(0.1f, 0.1f, 0.1f, 0.5f);
            scrollbarImage.type = Image.Type.Sliced;

            RectTransform slidingAreaRect = new GameObject("Sliding Area", typeof(RectTransform)).GetComponent<RectTransform>();
            slidingAreaRect.SetParent(scrollbarRect, false);
            slidingAreaRect.SetFullSize();

            RectTransform scrollbarHandleRect = new GameObject("Handle", typeof(Image)).GetComponent<RectTransform>();
            scrollbarHandleRect.SetParent(slidingAreaRect, false);
            scrollbarHandleRect.SetFullSize();
            Image scrollbarHandleImage = scrollbarHandleRect.GetComponent<Image>();
#if UNITY_EDITOR
            scrollbarHandleImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
#endif
            scrollbarHandleImage.color = new Color(0.5f, 0.5f, 1.0f, 0.5f);
            scrollbarHandleImage.type = Image.Type.Sliced;
            scrollbar.handleRect = scrollbarHandleRect;

            ScrollbarHandleSize scrollbarHandleSize = scrollRect.GetComponent<ScrollbarHandleSize>();
            if (scrollbarHandleSize == null)
            {
                scrollbarHandleSize = scrollRect.gameObject.AddComponent<ScrollbarHandleSize>();
                scrollbarHandleSize.MaxSize = 1.0f;
                scrollbarHandleSize.MinSize = 0.1f;
            }

            gameObject.SetLayer(transform.parent.gameObject.layer, true);
        }

        protected virtual void ResetPrototypeItem(RectTransform contentRect)
        {
            RectTransform prototypeItemRect = new GameObject("Prototype Item", typeof(RectTransform), typeof(Image), typeof(DynamicScrollViewItemBase)).GetComponent<RectTransform>();
            prototypeItemRect.SetParent(contentRect, false);
            if (direction == Direction.HORIZONTAL)
            {
                prototypeItemRect.SetSizeFromLeft(0.23f);
            }
            else
            {
                prototypeItemRect.SetSizeFromTop(0.23f);
            }

            DynamicScrollViewItemBase prototypeItem = prototypeItemRect.GetComponent<DynamicScrollViewItemBase>();
            Image prototypeItemBg = prototypeItemRect.GetComponent<Image>();
#if UNITY_EDITOR
            prototypeItemBg.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
#endif
            prototypeItemBg.type = Image.Type.Sliced;
            prototypeItem.Background = prototypeItemBg;
            itemPrototype = prototypeItemRect;

            RectTransform prototypeTitleRect = new GameObject("Title", typeof(RectTransform), typeof(Text)).GetComponent<RectTransform>();
            prototypeTitleRect.SetParent(prototypeItemRect, false);
            prototypeTitleRect.SetFullSize();
            Vector2 prototypeTitleSize = prototypeTitleRect.GetSize();
            prototypeTitleRect.SetSize(prototypeTitleSize - prototypeTitleSize * 0.1f);
            Text title = prototypeTitleRect.GetComponent<Text>();
            title.fontSize = 16;
            title.alignment = TextAnchor.MiddleCenter;
            title.horizontalOverflow = HorizontalWrapMode.Wrap;
            title.verticalOverflow = VerticalWrapMode.Truncate;
            title.color = Color.black;
            title.text = "Name000";
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 9;
            title.resizeTextMaxSize = 40;
            prototypeItem.Title = title;
        }

        protected virtual void Clear()
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
    }
}