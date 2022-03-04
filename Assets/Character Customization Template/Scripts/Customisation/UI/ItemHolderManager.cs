using System;
using System.Collections.Generic;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation.UI
{
    public enum ItemHolderType
    {
        EquipHolder,
        EditHolder,
        AttachmentHolder,
    }

    [Serializable]
    public class ItemHolderDataType
    {
        [SerializeField] private ItemHolderType itemHolderType;
        [SerializeField] private GameObject parentHolder;

        public ItemHolderType HolderType => itemHolderType;

        public GameObject ParentHolder => parentHolder;
    }

    public class ItemHolderManager : MonoBehaviour
    {
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private ItemHolder[] itemHolders;
        [SerializeField] private IconItemsContainer iconItemsContainer;
        [SerializeField] private List<ItemHolderDataType> itemHolderDataTypeList;
        private CharacterLoadSave characterLoadSave;

        private void Awake()
        {
            characterChannel.OnLoadCharacterData += LoadCharacterData;
            characterChannel.OnItemHolderChange += ChangeItemHolder;
        }

        private void OnDestroy()
        {
            characterChannel.OnLoadCharacterData -= LoadCharacterData;
            characterChannel.OnItemHolderChange -= ChangeItemHolder;
        }

        private void LoadCharacterData(CharacterLoadSave characterSave)
        {
            characterLoadSave = characterSave;

            InitializeItemHolder();
            ChangeItemHolder(characterChannel.DefaultOpenItemHolderType);
        }

        private void ChangeItemHolder(ItemHolderType itemHolderType)
        {
            if (itemHolderType != ItemHolderType.AttachmentHolder)
            {
                foreach (ItemHolderDataType itemHolderDataType in itemHolderDataTypeList)
                {
                    itemHolderDataType.ParentHolder.gameObject.SetActive(false);
                }

                ItemHolderDataType current = itemHolderDataTypeList.Find(e => e.HolderType == itemHolderType);
                current.ParentHolder.gameObject.SetActive(true);
            }
           
        }

        private void InitializeItemHolder()
        {
            foreach (ItemHolder itemHolder in itemHolders)
            {
                InitializeHolderDataList(itemHolder.HolderDataList);
                InitializeHolderDataList(itemHolder.AttachmentList);
                itemHolder.Initialize();
            }
        }


        private void InitializeHolderDataList(List<HolderData> itemHolder)
        {
            foreach (HolderData holderData in itemHolder)
            {
                IndexData currentIndexData = characterLoadSave.SelectedItemList.Find(e => e.itemType == holderData.CurrentItemType);
                ItemTypeData currentItemsOfType = GetItemsOfType(holderData.CurrentItemType);
                   Sprite[] sprites = iconItemsContainer.GetIconList(characterLoadSave.CharacterSaveData.gender, holderData.CurrentItemType);
               
                holderData.Initialize(currentIndexData.currentIndex, currentItemsOfType.itemDataList, sprites);
            }
        }


        private ItemTypeData GetItemsOfType(ItemType currentItemType)
        {
            ItemTypeData currentItemsOfType = characterLoadSave.CurrentItems.Find(e => e.itemType == currentItemType) ??
                                              characterLoadSave.AllGenderItems.Find(e => e.itemType == currentItemType);
            return currentItemsOfType;
        }
    }
}