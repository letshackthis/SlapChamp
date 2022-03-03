using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject game;
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject fail;
    [FormerlySerializedAs("coinSystem")] [SerializeField] private GameManager gameManager;
    [SerializeField] private Text levelTextUI;
    [SerializeField] private Button nextLevelButton, retryLevelButton;
    [SerializeField] private AddBluePrint addBluePrint;
    public static bool bonusLevel;

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
           // vsTextUI.fontSize = 60;
           // vsTextUI.text = "BONUS";
            levelTextUI.text = "LEVEL";
        }
        else
        {
            levelTextUI.text = "LEVEL " + PlayerPrefs.GetInt(StringKeys.level, 1).ToString();
           // vsTextUI.text = "VS";
           // vsTextUI.fontSize = 100;
        }
        
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "LEVEL_" + PlayerPrefs.GetInt(StringKeys.level, 1).ToString(), 
            "COINS_" +  GameWallet.Money);
    }

    private void FixedUpdate()
    {
        if (gameManager.playerWin)
        {
            gameManager.playerWin = false;
            Win();
        }
        else if (gameManager.playerLoose)
        {
            gameManager.playerLoose = false;
            Fail();
        }
    }

    private void NextLevel()
    {
        if (PlayerPrefs.GetInt(StringKeys.level, 1) % 3 == 0)
        {
            IronSourceManager.Instance.CallInterstitial(InterstitialPlacement.LEVEL_FINISHED.ToString());
        }
        SceneManager.LoadScene("Level1");
    }

    private void RetryLevel()
    {
        IronSourceManager.Instance.CallInterstitial(InterstitialPlacement.LEVEL_FINISHED.ToString());
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void Win()
    {
        if (Random.value >= 0.7f)
        {
            addBluePrint.Add();
        }
        
        if (fail.activeInHierarchy) return;
        game.SetActive(false);
        win.SetActive(true);
        SoundManager.Instance.PlaySound("win");
        GameWallet.Money += GetCoinsToAdd(25);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "LEVEL_" + PlayerPrefs.GetInt(StringKeys.level, 1).ToString(), 
            "COINS_" +  GameWallet.Money);
    }

    private void Fail()
    {
        if (win.activeInHierarchy) return;
        game.SetActive(false);
        fail.SetActive(true);
        SoundManager.Instance.PlaySound("fail");
        GameWallet.Money += GetCoinsToAdd(10);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "LEVEL_" + PlayerPrefs.GetInt(StringKeys.level, 1).ToString(), 
            "COINS_" +  GameWallet.Money);
    }

    private int GetCoinsToAdd(int multiplier)
    {
        return multiplier * PlayerPrefs.GetInt(StringKeys.level, 1);
    }
}