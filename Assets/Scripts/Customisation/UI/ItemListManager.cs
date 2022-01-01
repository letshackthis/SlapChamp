using System.Collections.Generic;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Customisation.UI
{
    public class ItemListManager : MonoBehaviour
    {
        [SerializeField] private ItemListController itemListController;
        [SerializeField] private ItemCell itemCellPrefab;
        [SerializeField] private Transform content;
        [SerializeField] private UIChannel uiChannel;

        private List<ItemData> itemList;
        private AssetReferenceSprite[] iconDataList;
        private int indexSelectedItem;
        private ItemHolderType currentItemHolderType;

        private List<ItemCell> temporaryItemCellList = new List<ItemCell>();

        private void Awake()
        {
            currentItemHolderType = uiChannel.DefaultOpenItemHolderType;
            uiChannel.OnItemListOpen += OnItemListOpen;
            uiChannel.OnItemHolderChange += OnItemHolderChange;
            itemListController.Hide();
        }

        private void OnItemHolderChange(ItemHolderType obj)
        {
            if (currentItemHolderType == obj) return;
            
            currentItemHolderType = obj;
            DestroyPreviousCells();
            itemListController.Hide();

        }

        private void OnDisable()
        {
            uiChannel.OnItemHolderChange -= OnItemHolderChange;
            uiChannel.OnItemListOpen -= OnItemListOpen;
        }

        private void OnItemListOpen(List<ItemData> itemDataValue, AssetReferenceSprite[] sprites, int selectedItem)
        {
            itemList = itemDataValue;
            iconDataList = sprites;
            indexSelectedItem = selectedItem;
            InstantiateItemCells();
            itemListController.Show();
        }

        private void InstantiateItemCells()
        {
            DestroyPreviousCells();

            for (int index = 0; index < itemList.Count; index++)
            {
                ItemData item = itemList[index];
                ItemCell currentItemCell = Instantiate(itemCellPrefab, content);
             //   AssetReferenceSprite currentItemIndex = iconDataList[index];
                currentItemCell.Initialize(item, null);
                
                temporaryItemCellList.Add(currentItemCell);
            }

            ConfigSelectedItem();
        }

        private void ConfigSelectedItem()
        {
            ItemCell currentItemCell = temporaryItemCellList[indexSelectedItem];
        }

        private void DestroyPreviousCells()
        {
            foreach (ItemCell itemCell in temporaryItemCellList)
            {
                itemCell.DestroyCell();
            }

            temporaryItemCellList.Clear();
        }
    }
}