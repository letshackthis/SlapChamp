using System.Collections.Generic;
using Customisation.SO;
using Customisation.UI;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation
{
    public static class SaveKeys
    {
        public static readonly string CharacterCustomisation = "ChCu";
        public static readonly string SelectedItems = "SelectedItems";
        public static readonly string OpenedItems = "OpenedItems";
        public static readonly string UnlockAds = "UnlockAds";
        public static readonly string Money = "money";
        public static readonly string Blueprint = "blueprint";
    }

    public class CharacterLoadSave : MonoBehaviour
    {
        [SerializeField] private CharacterCustomisationItems customisationItems;
        [SerializeField] private CharacterCustomization characterCustomization;
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private List<IndexData> selectedItemList;

        private List<IndexDataOpened> openedElementList = new List<IndexDataOpened>();
        private CharacterSaveData characterSaveData = new CharacterSaveData();
        private List<ItemTypeData> currentItemType = new List<ItemTypeData>();
        public List<IndexData> SelectedItemList => selectedItemList;
        public List<IndexDataOpened> OpenedElementList => openedElementList;

        public List<ItemTypeData> CurrentItems => currentItemType;
        public List<ItemTypeData> AllGenderItems => customisationItems.AllGenderItemTypeDataList;

        public IndexData TemporarySelectedItem => temporarySelectedItem;

        public CharacterSaveData CharacterSaveData => characterSaveData;

        private IndexData temporarySelectedItem;
        private ItemHolderType currentItemHolderType;

        private void Awake()
        {
            currentItemHolderType = characterChannel.DefaultOpenItemHolderType;
          
            characterChannel.OnOpenItem += OnOpenItem;
            characterChannel.OnSelectItem += OnSelectItem;
            characterChannel.OnItemListOpen += OnItemListOpen;
            characterChannel.OnItemListClose += OnItemListClose;
            characterChannel.OnItemHolderChange += OnItemHolderChange;
            characterChannel.OnChangeGender += OnChangeGender;
            characterChannel.OnSaveCharacter += OnSaveCharacter;
        }

        private void OnSaveCharacter()
        {
            Debug.Log("SaveLoad");
            AddDefaultSelectedItems();
            ES3.Save(SaveKeys.OpenedItems, openedElementList);
            ES3.Save(SaveKeys.SelectedItems, selectedItemList);
            ES3.Save(SaveKeys.CharacterCustomisation, characterSaveData);
            Debug.Log(characterSaveData.gender);
        }


        private void OnItemHolderChange(ItemHolderType obj)
        {
            if (currentItemHolderType == obj) return;
            currentItemHolderType = obj;
            foreach (IndexData selectedItem2 in selectedItemList)
            {
                characterCustomization.ActivateItem(selectedItem2.itemType, selectedItem2.currentIndex);
            }
        }

        private void OnDestroy()
        {
            characterChannel.OnOpenItem -= OnOpenItem;
            characterChannel.OnSelectItem -= OnSelectItem;
            characterChannel.OnItemListOpen -= OnItemListOpen;
            characterChannel.OnItemListClose -= OnItemListClose;
            characterChannel.OnItemHolderChange -= OnItemHolderChange;
            characterChannel.OnChangeGender -= OnChangeGender;
            characterChannel.OnSaveCharacter -= OnSaveCharacter;
        }


        private void Start()
        {
            Load();
            characterChannel.OnLoadCharacterData?.Invoke(this);
        }

        private void OnChangeGender(bool isMale)
        {
            characterSaveData.gender = isMale ? Gender.Male : Gender.Female;
            characterCustomization.DisableAllElements();
            
            Debug.Log(characterSaveData.gender);
            currentItemType = characterSaveData.gender == Gender.Female
                ? customisationItems.FemaleItemTypeDataList
                : customisationItems.MaleItemTypeDataList;

            characterCustomization.currentItems = currentItemType;

            foreach (IndexData selectedItem in selectedItemList)
            {
                characterCustomization.ActivateItem(selectedItem.itemType, selectedItem.currentIndex);
            }
            characterChannel.OnLoadCharacterData?.Invoke(this);
        }

        public void ChangeItem(ItemType itemType, int indexItem)
        {
            temporarySelectedItem ??= new IndexData();
            temporarySelectedItem.itemType = itemType;
            temporarySelectedItem.currentIndex = indexItem;
            characterChannel.temporaryIndexData = temporarySelectedItem;
        }

        private void OnItemListOpen(HolderData holderData)
        {
            if (temporarySelectedItem != null)
            {
                ItemTypeData currentItem = currentItemType.Find(e => e.itemDataList.Contains(holderData.ItemList[holderData.CurrentItemData])) ??
                                           customisationItems.AllGenderItemTypeDataList.Find(e =>
                                               e.itemDataList.Contains(holderData.ItemList[holderData.CurrentItemData]));

                IndexData selectedItem = selectedItemList.Find(e => e.itemType == currentItem.itemType);
                temporarySelectedItem.currentIndex = selectedItem.currentIndex;
                temporarySelectedItem.itemType = selectedItem.itemType;
            }
        }

        private void OnItemListClose()
        {
            if (temporarySelectedItem != null)
            {
                IndexData selectedItem = selectedItemList.Find(e => e.itemType == temporarySelectedItem.itemType);

                if (temporarySelectedItem.currentIndex != selectedItem.currentIndex)
                {
                    temporarySelectedItem.currentIndex = selectedItem.currentIndex;
                    
                    foreach (IndexData selectedItem2 in selectedItemList)
                    {
                        characterCustomization.ActivateItem(selectedItem2.itemType, selectedItem2.currentIndex);
                    }
                    //characterCustomization.ActivateItem(selectedItem.itemType, selectedItem.currentIndex);
                }
            }
        }

        private void OnOpenItem()
        {
            IndexDataOpened indexDataOpened = openedElementList.Find(e => e.itemType == temporarySelectedItem.itemType);
            indexDataOpened.itemIndexList.Add(temporarySelectedItem.currentIndex);
            ES3.Save(SaveKeys.OpenedItems, openedElementList);
            OnSelectItem();
        }

        private void OnSelectItem()
        {
            if (temporarySelectedItem != null)
            {
                IndexData selectedItem = selectedItemList.Find(e => e.itemType == temporarySelectedItem.itemType);
                selectedItem.currentIndex = temporarySelectedItem.currentIndex;
                characterChannel.OnItemSelect?.Invoke(temporarySelectedItem);
                ES3.Save(SaveKeys.SelectedItems, selectedItemList);
            }
        }

        private void Load()
        {
            AddDefaultSelectedItems();

            characterSaveData = ES3.Load(SaveKeys.CharacterCustomisation, characterSaveData);
            selectedItemList = ES3.Load(SaveKeys.SelectedItems, selectedItemList);
            openedElementList = ES3.Load(SaveKeys.OpenedItems, openedElementList);

            currentItemType = characterSaveData.gender == Gender.Female
                ? customisationItems.FemaleItemTypeDataList
                : customisationItems.MaleItemTypeDataList;

            characterCustomization.currentItems = currentItemType;

            foreach (IndexData selectedItem in selectedItemList)
            {
                characterCustomization.ActivateItem(selectedItem.itemType, selectedItem.currentIndex);
            }
        }

        private void AddDefaultSelectedItems()
        {
            foreach (IndexData selectedItem in selectedItemList)
            {
                IndexDataOpened currentOpenedItemsType = new IndexDataOpened()
                {
                    itemType = selectedItem.itemType,
                    itemIndexList = new List<int>() {selectedItem.currentIndex}
                };

                openedElementList.Add(currentOpenedItemsType);
            }
        }
    }
}