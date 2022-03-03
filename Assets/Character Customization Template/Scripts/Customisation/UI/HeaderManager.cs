using System;
using Customisation.SO;
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
