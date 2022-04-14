using System;
using System.Collections.Generic;
using System.Linq;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    [Serializable]
    public class HolderData
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private string name;

        private int selectedItemIndex;
        private List<ItemData> itemList = new List<ItemData>();
        private Sprite[] iconDataList;
        private ItemData currentItem;
        private Sprite currentSprite;

        public ItemType CurrentItemType => itemType;

        public int CurrentItemData => selectedItemIndex;

        public Sprite[] IconDataList => iconDataList;

        public ItemData CurrentItem => currentItem;

        public Sprite CurrentSprite => currentSprite;

        public List<ItemData> ItemList => itemList;

        public string Name => name;

        public void Initialize(int itemIndex, List<ItemData> itemDataList, Sprite[] iconDataListValue)
        {
            
            selectedItemIndex = itemIndex;
            itemList = itemDataList;
            iconDataList = iconDataListValue;

            currentItem = itemDataList[itemIndex];
            currentSprite = iconDataListValue[itemIndex];
        }

        public void ChangeCurrentHolderData(int itemIndex)
        {
            selectedItemIndex = itemIndex;
            currentItem = itemList[itemIndex];
            currentSprite = iconDataList[itemIndex];
        }
    }

    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private List<HolderData> holderDataList;
        [SerializeField] private List<HolderData> attachmentList;
        [SerializeField] private Swiper swiper;
        [SerializeField] private PointerSelection pointerPrefab;
        [SerializeField] private Transform contentPointerSelection;
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private Image glowImage;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button itemButtonHolder;

        private HolderData currentHolderData;
        private HolderData currentAttachmentHolderData;
        private readonly List<PointerSelection> pointerSelectionList = new List<PointerSelection>();
        private int indexHolder = 0;
        private int indexAttachment = 0;

        public List<HolderData> HolderDataList => holderDataList;

        public HolderData CurrentAttachmentHolderData => currentAttachmentHolderData;

        public List<HolderData> AttachmentList => attachmentList;

        public HolderData CurrentHolderData => currentHolderData;
        private bool isInitielizedSwiper;


        public void Initialize()
        {
            currentHolderData = holderDataList[0];
            if (holderDataList.Count > 0)
            {
                InitializeSwiper();
                SetCurrentHolderData();
                itemButtonHolder.onClick.AddListener(ShowItemList);
            }

            if (attachmentList.Count > 0)
            {
                SetCurrentAttachment(0);
            }
            characterChannel.OnItemSelect+= OnItemSelect;
        }

        private void OnDestroy()
        {
            characterChannel.OnItemSelect = null;
            swiper.OnSwipeVertical = null;
        }

        private void OnItemSelect(IndexData indexData)
        {
            if (indexData.itemType == currentHolderData.CurrentItemType)
            {
                currentHolderData.ChangeCurrentHolderData(indexData.currentIndex);
                SetCurrentHolderData();
            }
        }

        private void InitializeSwiper()
        {
            if (holderDataList.Count > 1)
            {
                InstantiatePointerList();
                if (!isInitielizedSwiper)
                {
                    isInitielizedSwiper = true;
                    swiper.OnSwipeVertical += ChangeItemType;
                }
               
                SetPointerListState();
            }
            else
            {
                swiper.enabled = false;
                contentPointerSelection.gameObject.SetActive(false);
            }
        }
        
        
        private void InstantiatePointerList()
        {
            for (var i = 0; i < pointerSelectionList.Count; i++)
            {
                Destroy(pointerSelectionList[i].gameObject);
            }
            
            pointerSelectionList.Clear();
            foreach (PointerSelection currentPointer in holderDataList.Select(holderData => Instantiate(pointerPrefab, contentPointerSelection)))
            {
                pointerSelectionList.Add(currentPointer);
            }
        }

        private void SetCurrentHolderData()
        {
            currentHolderData = holderDataList[indexHolder];
            itemIcon.sprite = currentHolderData.CurrentSprite;
            glowImage.color = characterChannel.GetCategoryColor(currentHolderData.CurrentItem.itemCategory);

        }

        public void SetCurrentAttachment(int index)
        {
            indexAttachment = index;
            currentAttachmentHolderData = attachmentList[indexAttachment];
        }

        private void SetPointerListState()
        {
            foreach (PointerSelection pointerSelection in pointerSelectionList)
            {
                pointerSelection.PointerStateSelection(false);
            }
            
            pointerSelectionList[indexHolder].PointerStateSelection(true);
        }

        private void ShowItemList()
        {
            Debug.Log("ShowItemList");
            characterChannel.OnItemListClose?.Invoke();
            characterChannel.OnItemHolderSelect?.Invoke(this);
            CameraViewChanger.OnCameraViewChange?.Invoke(currentHolderData.CurrentItemType);
            ItemHolderSelector.OnSelect?.Invoke(transform);
            characterChannel.OnItemListOpen?.Invoke(currentHolderData);
        }
        private void ChangeItemType(bool direction)
        {
            if (!direction)
            {
                if (indexHolder < holderDataList.Count-1)
                {
                    indexHolder++;
                    SetPointerListState();
                    SetCurrentHolderData();
                    ShowItemList();
                } 
            }
            else
            {
                if (indexHolder > 0)
                {
                    indexHolder--;
                    SetPointerListState();
                    SetCurrentHolderData();
                    ShowItemList();
                } 
            }
        }

        public void SelectItem(ItemData itemData)
        {
            
        }
    }
}