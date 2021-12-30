using System.Collections.Generic;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private ItemType equipItemType;
        [SerializeField] private ItemType editItemType;
        
        [SerializeField] private Image glowImage;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button itemButtonHolder;

        private ItemData currentItemData;
        private List<ItemData> itemList= new List<ItemData>();

        public ItemType EquipItemType => equipItemType;

        public ItemType EditItemType => editItemType;

        public void Initialize()
        {
            
        }

        public void Show()
        {
            
        }
    }
}
