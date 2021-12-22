using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform hitIndicator;

    public static bool bonusLevel, wasReseted;

    protected override void Awake()
    {
        hitIndicator.SetParent(null);
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
    }

    void FixedUpdate()
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
        while (true)
        {
            int randomSceneIndex = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
            if (randomSceneIndex != SceneManager.GetActiveScene().buildIndex)
            {
                SceneManager.LoadScene(randomSceneIndex);
            }
            else
            {
                continue;
            }

            break;
        }
    }

    private void RetryLevel()
    {
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
    }

    private void Fail()
    {
        game.SetActive(false);
        fail.SetActive(true);
        SoundManager.Instance.PlaySound("fail");
        PlayerPrefs.SetInt(StringKeys.totalCoins, GetCoinsToAdd(10));
    }

    private int GetCoinsToAdd(int multiplier)
    {
        return coinSystem.totalCoins + multiplier * PlayerPrefs.GetInt(StringKeys.level, 1);
    }
}