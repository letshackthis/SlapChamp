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
        public Gender gender= Gender.Male;
        public Elements elements;
        public HeadCovering headCovering;

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

        public List<IndexDataOpened> openedItems = new List<IndexDataOpened>();
        
        public List<IndexData> selectedItems = new List<IndexData>()
        {
            new IndexData() {currentIndex = 0, itemType = ItemType.All_Elements},
            new IndexData() {currentIndex = 0, itemType = ItemType.No_Elements},
            new IndexData() {currentIndex = 0, itemType = ItemType.Eyebrows},
            new IndexData() {currentIndex = 0, itemType = ItemType.FacialHair},
            new IndexData() {currentIndex = 0, itemType = ItemType.Torso},
            new IndexData() {currentIndex = 0, itemType = ItemType.Arm_Upper_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Arm_Upper_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Arm_Lower_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Arm_Lower_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Hand_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Hand_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Hips},
            new IndexData() {currentIndex = 0, itemType = ItemType.Leg_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Leg_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Hair},
            new IndexData() {currentIndex = 0, itemType = ItemType.Head_Attachment},
            new IndexData() {currentIndex = 0, itemType = ItemType.Base_Hair},
            new IndexData() {currentIndex = 0, itemType = ItemType.No_FacialHair},
            new IndexData() {currentIndex = 0, itemType = ItemType.No_Hair},
            new IndexData() {currentIndex = 0, itemType = ItemType.Back_Attachment},
            new IndexData() {currentIndex = 0, itemType = ItemType.Shoulder_Attachment_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Shoulder_Attachment_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Elbow_Attachment_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Elbow_Attachment_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Hips_Attachment},
            new IndexData() {currentIndex = 0, itemType = ItemType.Knee_Attachement_Right},
            new IndexData() {currentIndex = 0, itemType = ItemType.Knee_Attachement_Left},
            new IndexData() {currentIndex = 0, itemType = ItemType.Elf_Ear},
        };
    }
}