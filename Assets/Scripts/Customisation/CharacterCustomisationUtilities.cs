using System.Collections.Generic;
using System.Linq;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Customisation
{
    public class CharacterCustomisationUtilities : MonoBehaviour
    {
        [SerializeField] private CharacterCustomisationItems characterCustomisationItems;
        [SerializeField] private GameObject characterReferences;
        
        private string noElement = "No_Elements";
        private string HeadCoverings_Base_Hair = "HeadCoverings_Base_Hair";
        private string HeadCoverings_No_FacialHair = "HeadCoverings_No_FacialHair";
        private string HeadCoverings_No_Hair = "HeadCoverings_No_Hair";

        private string[] malePartList =
        {
            "Male_Head_All_Elements",
            "Male_Head_No_Elements",
            "Male_01_Eyebrows",
            "Male_02_FacialHair",
            "Male_03_Torso",
            "Male_04_Arm_Upper_Right",
            "Male_05_Arm_Upper_Left",
            "Male_06_Arm_Lower_Right",
            "Male_07_Arm_Lower_Left",
            "Male_08_Hand_Right",
            "Male_09_Hand_Left",
            "Male_10_Hips",
            "Male_11_Leg_Right",
            "Male_12_Leg_Left",
        };

        private string[] femalePartList =
        {
            "Female_Head_All_Elements",
            "Female_Head_No_Elements",
            "Female_01_Eyebrows",
            "Female_02_FacialHair",
            "Female_03_Torso",
            "Female_04_Arm_Upper_Right",
            "Female_05_Arm_Upper_Left",
            "Female_06_Arm_Lower_Right",
            "Female_07_Arm_Lower_Left",
            "Female_08_Hand_Right",
            "Female_09_Hand_Left",
            "Female_10_Hips",
            "Female_11_Leg_Right",
            "Female_12_Leg_Left",
        };

        private string[] allGenderPartList =
        {
            "All_01_Hair",
            "All_02_Head_Attachment",
            "HeadCoverings_Base_Hair",
            "HeadCoverings_No_FacialHair",
            "HeadCoverings_No_Hair",
            "All_03_Chest_Attachment",
            "All_04_Back_Attachment",
            "All_05_Shoulder_Attachment_Right",
            "All_06_Shoulder_Attachment_Left",
            "All_07_Elbow_Attachment_Right",
            "All_08_Elbow_Attachment_Left",
            "All_09_Hips_Attachment",
            "All_10_Knee_Attachement_Right",
            "All_11_Knee_Attachement_Left",
            "Elf_Ear"
        };


        public void CollectReference()
        {
            characterCustomisationItems.MaleItemTypeDataList.Clear();
            characterCustomisationItems.FemaleItemTypeDataList.Clear();
            characterCustomisationItems.AllGenderItemTypeDataList.Clear(); 
            
            foreach (string name in malePartList)
            {
                BuildList(characterCustomisationItems.MaleItemTypeDataList, name);
            }

            foreach (string name in femalePartList)
            {
                BuildList(characterCustomisationItems.FemaleItemTypeDataList, name);
            }

            foreach (string name in allGenderPartList)
            {
                BuildList(characterCustomisationItems.AllGenderItemTypeDataList, name);
            }
        }

        void BuildList(List<ItemTypeData> targetList, string characterPart)
        {
            EditorUtility.SetDirty(characterCustomisationItems);

            Transform[] rootTransform = characterReferences.GetComponentsInChildren<Transform>();
            Transform targetRoot = rootTransform.FirstOrDefault(t => t.gameObject.name == characterPart);

            ItemTypeData itemTypeData = new ItemTypeData();
            
            itemTypeData.name = characterPart;
            
            targetList.Add(itemTypeData);
            itemTypeData.itemDataList.Clear();

            for (int i = 0; i < targetRoot.childCount; i++)
            {
                GameObject item = targetRoot.GetChild(i).gameObject;

                ItemData currentItemData = new ItemData();

                currentItemData.item = item;
                currentItemData.openDefault = true;

                SetItemCondition(currentItemData, characterPart);

                itemTypeData.itemDataList.Add(currentItemData);
            }

            
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private void SetItemCondition(ItemData itemData, string characterPart)
        {
            itemData.elements = characterPart.Contains(noElement) ? Elements.No : Elements.Yes;

            if (characterPart.Contains(HeadCoverings_No_FacialHair))
            {
                itemData.headCovering = HeadCovering.HeadCoveringsNoFacialHair;
            }

            if (characterPart.Contains(HeadCoverings_Base_Hair))
            {
                itemData.headCovering = HeadCovering.HeadCoveringsBaseHair;
            }

            if (characterPart.Contains(HeadCoverings_No_Hair))
            {
                itemData.headCovering = HeadCovering.HeadCoveringsNoHair;
            }
        }
    }
}