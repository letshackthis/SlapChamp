using System;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class ItemListController : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private List<GameObject> gameObjectList;
        [SerializeField] private float height;
        [SerializeField] private float heightAttachment;
        [SerializeField] private float minHeight;
        [SerializeField] private Swiper swiper;
        [SerializeField] private  Button closeButton;
      
        private bool isOpened;
        private bool isOpenedAttachment;
        public bool IsOpened => isOpened;

        private void Awake()
        {
            swiper.OnSwipeVertical += OnSwipe;
            closeButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            ItemHolderSelector.OnHide?.Invoke();
            CameraViewChanger.OnCameraViewChange?.Invoke(ItemType.None);
            Hide();
            GameAnalytics.NewDesignEvent("CLOSE_OPTION",0);
        }

        private void OnDestroy()
        {
            swiper.OnSwipeVertical -= OnSwipe;
        }

        private void OnSwipe(bool obj)
        {
            if (obj)
            {
                GameAnalytics.NewDesignEvent("CLOSE_OPTION",1);
                ItemHolderSelector.OnHide?.Invoke();
                CameraViewChanger.OnCameraViewChange?.Invoke(ItemType.None);
                Hide();
            }
            else
            {
                ShowLast();
            }
        }


        private void ShowLast()
        {
            
            if (isOpenedAttachment)
            {
                ShowAttachment();
            }
            else
            {
                Show();
            }
        }

        public void ShowAttachment()
        {
            isOpenedAttachment = true;
            rectTransform.sizeDelta = Vector2.up*heightAttachment;
            
            foreach (GameObject uiObject in gameObjectList)
            {
                uiObject.transform.localScale=Vector3.one;
                
            }
        }

        public void Show()
        {
            isOpenedAttachment = false;
            foreach (GameObject uiObject in gameObjectList)
            {
                uiObject.transform.localScale=Vector3.one;
                
            }
            rectTransform.sizeDelta = Vector2.up*height;
            isOpened = true;
        }
        
        public void Hide()
        {
          
            rectTransform.sizeDelta = Vector2.up*minHeight;
            foreach (GameObject uiObject in gameObjectList)
            {
                uiObject.transform.localScale=Vector3.zero;
                
            }
            isOpened = false;
        }
    }
}

