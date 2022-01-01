using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject game;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject fail;
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private Text levelTextUI, vsTextUI;
    [SerializeField] private Button nextLevelButton, retryLevelButton;

    public static bool bonusLevel, wasReseted;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        nextLevelButton.onClick.AddListener(NextLevel);
        retryLevelButton.onClick.AddListener(RetryLevel);

        if (bonusLevel)
        {
            vsTextUI.fontSize = 60;
            vsTextUI.text = "BONUS";
            levelTextUI.text = "LEVEL";
        }
        else
        {
            levelTextUI.text = "LEVEL " + PlayerPrefs.GetInt(StringKeys.level, 1).ToString();
            vsTextUI.text = "VS";
            vsTextUI.fontSize = 100;
        }
        
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "LEVEL_" + PlayerPrefs.GetInt(StringKeys.level, 1).ToString(), 
            "COINS_" +  PlayerPrefs.GetInt(StringKeys.totalCoins).ToString());
    }

    private void FixedUpdate()
    {
        if (coinSystem.playerWin)
        {
            coinSystem.playerWin = false;
            Win();
        }
        else if (coinSystem.playerLoose)
        {
            coinSystem.playerLoose = false;
            Fail();
        }
    }

    private void NextLevel()
    {
        if (PlayerPrefs.GetInt(StringKeys.level, 1) % 3 == 0)
        {
            IronSourceManager.Instance.CallInterstitial(InterstitialPlacement.LEVEL_FINISHED.ToString());
        }
        SceneManager.LoadScene(1);
    }

    private void RetryLevel()
    {
        IronSourceManager.Instance.CallInterstitial(InterstitialPlacement.LEVEL_FINISHED.ToString());
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Vibration()
    {
        if(PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            Handheld.Vibrate();
        }
    }

    private void Win()
    {
        game.SetActive(false);
        win.SetActive(true);
        SoundManager.Instance.PlaySound("win");
        PlayerPrefs.SetInt(StringKeys.totalCoins, GetCoinsToAdd(25));
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "LEVEL_" + PlayerPrefs.GetInt(StringKeys.level, 1).ToString(), 
            "COINS_" +  PlayerPrefs.GetInt(StringKeys.totalCoins).ToString());
    }

    private void Fail()
    {
        game.SetActive(false);
        fail.SetActive(true);
        SoundManager.Instance.PlaySound("fail");
        PlayerPrefs.SetInt(StringKeys.totalCoins, GetCoinsToAdd(10));
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "LEVEL_" + PlayerPrefs.GetInt(StringKeys.level, 1).ToString(), 
            "COINS_" +  PlayerPrefs.GetInt(StringKeys.totalCoins).ToString());
    }

    private int GetCoinsToAdd(int multiplier)
    {
        return coinSystem.totalCoins + multiplier * PlayerPrefs.GetInt(StringKeys.level, 1);
    }
}