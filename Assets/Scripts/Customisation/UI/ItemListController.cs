using System;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class ItemListController : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Button hideButton;
        [SerializeField] private float height;

        private void Awake()
        {
            hideButton.onClick.AddListener(Hide);
        }

        public void Show()
        {
            rectTransform.sizeDelta = Vector2.up*height;
        }
        
        public void Hide()
        {
            rectTransform.sizeDelta = Vector2.zero;
        }
    }
}

