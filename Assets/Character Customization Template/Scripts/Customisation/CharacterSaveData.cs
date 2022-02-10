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
    public class IndexDataOpened
    {
        public ItemType itemType;
        public List<int> itemIndexList= new List<int>();
    }
    
    [Serializable]
    public class CharacterSaveData
    {
        public string nickName;
        public Gender gender;
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