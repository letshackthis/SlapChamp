using System.Collections.Generic;
using System.Linq;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation
{
    public class CharacterCustomization : MonoBehaviour
    {
        [SerializeField] private CharacterCustomisationItems characterCustomisationItems;
        [SerializeField] private CharacterLoadSave characterLoadSave;
        [SerializeField] private CharacterChannel characterChannel;
        [HideInInspector] public List<ItemTypeData> currentItems;


        private ItemTypeData currentItemTypeData;
        private int currentItemIndex;
        private List<ItemType> possibleHideElementList = new List<ItemType>();

        private void Awake()
        {
            FindMaximHideList();

            characterChannel.OnChangeItem += OnChangeItem;

            DisableAllElements();
        }

        private void FindMaximHideList()
        {
            int max = 0;
            foreach (ItemTypeData itemTypeData in characterCustomisationItems.MaleItemTypeDataList)
            {
                foreach (ItemData itemData in itemTypeData.itemDataList)
                {
                    if (itemData.itemTypesHideList.Count > max)
                    {
                        max = itemData.itemTypesHideList.Count;
                        possibleHideElementList = itemData.itemTypesHideList;
                    }
                }
            }
        }

        private void OnChangeItem(ItemData item)
        {
            ItemTypeData currentItem = currentItems.Find(e => e.itemDataList.Contains(item)) ??
                                       characterCustomisationItems.AllGenderItemTypeDataList.Find(e =>
                                           e.itemDataList.Contains(item));
            
            int index = currentItem.itemDataList.IndexOf(item);

            if (!SameItemData(currentItem, index))
            {
                characterLoadSave.ChangeItem(currentItem.itemType, index);

                currentItemTypeData = currentItem;
                currentItemIndex = index;
                ActivateItem(currentItemTypeData.itemType, currentItemIndex);
            }
        }

        private bool SameItemData(ItemTypeData newItemData, int newIndex)
        {
            if (currentItemTypeData == null)
            {
                return false;
            }

            return currentItemTypeData.itemType == newItemData.itemType && newIndex == currentItemIndex;
        }

        private void OnDestroy()
        {
            characterChannel.OnChangeItem -= OnChangeItem;
        }

        public void ActivateItem(ItemType itemType, int itemIndex)
        {
            ItemTypeData currentItemsOfType = GetCurrentList(itemType);

            foreach (ItemData itemData in currentItemsOfType.itemDataList)
            {
                itemData.item.SetActive(false);
            }

            HideItems(currentItemsOfType.itemDataList[itemIndex], currentItemsOfType);
            currentItemsOfType.itemDataList[itemIndex].item.gameObject.SetActive(true);
        }

        private void HideItems(ItemData itemData, ItemTypeData currentItemsOfTypeValue)
        {
            foreach (ItemType itemType in itemData.itemTypesHideList)
            {
                SetItemState(itemType,false );
            }

            foreach (ItemType itemType in possibleHideElementList.Where(itemType => !itemData.itemTypesHideList.Contains(itemType)))
            {
                if (possibleHideElementList.Contains(currentItemsOfTypeValue.itemType) && itemType != currentItemsOfTypeValue.itemType)
                {
                    SetItemState(itemType,true );
                }
                else
                {
                    if (currentItems.Where(e => e.itemType != currentItemsOfTypeValue.itemType).All(itemTypeData => itemTypeData.itemDataList.
                            All(data => !data.itemTypesHideList.Contains(itemType))) && characterCustomisationItems.AllGenderItemTypeDataList
                            .Where(e => e.itemType != currentItemsOfTypeValue.itemType).All(itemTypeData => itemTypeData.itemDataList.All(data => !data.itemTypesHideList.Contains(itemType))))
                    {
                        SetItemState(itemType,true );
                    }
                }
            }
        }


        private void SetItemState(ItemType itemType, bool state)
        {
            ItemTypeData currentItemsOfType = GetCurrentList(itemType);
            int currentIndex = characterLoadSave.SelectedItemList.Find(e => e.itemType == itemType).currentIndex;
            currentItemsOfType.itemDataList[currentIndex].item.SetActive(state);
        }

        private ItemTypeData GetCurrentList(ItemType itemType)
        {
            ItemTypeData currentItemsOfType = currentItems.Find(e => e.itemType == itemType) ?? characterCustomisationItems.AllGenderItemTypeDataList.Find(
                                                  e =>
                                                      e.itemType == itemType);
            return currentItemsOfType;
        }

        
        public void DisableAllElements()
        {
            foreach (ItemData itemData in characterCustomisationItems.FemaleItemTypeDataList.SelectMany(itemTypeData =>
                itemTypeData.itemDataList))
            {
                itemData.item.SetActive(false);
            }

            foreach (ItemData itemData in characterCustomisationItems.AllGenderItemTypeDataList.SelectMany(
                itemTypeData =>
                    itemTypeData.itemDataList))
            {
                itemData.item.SetActive(false);
            }

            foreach (ItemData itemData in characterCustomisationItems.MaleItemTypeDataList.SelectMany(itemTypeData =>
                itemTypeData.itemDataList))
            {
                itemData.item.SetActive(false);
            }
        }
    }
}