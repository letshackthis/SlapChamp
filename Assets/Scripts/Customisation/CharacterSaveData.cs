using System;
using System.Collections.Generic;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation
{
    [Serializable]
  public class IndexData
  {
      public ItemType itemType;
      public int currentIndex;

  }
    [Serializable]
    public class CharacterSaveData
    {
        public Gender gender;
        public Elements elements;
        public HeadCovering headCovering;
        public List<IndexData> indexDataList= new List<IndexData>();
        
        public Color colorPrimary;
        public Color colorSecondary;
        public Color colorLeatherPrimary;
        public Color colorLeatherSecondary;
        public Color colorMetalMetal;
        public Color colorMetalSecondary;
        public Color colorMetalDark;
        public Color colorHair;
        public Color colorSkin;
        public Color colorStubble;
        public Color colorScar;
        public Color colorBodyArt;
        public Color colorEyes;
    }
}