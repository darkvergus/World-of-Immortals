using UnityEngine;

namespace Utils
{
    public static class RectTransformUtils
    {
        public static RectTransform SetFullSize(this RectTransform self)
        {
            self.sizeDelta = new Vector2(0.0f, 0.0f);
            self.anchorMin = new Vector2(0.0f, 0.0f);
            self.anchorMax = new Vector2(1.0f, 1.0f);
            self.pivot = new Vector2(0.5f, 0.5f);
            return self;
        }

        public static Vector2 GetSize(this RectTransform self) => self.rect.size;

        public static void SetSize(this RectTransform self, Vector2 newSize)
        {
            Vector2 pivot = self.pivot;
            Vector2 dist = newSize - self.rect.size;
            self.offsetMin -= new Vector2(dist.x * pivot.x, dist.y * pivot.y);
            self.offsetMax += new Vector2(dist.x * (1f - pivot.x), dist.y * (1f - pivot.y));
        }

        public static RectTransform SetSizeFromLeft(this RectTransform self, float rate)
        {
            self.SetFullSize();

            float width = self.rect.width;

            self.anchorMin = new Vector2(0.0f, 0.0f);
            self.anchorMax = new Vector2(0.0f, 1.0f);
            self.pivot = new Vector2(0.0f, 1.0f);
            self.sizeDelta = new Vector2(width * rate, 0.0f);

            return self;
        }

        public static RectTransform SetSizeFromRight(this RectTransform self, float rate)
        {
            self.SetFullSize();

            float width = self.rect.width;

            self.anchorMin = new Vector2(1.0f, 0.0f);
            self.anchorMax = new Vector2(1.0f, 1.0f);
            self.pivot = new Vector2(1.0f, 1.0f);
            self.sizeDelta = new Vector2(width * rate, 0.0f);

            return self;
        }

        public static RectTransform SetSizeFromTop(this RectTransform self, float rate)
        {
            self.SetFullSize();

            float height = self.rect.height;

            self.anchorMin = new Vector2(0.0f, 1.0f);
            self.anchorMax = new Vector2(1.0f, 1.0f);
            self.pivot = new Vector2(0.0f, 1.0f);
            self.sizeDelta = new Vector2(0.0f, height * rate);

            return self;
        }

        public static RectTransform SetSizeFromBottom(this RectTransform self, float rate)
        {
            self.SetFullSize();

            float height = self.rect.height;

            self.anchorMin = new Vector2(0.0f, 0.0f);
            self.anchorMax = new Vector2(1.0f, 0.0f);
            self.pivot = new Vector2(0.0f, 0.0f);
            self.sizeDelta = new Vector2(0.0f, height * rate);

            return self;
        }

        public static void SetOffset(this RectTransform self, float left, float top, float right, float bottom)
        {
            self.offsetMin = new Vector2(left, top);
            self.offsetMax = new Vector2(right, bottom);
        }

        public static bool InScreenRect(this RectTransform self, Vector2 screenPos)
        {
            Canvas canvas = self.GetComponentInParent<Canvas>();
            switch (canvas.renderMode)
            {
                case RenderMode.ScreenSpaceCamera:
                    {
                        Camera camera = canvas.worldCamera;
                        if (camera != null)
                        {
                            return RectTransformUtility.RectangleContainsScreenPoint(self, screenPos, camera);
                        }
                    }
                    break;

                case RenderMode.ScreenSpaceOverlay:
                    return RectTransformUtility.RectangleContainsScreenPoint(self, screenPos);

                case RenderMode.WorldSpace:
                    return RectTransformUtility.RectangleContainsScreenPoint(self, screenPos);
                default:
                    return RectTransformUtility.RectangleContainsScreenPoint(self, screenPos);
            }
            return false;
        }

        public static bool InScreenRect(this RectTransform self, RectTransform rectTransform)
        {
            Rect rect1 = GetScreenRect(self);
            Rect rect2 = GetScreenRect(rectTransform);
            return rect1.Overlaps(rect2);
        }

        public static Rect GetScreenRect(this RectTransform self)
        {
            Rect rect = new Rect();
            Canvas canvas = self.GetComponentInParent<Canvas>();
            Camera camera = canvas.worldCamera;
            if (camera != null)
            {
                Vector3[] corners = new Vector3[4];
                self.GetWorldCorners(corners);
                rect.min = camera.WorldToScreenPoint(corners[0]);
                rect.max = camera.WorldToScreenPoint(corners[2]);
            }
            return rect;
        }
    }
}