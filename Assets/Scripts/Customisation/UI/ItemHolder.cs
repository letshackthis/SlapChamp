using System.Collections.Generic;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private ItemType equipItemType;
        [SerializeField] private ItemType editItemType;

        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private Image glowImage;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button itemButtonHolder;

        private ItemData currentItemData;
        private List<ItemData> itemList;
        private AssetReferenceSprite[] iconDataList;

        public ItemType EquipItemType => equipItemType;

        public ItemType EditItemType => editItemType;

        public void Initialize(ItemData itemData, List<ItemData> itemDataList, AssetReferenceSprite[] iconDataListValue)
        {
            currentItemData = itemData;
            itemList = itemDataList;
            iconDataList = iconDataListValue;

            itemButtonHolder.onClick.AddListener(OpenItemList);
        }

        private void OpenItemList()
        {
            int selectedIndexItem = itemList.IndexOf(currentItemData);
            uiChannel.OnItemListOpen?.Invoke(itemList, iconDataList, selectedIndexItem);
        }
    }
}