using System;
using System.Collections.Generic;
using System.Linq;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Customisation.UI
{
    [Serializable]
    public class ButtonOptionData
    {
        [SerializeField] private ItemHolderType itemHolderType;
        [SerializeField] private string buyName;
        [SerializeField] private string selectName;
        [SerializeField] private string selectedName;


        public string BuyName => buyName;

        public string SelectName => selectName;

        public ItemHolderType HolderType => itemHolderType;

        public string SelectedName => selectedName;
    }

    public class ItemOptions : MonoBehaviour
    {
        [SerializeField] private List<ButtonOptionData> buttonOptionDataList;
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private Button buttonOption;
        [SerializeField] private Text buttonOptionText;

        private CharacterLoadSave characterSaveData;
        private ItemHolderType currentItemHolderType;
        private ItemData currentItemData;
        private int currentIndex;
        private Action onActivateCurrentOption;

        private void Awake()
        {
            buttonOption.onClick.AddListener(ActivateOption);
            characterChannel.OnLoadCharacterData += OnLoadCharacterData;
            characterChannel.OnItemListOpen += OnItemListOpen;
            characterChannel.OnChangeItem += OnChangeItem;
            characterChannel.OnItemHolderChange += OnItemHolderChange;
            characterChannel.OnUnlockOptionState += OnUnlockOptionState;
        }

        private void OnUnlockOptionState(bool status)
        {
            if (!status)
            {
                SetOption();
            }
        }

        private void OnDestroy()
        {
            characterChannel.OnLoadCharacterData -= OnLoadCharacterData;
            characterChannel.OnItemListOpen -= OnItemListOpen;
            characterChannel.OnChangeItem -= OnChangeItem;
            characterChannel.OnItemHolderChange -= OnItemHolderChange;
        }

        private void OnItemHolderChange(ItemHolderType obj)
        {
            currentItemHolderType = obj;
        }

        private void OnItemListOpen(HolderData holderData)
        {
            currentItemData = holderData.ItemList[holderData.CurrentItemData];
            currentIndex = holderData.CurrentItemData;
            SetOption();
        }

        private void OnChangeItem(ItemData obj)
        {
            currentItemData = obj;
            SetOption();
        }

        private void OnLoadCharacterData(CharacterLoadSave obj)
        {
            characterSaveData = obj;
        }

        private void ActivateOption()
        {
            onActivateCurrentOption?.Invoke();
        }

        private void SetOption()
        {
            Debug.Log(currentItemData);
            Debug.Log(currentItemData.item.name);
            ItemTypeData currentItem = characterSaveData.CurrentItems.Find(e => e.itemDataList.Contains(currentItemData)) ??
                                       characterSaveData.AllGenderItems.Find(e =>
                                           e.itemDataList.Contains(currentItemData));

            currentIndex = currentItem.itemDataList.IndexOf(currentItemData);



            if (IsSelected(currentItem.itemType))
            {
                onActivateCurrentOption = null;
                ShowOptionText(0);

            }
            else if (IsOpened(currentItem.itemType))
            {
                onActivateCurrentOption = SelectItem;
                ShowOptionText(1);
            }
            else
            {
                onActivateCurrentOption = BuyItem;
                ShowOptionText(2);
            }
        }


        private void ShowOptionText(int option)
        {
            switch (option)
            {
                case 0:
                {
                    buttonOptionText.text = buttonOptionDataList.Find(e => e.HolderType == currentItemHolderType)
                        .SelectedName;
                    break;
                }
                case 1:
                {
                    buttonOptionText.text = buttonOptionDataList.Find(e => e.HolderType == currentItemHolderType)
                        .SelectName;
                    break;
                }
                case 2:
                {
                    buttonOptionText.text = buttonOptionDataList.Find(e => e.HolderType == currentItemHolderType)
                        .BuyName;
                    break;
                }
            }
        }

        private bool IsSelected(ItemType itemType)
        {
            return characterSaveData.SelectedItemList.Any(e =>
                e.itemType == itemType && e.currentIndex == currentIndex);
        }

        private bool IsOpened(ItemType itemType)
        {
            return characterSaveData.OpenedElementList.Find(e => e.itemType == itemType).itemIndexList
                .Contains(currentIndex);
        }

        private void BuyItem()
        {
            characterChannel.OnUnlockOptionState?.Invoke(true);
        }

        private void SelectItem()
        {
            characterChannel.OnSelectItem?.Invoke();
            SetOption();
        }
    }
}