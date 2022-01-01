using System;
using System.Collections.Generic;
using Customisation.UI;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Customisation.SO
{
    [CreateAssetMenu(fileName = "UI Channel", menuName = "Channels/UI Channel", order = 0)]
    public class UIChannel : ScriptableObject
    {
        [SerializeField] private ItemHolderType defaultOpenItemHolderType;

        public Action<CharacterSaveData, List<ItemTypeData>, List<ItemTypeData>> OnLoadCharacterData;
        public Action<List<ItemData>, AssetReferenceSprite[], int> OnItemListOpen;
        public Action<ItemHolderType> OnItemHolderChange;

        public ItemHolderType DefaultOpenItemHolderType => defaultOpenItemHolderType;
    }
}