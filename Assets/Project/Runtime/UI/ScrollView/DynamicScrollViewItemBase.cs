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

        public Text title;
        public Image background;

        public void OnUpdateItem(int index)
        {
            title.text = string.Format("Name{0:d3}", (index + 1));
            background.color = colors[Mathf.Abs(index) % colors.Length];
        }
    }
}