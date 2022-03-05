using System;
using System.Collections.Generic;
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
      
        private bool isOpened;
        private bool isOpenedAttachment;
        public bool IsOpened => isOpened;

        private void Awake()
        {
            swiper.OnSwipeVertical += OnSwipe;
        }

        private void OnDestroy()
        {
            swiper.OnSwipeVertical -= OnSwipe;
        }

        private void OnSwipe(bool obj)
        {
            if (obj)
            {
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

