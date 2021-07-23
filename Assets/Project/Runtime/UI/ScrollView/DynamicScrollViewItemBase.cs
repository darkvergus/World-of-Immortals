using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DynamicScrollViewItemBase : UIBehaviour, IDynamicScrollViewItem
    {
        private readonly Color[] colors = new Color[]
        {
            Color.cyan,
            Color.green,
        };

        private Text title;

        private Image background;

        public Text Title { get { return title; } set { title = value; } }
        public Image Background { get { return background; } set { background = value; } }

        public void OnUpdateItem(int index)
        {
            title.text = string.Format("Name{0:d3}", (index + 1));
            background.color = colors[Mathf.Abs(index) % colors.Length];
        }
    }
}