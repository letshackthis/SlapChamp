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

    private float timeSpent;
    private bool isOnline;
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        timeSpent = 0;
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
            Debug.Log(ES3.Load(StringKeys.level, 1));
            levelTextUI.text = "LEVEL " + ES3.Load(StringKeys.level, 1).ToString();
            // vsTextUI.text = "VS";
            // vsTextUI.fontSize = 100;
        }

        isOnline = ES3.Load(SaveKeys.IsOnline, false);

        // GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Map0", "LEVEL_" + ES3.Load(StringKeys.level, 1),
        //     isOnline ? "ONLINE" : "OFFLINE", GameWallet.Money);

        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("level", ES3.Load(StringKeys.level, 1));
        
        GameAnalytics.NewDesignEvent ("level_start", fields);
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

        if (!gameManager.playerLoose)
        {
            timeSpent += Time.fixedTime;
        }
    }

    private void NextLevel()
    {
        if (ES3.Load(StringKeys.level, 1) % 3 == 0)
        {
            IronSourceManager.Instance.CallInterstitial(InterstitialPlacement.LEVEL_FINISHED.ToString());
        }
        Loader.OnLoadScene?.Invoke(ES3.Load(SaveKeys.IsOnline,false),"Level1");
    }

    private void RetryLevel()
    {
        IronSourceManager.Instance.CallInterstitial(InterstitialPlacement.LEVEL_FINISHED.ToString());
        var sceneName = SceneManager.GetActiveScene().name;
        
        
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("level", ES3.Load(StringKeys.level, 1));
        
        GameAnalytics.NewDesignEvent("restart", fields); 
        
        Loader.OnLoadScene?.Invoke(ES3.Load(SaveKeys.IsOnline,false),"Level1");
        
        
    }

    private void Win()
    {
        if (Random.value >= 0f)
        {
            addBluePrint.Add();
        }
        
        if (fail.activeInHierarchy) return;
        //game.SetActive(false);
        win.SetActive(true);
        SoundManager.Instance.PlaySound("win");
        GameWallet.Money += GetCoinsToAdd(25);
      
    }

    private void Fail()
    {
        if (win.activeInHierarchy) return;
        //game.SetActive(false);
        fail.SetActive(true);
        SoundManager.Instance.PlaySound("fail");
        GameWallet.Money += GetCoinsToAdd(10);

        int currentLevel = ES3.Load(StringKeys.level, 1);
        
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("level", currentLevel);
        fields.Add("time_spent", (int) timeSpent);

        GameAnalytics.NewDesignEvent("fail", fields);
    }

    private int GetCoinsToAdd(int multiplier)
    {
        return multiplier *ES3.Load(StringKeys.level, 1);
    }
}