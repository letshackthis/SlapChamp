using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Character_Customization_Template.Scripts
{
    [Serializable]
    public class SettingsData
    {
        [SerializeField] private Image selector;
        [SerializeField] private Button onButton;
        [SerializeField] private Button offButton;
        [SerializeField] private Image fadeOn;
        [SerializeField] private Image fadeOff;

        public Image Selector => selector;

        public Button ONButton => onButton;

        public Button OffButton => offButton;

        public Image FadeOn => fadeOn;

        public Image FadeOff => fadeOff;
    }
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private SettingsData musicData;
        [SerializeField] private SettingsData soundData;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;

        [SerializeField] private GameObject bg;
        [SerializeField] private Button backButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Text backText;
        [SerializeField] private GameObject backGround;
        
        private bool isInHouse;
        private void Start()
        {
            musicData.OffButton.onClick.AddListener(()=>MusicOnOff(false));
            musicData.ONButton.onClick.AddListener(()=>MusicOnOff(true));
            soundData.OffButton.onClick.AddListener(()=>SoundOnOff(false));
            soundData.ONButton.onClick.AddListener(()=>SoundOnOff(true));
            
            bool isSoundOn=   ES3.Load("sound", true);
            Select(soundData, isSoundOn);
            
            bool isSoundOff=   ES3.Load("music", true);
            Select(musicData, isSoundOff);
            
       

            isInHouse = SceneManager.GetActiveScene().name.Equals("CharacterHouse");
            closeButton.onClick.AddListener(Close);
        
            if (isInHouse)
            {
                backText.text = "RESUME";
                backButton.onClick.AddListener(Close);
                SoundManager.OnSoundCheck?.Invoke();
            }
            else
            {
                backText.text = "HOUSE";
                backButton.onClick.AddListener(BackToHouse);
            }
        }

        private void Select(SettingsData settingsData, bool isOn)
        {
            if (isOn)
            {
                settingsData.Selector.transform.SetParent(settingsData.ONButton.transform);
                settingsData.FadeOn.color = selectedColor;
                settingsData.FadeOff.color = unselectedColor;
            }
            else
            {
                settingsData.Selector.transform.SetParent(settingsData.OffButton.transform);
                settingsData.FadeOn.color = unselectedColor;
                settingsData.FadeOff.color = selectedColor;
            }
            settingsData.Selector.GetComponent<RectTransform>().anchoredPosition=Vector2.zero;
            settingsData.Selector.GetComponent<RectTransform>().sizeDelta=Vector2.one;
            settingsData.Selector.SetNativeSize();
        }
        
        private void SoundOnOff(bool isOn)
        {
            if (isOn)
            {
                ES3.Save("sound", true);
                Select(soundData, true);
            }
            else
            {
                ES3.Save("sound", false);
                Select(soundData, false);
            }
            SoundManager.OnSoundCheck?.Invoke();
        }

        private void MusicOnOff(bool isOn)
        {
            if (isOn)
            {
                ES3.Save("music", true);
                Select(musicData, true);
            }
            else
            {
                ES3.Save("music", false);
                Select(musicData, false);
            }
            
            SoundManager.OnSoundCheck?.Invoke();
        }

        private void BackToHouse()
        {
            Loader.OnLoadScene?.Invoke(false,"CharacterHouse");
        }

        private void Close()
        {
           bg.SetActive(false);
           backGround.SetActive(false);
        }
    }
}