using System;
using System.Linq;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

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
        
        private CharacterSaveData characterSaveData;
        
        private void Awake()
        {
            uiChannel.OnLoadCharacterData+= LoadCharacterData;
        }

        private void LoadCharacterData(CharacterSaveData characterSave, ItemHolderType itemHolderType)
        {
            characterSaveData = characterSave;
            InitializeItemHolder(itemHolderType);
        }

        public void InitializeItemHolder(ItemHolderType itemHolderType)
        {
            switch (itemHolderType)
            {
                case ItemHolderType.EquipHolder:
                    
                    foreach (ItemHolder itemHolder in itemHolders)
                    {
                        IndexData currentItemData = characterSaveData.indexDataList.Find(e => e.itemType == itemHolder.EquipItemType);
                    }
                
                    break;
                case ItemHolderType.EditHolder:
                    foreach (ItemHolder itemHolder in itemHolders)
                    {
                        IndexData currentItemData = characterSaveData.indexDataList.Find(e => e.itemType == itemHolder.EditItemType);
                    }
                    break;
            }
        }

        private void OnDestroy()
        {
            uiChannel.OnLoadCharacterData -= LoadCharacterData;
        }
    }
}
