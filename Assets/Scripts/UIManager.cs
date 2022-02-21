using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform settings, soundVibroWindow;
    [SerializeField] private GameObject soundOff, vibroOff, musicOff;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text bluePrintText;
    [SerializeField] private RectTransform bluePrintImage;
    [SerializeField] private RectTransform moneyImage;
    void Start()
    {
        if (ES3.Load("sound", true) == false)
        {
            soundOff.SetActive(false);
        }
        else
        {
            soundOff.SetActive(true);
        }
        
        if (ES3.Load("music", true) == false)
        {
            musicOff.SetActive(false);
        }
        else
        {
            musicOff.SetActive(true);
        }

        if (ES3.Load("vibro", true) == false)
        {
            vibroOff.SetActive(false);

        }
        else
        {
            vibroOff.SetActive(true);
        }
        
        bluePrintText.text = GameWallet.Blueprint.ToString();
        moneyText.text = GameWallet.Money.ToString();
        
        GameWallet.OnChangeMoney+= OnChangeMoney;
        GameWallet.OnChangeBlueprint+= OnChangeBlueprint;
        
        SoundManager.Instance.CheckSounds();
    }

    private void OnDestroy()
    {
        GameWallet.OnChangeMoney-= OnChangeMoney;
        GameWallet.OnChangeBlueprint-= OnChangeBlueprint;
    }

    private void OnChangeBlueprint()
    {
        bluePrintText.text = GameWallet.Blueprint.ToString();
        ScaleImage(bluePrintImage);
    }

    private void OnChangeMoney()
    {
        moneyText.text = GameWallet.Money.ToString();
        ScaleImage(moneyImage);
    }

    private void ScaleImage(RectTransform target)
    {
        target.DOScale(Vector3.one*1.1f, 0.5f).OnComplete(() =>
        {
            target.DOScale(Vector3.one, 0.3f);
        });
    }
    public void ToSettings()
    {
        settings.DOAnchorPos(new Vector2(0, 0), 0.35f);
        soundVibroWindow.DOShakeScale(0.6f);
    }

    public void BackToHouse()
    {
        SceneManager.LoadScene("CharacterHouse");
    }

    public void Back()
    {
        settings.DOAnchorPos(new Vector2(-1296, 0), 0.35f);
    }

    public void SoundOnOff()
    {
        if (ES3.Load("sound", true))
        {
            ES3.Save("sound", false);
            soundOff.SetActive(false);
            Debug.Log("Sound off");
        }
        else
        {
            ES3.Save("sound", true);
            soundOff.SetActive(true);
            Debug.Log("Sound on");
        }
        
        SoundManager.Instance.CheckSounds();
    }
    
    public void MusicOnOff()
    {
        if (ES3.Load("music", true))
        {
            ES3.Save("music", false);
            musicOff.SetActive(false);
            Debug.Log("Sound off");
        }
        else
        {
            ES3.Save("music", true);
            musicOff.SetActive(true);
            Debug.Log("Sound on");
         
        }

        SoundManager.Instance.CheckSounds();
    }

    public void VibrationOnOff()
    {
        if (PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            vibroOff.SetActive(true);
            PlayerPrefs.SetInt("vibro", 0);
        }
        else
        {
            vibroOff.SetActive(false);
            PlayerPrefs.SetInt("vibro", 1);
        }

    }
}
