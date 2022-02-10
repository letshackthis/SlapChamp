using System;
using System.Collections.Generic;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Customisation.SO
{
    [Serializable]
    public class ItemIconData
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private Sprite[] sprites;

        public ItemType ItemType1 => itemType;

        public Sprite[] Sprites => sprites;
    }
    
    [CreateAssetMenu(fileName ="Icon Items Container", menuName = "Customisation/Icon Items Container", order = 0)]
    public class IconItemsContainer : ScriptableObject
    {
        [SerializeField] private ItemIconData[] maleIcons;
        [SerializeField] private ItemIconData[] femaleIcons;
        [SerializeField] private ItemIconData[] allGenderIcons;

        public ItemIconData[] MaleIcons => maleIcons;

        public ItemIconData[] FemaleIcons => femaleIcons;

        public ItemIconData[] AllGenderIcons => allGenderIcons;


        public Sprite[] GetIconList(Gender characterGender, ItemType itemType)
        {
            List<ItemIconData> currentIconList;

            ItemIconData[] iconDatas = characterGender == Gender.Male ? maleIcons : femaleIcons;
            ItemIconData currenIconContainer = Array.Find(iconDatas, e => e.ItemType1 == itemType);
            if (currenIconContainer == null)
            {
                string itemString = itemType.ToString();

                if (itemString.Contains("Left"))
                {
                    itemString = itemString.Replace("Left", "Right");
                }
                currenIconContainer = Array.Find(allGenderIcons, e => e.ItemType1.ToString().Equals(itemString));
            }
            

            return currenIconContainer.Sprites;

        }
        
    }
}
