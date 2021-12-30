using System;
using System.Collections.Generic;
using System.Linq;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation
{
    public class CharacterLoadSave : MonoBehaviour
    {
        [SerializeField] private CharacterCustomisationItems customisationItems;

        private CharacterSaveData characterSaveData = new CharacterSaveData();
        private List<ItemTypeData> currentItemType = new List<ItemTypeData>();

        private void Awake()
        {
            Load();
        }

        private void Load()
        {
            DisableAllElements();
            
            characterSaveData = ES3.Load(SaveKeys.CharacterCustomisation, characterSaveData);
            currentItemType = characterSaveData.gender == Gender.Female ? customisationItems.FemaleItemTypeDataList : customisationItems.MaleItemTypeDataList;
            
         
            foreach (ItemTypeData itemTypeData in currentItemType)
            {
                Debug.Log(itemTypeData.itemType);
                Debug.Log(CurrentIndex(itemTypeData.itemType));
                ItemDataFromList(currentItemType, itemTypeData.itemType)[CurrentIndex(itemTypeData.itemType)].item.SetActive(true);
            }
            
            foreach (ItemTypeData itemTypeData in customisationItems.AllGenderItemTypeDataList)
            {
                ItemDataFromList(customisationItems.AllGenderItemTypeDataList, itemTypeData.itemType)[CurrentIndex(itemTypeData.itemType)].item.SetActive(true);
            }

            if (characterSaveData.elements == Elements.No)
            {
                ItemDataFromList(currentItemType, ItemType.All_Elements)[CurrentIndex(ItemType.All_Elements)].item.SetActive(false);
                ItemDataFromList(currentItemType, ItemType.No_Elements)[CurrentIndex(ItemType.No_Elements)].item.SetActive(true);
            }
            else
            {
                ItemDataFromList(currentItemType, ItemType.All_Elements)[CurrentIndex(ItemType.All_Elements)].item.SetActive(true);
                ItemDataFromList(currentItemType, ItemType.No_Elements)[CurrentIndex(ItemType.No_Elements)].item.SetActive(false);
            }
        }

        private int CurrentIndex( ItemType itemType)
        {
            return characterSaveData.indexDataList.Find(e => e.itemType == itemType).currentIndex;
        }
        private List<ItemData> ItemDataFromList(List<ItemTypeData>  itemTypeDataList,ItemType itemType)
        {
            foreach (ItemTypeData itemTypeData in itemTypeDataList)
            {
                if (itemTypeData.itemType == itemType)
                {
                    return itemTypeData.itemDataList;
                }
            }
            
            return null;
            
        }

        private void DisableAllElements()
        {
            foreach (ItemData itemData in customisationItems.FemaleItemTypeDataList.SelectMany(itemTypeData =>
                itemTypeData.itemDataList))
            {
                itemData.item.SetActive(false);
            }

            foreach (ItemData itemData in customisationItems.AllGenderItemTypeDataList.SelectMany(itemTypeData =>
                itemTypeData.itemDataList))
            {
                itemData.item.SetActive(false);
            }

            foreach (ItemData itemData in customisationItems.MaleItemTypeDataList.SelectMany(itemTypeData =>
                itemTypeData.itemDataList))
            {
                itemData.item.SetActive(false);
            }
        }

        public void Save()
        {
            
        }
    }
}