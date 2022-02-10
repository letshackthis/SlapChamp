using System;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class UnlockItemOption : MonoBehaviour
    {
        [SerializeField] private Text priceText;
        [SerializeField] private Text unlockName;
        [SerializeField] private Text unlockButtonText;
        [SerializeField] private Image unlockImage;
        [SerializeField] private Button unlockButton;

        public Action unlock;

        public Text PriceText => priceText;

        private void Awake()
        {
            unlockButton.onClick.AddListener(()=>unlock?.Invoke());
        }

        public void Initialize(string priceTextValue, string nameUnlock, string unlockText, Sprite sprite)
        {
            priceText.text = priceTextValue;
            unlockName.text = nameUnlock;
            unlockImage.sprite = sprite;
            unlockButtonText.text = unlockText;
        }
        
    }
}
