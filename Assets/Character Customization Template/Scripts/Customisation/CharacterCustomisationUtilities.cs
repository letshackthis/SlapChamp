#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random; //using UnityEditor;

namespace Customisation
{
    [Serializable]
    public class OptionBuy
    {
        public ItemCategory itemCategory;
        public UnlockItem[] unlockItemList;
    }
    public class CharacterCustomisationUtilities : MonoBehaviour
    {
        [SerializeField] private List<GameObject> currentList;
        [SerializeField] private CharacterCustomisationItems characterCustomisationItems;
        [SerializeField] private GameObject characterReferences;
        [SerializeField] private OptionBuy[] unlockItemList;
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

        private int[] per =  {20, 25, 30, 15, 10};

        public void DestroyListObjects()
        {
            EditorUtility.SetDirty(this);
            foreach (GameObject o in currentList)
            {
                if (o.activeSelf == false)
                {
                    DestroyImmediate(o);
                }
            }
            currentList.Clear();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        
        public void SetBuyOption()
        {
            EditorUtility.SetDirty(characterCustomisationItems);

            SetBuyOptionToList(characterCustomisationItems.MaleItemTypeDataList);
            SetBuyOptionToList(characterCustomisationItems.FemaleItemTypeDataList);
            SetBuyOptionToList(characterCustomisationItems.AllGenderItemTypeDataList);

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private void SetBuyOptionToList(List<ItemTypeData> currentList)
        {
            foreach (ItemTypeData typeData in currentList)
            {
                
                foreach (ItemData itemData in typeData.itemDataList)
                {
                    
                    OptionBuy currentUnlock = Array.Find(unlockItemList, e => e.itemCategory == itemData.itemCategory);

                    if (currentUnlock == null)
                    {
                        Debug.Log(typeData.itemType+" "+itemData.item.name);
                    }
                    else
                    {
                        itemData.buyOptions = currentUnlock.unlockItemList;
                    }
                   
                }
            }
            
        }
        public void SetRandomCategory()
        {
            EditorUtility.SetDirty(characterCustomisationItems);

            SetRandomToList(characterCustomisationItems.MaleItemTypeDataList);
            SetRandomToList(characterCustomisationItems.FemaleItemTypeDataList);
            SetRandomToList(characterCustomisationItems.AllGenderItemTypeDataList);

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private void SetRandomToList(List<ItemTypeData> currentList)
        {
            foreach (ItemTypeData typeData in currentList)
            {
                List<ItemData> temItemDataList= new List<ItemData>();
                
                foreach (ItemData itemData in typeData.itemDataList)
                {
                    temItemDataList.Add(itemData);
                }

                int min = 0;
                int max = temItemDataList.Count;

                List<int> iterationNumber= new List<int>();
                
                for (var i = 0; i < per.Length; i++)
                {
                    int value =  Mathf.RoundToInt( per[i] * (max - min) / 100 + min);
                    iterationNumber.Add(value);
                }
                
                for (var i = 0; i < iterationNumber.Count; i++)
                {

                    for (int j = 0; j < iterationNumber[i]; j++)
                    {
                        Check(i, j, temItemDataList);
                    }
                    
                }
            }
        }
        private void Check(int i,int j,List<ItemData> temItemDataList)
        {
            if (i == 0 && j == 0)
            {
                ItemData currentItem = temItemDataList[0];
                currentItem.itemCategory = ItemCategory.One;
                temItemDataList.Remove(currentItem);
            }
            else if (i == 0 && j != 0)
            {
                ItemData currentItem = temItemDataList[Random.Range(0,temItemDataList.Count)];
                currentItem.itemCategory = ItemCategory.One;
                temItemDataList.Remove(currentItem);
            }
            else if (i == 1 )
            {
                ItemData currentItem = temItemDataList[Random.Range(0,temItemDataList.Count)];
                currentItem.itemCategory = ItemCategory.Two;
                temItemDataList.Remove(currentItem);
            }
            else if (i == 2 )
            {
                ItemData currentItem = temItemDataList[Random.Range(0,temItemDataList.Count)];
                currentItem.itemCategory = ItemCategory.Three;
                temItemDataList.Remove(currentItem);
            }
            else if (i == 3 )
            {
                ItemData currentItem = temItemDataList[Random.Range(0,temItemDataList.Count)];
                currentItem.itemCategory = ItemCategory.Four;
                temItemDataList.Remove(currentItem);
            }
            else if (i == 4 )
            {
                ItemData currentItem = temItemDataList[Random.Range(0,temItemDataList.Count)];
                currentItem.itemCategory = ItemCategory.Five;
                temItemDataList.Remove(currentItem);
            }
        }
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

        public void SetElementsCategory()
        {
            EditorUtility.SetDirty(characterCustomisationItems);
            
            foreach (ItemTypeData typeData in characterCustomisationItems.MaleItemTypeDataList)
            {
                foreach (ItemData itemData in typeData.itemDataList)
                {
                    itemData.itemCategory =(ItemCategory) Random.Range(1, 6);
                }
            }
            
            foreach (ItemTypeData typeData in characterCustomisationItems.FemaleItemTypeDataList)
            {
                foreach (ItemData itemData in typeData.itemDataList)
                {
                    itemData.itemCategory = (ItemCategory) Random.Range(1, 6);
                }
            }

            foreach (ItemTypeData typeData in characterCustomisationItems.AllGenderItemTypeDataList)
            {
                foreach (ItemData itemData in typeData.itemDataList)
                {
                    itemData.itemCategory =(ItemCategory) Random.Range(1, 6);
                }
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        

        public void SetHideElements()
        {
            EditorUtility.SetDirty(characterCustomisationItems);
            foreach (ItemTypeData typeData in characterCustomisationItems.MaleItemTypeDataList)
            {
                if (typeData.itemType == ItemType.Eyebrows || typeData.itemType == ItemType.FacialHair ||
                    typeData.itemType == ItemType.Hair || typeData.itemType == ItemType.ElfEar|| typeData.itemType == ItemType.Head)
                {
                    foreach (ItemData itemData in typeData.itemDataList)
                    {
                        itemData.itemTypesHideList.Clear();
                        itemData.itemTypesHideList.Add(ItemType.Helmet);
                        itemData.itemTypesHideList.Add(ItemType.HeadAttachment);
                    }
                }
            }
            
            foreach (ItemTypeData typeData in characterCustomisationItems.FemaleItemTypeDataList)
            {
                if (typeData.itemType == ItemType.Eyebrows || typeData.itemType == ItemType.FacialHair ||
                    typeData.itemType == ItemType.Hair || typeData.itemType == ItemType.ElfEar|| typeData.itemType == ItemType.Head)
                {
                    foreach (ItemData itemData in typeData.itemDataList)
                    {
                        itemData.itemTypesHideList.Clear();
                        itemData.itemTypesHideList.Add(ItemType.Helmet);
                        itemData.itemTypesHideList.Add(ItemType.HeadAttachment);
                    }
                }
            }
            
               
            foreach (ItemTypeData typeData in characterCustomisationItems.AllGenderItemTypeDataList)
            {
                if (typeData.itemType == ItemType.Eyebrows || typeData.itemType == ItemType.FacialHair ||
                    typeData.itemType == ItemType.Hair || typeData.itemType == ItemType.ElfEar|| typeData.itemType == ItemType.Head)
                {
                    foreach (ItemData itemData in typeData.itemDataList)
                    {
                        itemData.itemTypesHideList.Clear();
                        itemData.itemTypesHideList.Add(ItemType.Helmet);
                        itemData.itemTypesHideList.Add(ItemType.HeadAttachment);
                    }
                }
            }

            // foreach (ItemData itemData in itemTypeData.itemDataList)
            // {
            //     if (itemData.item.name.Contains(noElement))
            //     {
            //         itemData.itemTypesHideList.Clear();
            //         itemData.itemTypesHideList.Add(ItemType.Head);
            //         itemData.itemTypesHideList.Add(ItemType.Eyebrows);
            //         itemData.itemTypesHideList.Add(ItemType.FacialHair);
            //         itemData.itemTypesHideList.Add(ItemType.Hair);
            //         itemData.itemTypesHideList.Add(ItemType.ElfEar);
            //     }
            //     else if (itemData.item.name.Contains(HeadCoverings_No_Hair))
            //     {
            //         itemData.itemTypesHideList.Clear();
            //         itemData.itemTypesHideList.Add(ItemType.Hair);
            //         itemData.itemTypesHideList.Add(ItemType.ElfEar);
            //     }
            //     else if (itemData.item.name.Contains(HeadCoverings_No_FacialHair))
            //     {
            //         itemData.itemTypesHideList.Clear();
            //         itemData.itemTypesHideList.Add(ItemType.FacialHair);
            //     }
            //     else if (itemData.item.name.Contains(HeadCoverings_Base_Hair))
            //     {
            //         itemData.itemTypesHideList.Clear();
            //         itemData.itemTypesHideList.Add(ItemType.Hair);
            //     }
            // }
            
        
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        
        void BuildList(List<ItemTypeData> targetList, string characterPart)
        {
            //EditorUtility.SetDirty(characterCustomisationItems);

            Transform[] rootTransform = characterReferences.GetComponentsInChildren<Transform>();
            Transform targetRoot = rootTransform.FirstOrDefault(t => t.gameObject.name == characterPart);

            ItemTypeData itemTypeData = new ItemTypeData();


            targetList.Add(itemTypeData);
            itemTypeData.itemDataList.Clear();

            for (int i = 0; i < targetRoot.childCount; i++)
            {
                GameObject item = targetRoot.GetChild(i).gameObject;

                ItemData currentItemData = new ItemData();

                currentItemData.item = item;

                SetItemCondition(currentItemData, characterPart);

                itemTypeData.itemDataList.Add(currentItemData);
            }

            //AssetDatabase.Refresh();
            // AssetDatabase.SaveAssets();
        }

        private void SetItemCondition(ItemData itemData, string characterPart)
        {
            // itemData.elements = characterPart.Contains(noElement) ? Elements.No : Elements.Yes;
            //
            // if (characterPart.Contains(HeadCoverings_No_FacialHair))
            // {
            //     itemData.headCovering = HeadCovering.HeadCoveringsNoFacialHair;
            // }
            //
            // if (characterPart.Contains(HeadCoverings_Base_Hair))
            // {
            //     itemData.headCovering = HeadCovering.HeadCoveringsBaseHair;
            // }
            //
            // if (characterPart.Contains(HeadCoverings_No_Hair))
            // {
            //     itemData.headCovering = HeadCovering.HeadCoveringsNoHair;
            // }
        }
    }
}
#endif