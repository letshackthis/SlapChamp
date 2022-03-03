using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Customisation;
using Customisation.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Character_Customization_Template.Scripts.Customisation
{
    [Serializable]
    public class GenderData
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private List<IndexData> indexData;
        [SerializeField] private Image image;
        [SerializeField] private Button genderButton;
        [SerializeField] private bool isMale;
        
        public RectTransform Target => target;

        public Image Image1 => image;

        public Button GenderButton => genderButton;

        public bool IsMale => isMale;

        public List<IndexData> Data => indexData;
    }
    public class CharacterSelection : MonoBehaviour
    {
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private List<GenderData> genderDataList;
        [SerializeField] private RectTransform selector;
        [SerializeField] private Button confirmButton;
        [SerializeField] private InputField inputField;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;
        
        private GenderData currentGenderData=null;
        private CharacterLoadSave characterLoadSave;
        private void Awake()
        {
            confirmButton.onClick.AddListener(Confirm);
            characterChannel.OnLoadCharacterData+= OnLoadCharacterData;
            ButtonInitialization();
           
        }
        
        private void OnDestroy()
        {
            characterChannel.OnLoadCharacterData-= OnLoadCharacterData;
        }

        private void OnLoadCharacterData(CharacterLoadSave obj)
        {
            
            if(characterLoadSave==null)
                characterLoadSave = obj;
            
            if (currentGenderData == null)
            {
                ChooseGender(genderDataList[0]);
            }
        }

      
        private void ButtonInitialization()
        {
            foreach (GenderData genderData in genderDataList)
            {
                genderData.GenderButton.onClick.AddListener(()=>
                {
                    ChooseGender(genderData);
                });
            }
        }
        private void Confirm()
        {
            string nickname = inputField.text;
            characterLoadSave.CharacterSaveData.nickName = nickname;
            characterChannel.OnSaveCharacter?.Invoke(); 
        }

        private void ChooseGender(GenderData genderData)
        {
            
            
            if (currentGenderData == null || currentGenderData != genderData)
            {
               
                SetDefaultConfigGenderData();
                
                currentGenderData = genderData;
                
                genderData.Image1.color = selectedColor;
                selector.SetParent(genderData.Target);
                selector.anchoredPosition=Vector2.zero;
                selector.localScale=Vector3.one;
                selector.GetComponent<Image>().SetNativeSize();

                for (var i = 0; i < genderData.Data.Count; i++)
                {
                    IndexData currentData = characterLoadSave.SelectedItemList.Find(e => e.itemType == genderData.Data[i].itemType);
                    currentData.currentIndex = genderData.Data[i].currentIndex;
                }
                
                characterChannel.OnChangeGender?.Invoke(genderData.IsMale);
       
            }
        }

        private void SetDefaultConfigGenderData()
        {
            foreach (GenderData data in genderDataList)
            {
                data.Image1.color = unselectedColor;
            }
        }
    }
}
