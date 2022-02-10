using System;
using Customisation.UI;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation.SO
{
    [Serializable]
    public class ItemCategoryData
    {
        [SerializeField] private Color categoryColor;
        [SerializeField] private ItemCategory itemCategory;

        public ItemCategory Category => itemCategory;

        public Color CategoryColor => categoryColor;
    }
    
    [CreateAssetMenu(fileName = "UI Channel", menuName = "Channels/UI Channel", order = 0)]
    public class CharacterChannel : ScriptableObject
    {
        [SerializeField] private ItemHolderType defaultOpenItemHolderType;
        [SerializeField] private ItemCategoryData[] categoryDatas; 
        public Action<CharacterLoadSave> OnLoadCharacterData;
        public Action<ItemData> OnChangeItem;
        public Action<HolderData> OnItemListOpen;
        public Action<IndexData> OnItemSelect;
        public Action OnItemListClose;
        
        public Action<ItemHolder> OnItemHolderSelect;
        public Action<ItemHolderType> OnItemHolderChange;
        public Action<bool> OnUnlockOptionState;

        public Action OnOpenItem;
        public Action OnSelectItem;

        public ItemHolderType DefaultOpenItemHolderType => defaultOpenItemHolderType;


        public Color GetCategoryColor(ItemCategory category)
        {
            ItemCategoryData currentCategory = Array.Find(categoryDatas, e => e.Category == category);

            return currentCategory.CategoryColor;
        }
    }
}