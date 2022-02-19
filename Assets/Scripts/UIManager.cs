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
    [SerializeField] private GameObject soundOff, vibroOff;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text bluePrintText;
    [SerializeField] private RectTransform bluePrintImage;
    [SerializeField] private RectTransform moneyImage;
    void Start()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            soundOff.SetActive(false);
        }
        else
        {
            soundOff.SetActive(true);
        }

        if (PlayerPrefs.GetInt("vibro", 1) == 1)
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
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            PlayerPrefs.SetInt("sound", 0);
            Debug.Log("Sound off");
            if (soundOff.activeSelf)
            {
                soundOff.SetActive(false);
            }
            else
            {
                soundOff.SetActive(true);
            }
    }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            Debug.Log("Sound on");
            if (!soundOff.activeSelf)
            {
                soundOff.SetActive(true);
            }
            else
            {
                soundOff.SetActive(false);
            }
        }

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
