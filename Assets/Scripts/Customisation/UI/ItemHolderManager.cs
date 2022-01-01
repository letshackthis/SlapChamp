using System.Collections.Generic;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Customisation.UI
{
    public enum ItemHolderType
    {
        EquipHolder,
        EditHolder,
    }

    public class ItemHolderManager : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private ItemHolder[] itemHolders;
        [SerializeField] private IconItemsContainer iconItemsContainer;

        private CharacterSaveData characterSaveData;
        private List<ItemTypeData> currentItems;
        private List<ItemTypeData> itemsUnisex;

        private void Awake()
        {
            uiChannel.OnLoadCharacterData += LoadCharacterData;
            uiChannel.OnItemHolderChange += InitializeItemHolder;
        }

        private void OnDisable()
        {
            uiChannel.OnLoadCharacterData -= LoadCharacterData;
            uiChannel.OnItemHolderChange -= InitializeItemHolder;
        }

        private void LoadCharacterData(CharacterSaveData characterSave, List<ItemTypeData> itemList,
            List<ItemTypeData> unisexItems)
        {
            characterSaveData = characterSave;
            itemsUnisex = unisexItems;
            currentItems = itemList;

            InitializeItemHolder(uiChannel.DefaultOpenItemHolderType);
        }

        public void InitializeItemHolder(ItemHolderType itemHolderType)
        {
            foreach (ItemHolder itemHolder in itemHolders)
            {
                ItemType currentItemType = itemHolderType == ItemHolderType.EquipHolder
                    ? itemHolder.EquipItemType
                    : itemHolder.EditItemType;

                InitializeItemHolderList(itemHolder, currentItemType);
            }
        }

        private void InitializeItemHolderList(ItemHolder itemHolder, ItemType currentItemType)
        {
            IndexData currentIndexData = characterSaveData.selectedItems.Find(e => e.itemType == currentItemType);

            ItemTypeData currentItemsOfType = GetItemsOfType(currentItemType);
            ItemData selectedItem = currentItemsOfType.itemDataList[currentIndexData.currentIndex];

            AssetReferenceSprite[] sprites = iconItemsContainer.GetIconList(characterSaveData.gender, currentItemType);
            itemHolder.Initialize(selectedItem, currentItemsOfType.itemDataList, sprites);
        }

        private ItemTypeData GetItemsOfType(ItemType currentItemType)
        {
            ItemTypeData currentItemsOfType = currentItems.Find(e => e.itemType == currentItemType) ??
                                              itemsUnisex.Find(e => e.itemType == currentItemType);
            return currentItemsOfType;
        }
    }
}