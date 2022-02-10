using UnityEngine;

namespace Customisation.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SelectorCellImage : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            Unselect();
        }

        public void SelectCurrentCell(GameObject cell)
        {
            transform.SetParent(cell.transform);
            rectTransform.anchoredPosition=Vector2.zero;
            rectTransform.sizeDelta=Vector2.zero;
            rectTransform.localScale=Vector3.one;
        }

        public void Unselect()
        {
            transform.SetParent(null);
            rectTransform.localScale=Vector3.zero;

        }
    }
}
