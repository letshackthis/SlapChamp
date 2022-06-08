using System;
using Customisation.SO;
using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Customisation.UI
{
    [Serializable]
    public class ButtonHeaderData
    {
        [SerializeField] private Text textButton;
        [SerializeField] private Image imageButton;
        [SerializeField] private Button currentButton;
      

        public Image ImageButton => imageButton;

        public Text TextButton => textButton;

        public Button CurrentButton => currentButton;
    }
    public class HeaderManager : MonoBehaviour
    {
        [SerializeField] private CharacterChannel characterChannel;
        [SerializeField] private ButtonHeaderData[] buttonList;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Text blueprintText;
        [SerializeField] private Text moneyText;
        [SerializeField] private Text nickname;

        private CharacterLoadSave characterLoadSave;
        private void Awake()
        {
            ChangeActiveButton(0);
            buttonList[0].CurrentButton.onClick.AddListener(()=>
            {
                ChangeActiveButton(0);
                CameraViewChanger.OnCameraViewChange?.Invoke(ItemType.None);
                characterChannel.OnItemHolderChange?.Invoke(ItemHolderType.EquipHolder);
            });
            buttonList[1].CurrentButton.onClick.AddListener(()=>
            {
                ChangeActiveButton(1);
                CameraViewChanger.OnCameraViewChange?.Invoke(ItemType.None);
                characterChannel.OnItemHolderChange?.Invoke(ItemHolderType.EditHolder);
            });

            GameWallet.OnChangeBlueprint+= OnChangeBlueprint;
            GameWallet.OnChangeMoney += OnChangeMoney;
            characterChannel.OnSaveCharacter += OnSaveCharacter;
            characterChannel.OnLoadCharacterData += OnLoadCharacterData;
            moneyText.text = GameWallet.Money.ToString();
            blueprintText.text = GameWallet.Blueprint.ToString();
            
            SoundManager.OnSoundCheck?.Invoke();
            
            GameAnalytics.NewDesignEvent("main_menu");
        }

        private void OnLoadCharacterData(CharacterLoadSave obj)
        {
            characterLoadSave = obj;
            nickname.text = characterLoadSave.CharacterSaveData.nickName;
        }

        private void OnSaveCharacter()
        {
            nickname.text = characterLoadSave.CharacterSaveData.nickName;
        }

        private void OnChangeBlueprint()
        {
            blueprintText.text = GameWallet.Blueprint.ToString();
        }

        private void OnChangeMoney()
        {
            moneyText.text = GameWallet.Money.ToString();
        }

        private void OnDestroy()
        {
            GameWallet.OnChangeBlueprint-= OnChangeBlueprint;
            GameWallet.OnChangeMoney -= OnChangeMoney;
            characterChannel.OnSaveCharacter -= OnSaveCharacter;
            characterChannel.OnLoadCharacterData -= OnLoadCharacterData;
        }

        private void ChangeActiveButton(int index)
        {
            for (var i = 0; i < buttonList.Length; i++)
            {
                buttonList[i].ImageButton.sprite = inactiveSprite;
                buttonList[i].TextButton.color = activeColor;
            }
            
            buttonList[index].ImageButton.sprite = activeSprite;
            buttonList[index].TextButton.color = inactiveColor;
        }

    }
}
