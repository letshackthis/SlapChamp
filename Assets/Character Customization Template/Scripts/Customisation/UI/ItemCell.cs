using System;
using Customisation.SO;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class ItemCell : MonoBehaviour
    {
        [SerializeField] private Image glowImage;
        [SerializeField] private Image itemIcon;
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private Button buttonCell;
        [SerializeField] private ItemData itemData;
        private Sprite sprite;
        
        public void Initialize(ItemData itemDataValue, Sprite spriteValue)
        {
            itemData = itemDataValue;
            sprite = spriteValue;
            itemIcon.sprite = sprite;
            glowImage.color = characterChannel.GetCategoryColor(itemData.itemCategory);
            buttonCell.onClick.AddListener(EquipItem);
        }

        private void EquipItem()
        {
            characterChannel.OnChangeItem?.Invoke(itemData);
        }

        public void DestroyCell()
        {
            Destroy(gameObject);
        }
    }
}
