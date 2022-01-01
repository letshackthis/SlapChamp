using System;
using System.Collections.Generic;
using Customisation;
using PsychoticLab;
using UnityEngine;

namespace PolygonFantasyHeroCharacters.Scripts
{
    public enum Gender { Male, Female }
    public enum Race { Human, Elf }
    public enum SkinColor { White, Brown, Black, Elf }
    public enum Elements {  Yes, No }
    public enum HeadCovering { HeadCoveringsBaseHair, HeadCoveringsNoFacialHair, HeadCoveringsNoHair,None }
    public enum ItemCategory { None, One, Two, Three, Four, Five }
    public enum FacialHair { Yes, No }
    
    [Serializable]
    public class ItemTypeData
    {
        public ItemType itemType;
        public List<ItemData> itemDataList= new List<ItemData>();
    }

    [Serializable]
    public class ItemData
    {
        public GameObject item;

        public Elements elements;
        public HeadCovering headCovering= HeadCovering.None;
        public ItemCategory itemCategory = ItemCategory.None;
        public BuyOption[] BuyOptions;
    }

   
    
}