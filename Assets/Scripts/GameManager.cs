using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> lowSlapList;
    [SerializeField] private List<ParticleSystem> mediumSlapList;
    [SerializeField] private List<ParticleSystem> highSlapList;
    [SerializeField] private Button buyHealthBtn, buyPowerBtn;
    [SerializeField] private MMProgressBar playerHealthBar;
    [SerializeField] private MMProgressBar enemyHealthBar;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerHand;
    [SerializeField] private Transform enemyHand;
    
    public MMFeedbacks slapFeedbackEnemy;
    public MMFeedbacks slapFeedbackPlayer;
    [SerializeField] private Text totalCoinsText,
        healthPriceText,
        powerPriceText,
        bonusHealthText,
        bonusPowerText,
        playerHealthText,
        enemyHealthText;

    [SerializeField] private TextMeshProUGUI playerPowerText;

    [SerializeField] private Image unableHealth, unablePower;

    public int totalCoins,
        healthPrice,
        powerPrice,
        bonusHealthValue,
        bonusPowerValue,
        playerPower,
        playerHealth,
        enemyHealth,
        enemyPower,
        currentLevel,
        dmgPwr;
    
    [SerializeField] private HitPower hitPower;

    public bool strongHit;
    public bool playerWin, playerLoose;
    private int maxHealhPlayer;
    private int maxHealhEnemy;

    private void Awake()
    {
        Initialization();
        InitializeHealth();
        CheckHealthButton();
        CheckPowerButton();

        buyHealthBtn.onClick.AddListener(BuyHealth);
        buyPowerBtn.onClick.AddListener(BuyPower);

        playerWin = false;
        playerLoose = false;

        dmgPwr = 0;

        currentLevel = PlayerPrefs.GetInt(StringKeys.level, 1);
        enemyHealth = Random.Range(5 * currentLevel + 95, 9 * currentLevel + 101);
        enemyHealthText.text = enemyHealth.ToString();
        playerHealthText.text = playerHealth.ToString();
    }

    private void InitializeHealth()
    {
        playerHealth= PlayerPrefs.GetInt("PlayerHealth", 100);
        enemyHealth= PlayerPrefs.GetInt("EnemyHealth", 100);
        maxHealhEnemy= enemyHealth;
        maxHealhPlayer= playerHealth;
        
    }

    private void EnemyAttackPower()
    {
        int lowestHit = 6 * currentLevel + 17;
        int strongestHit = 7 * currentLevel + 21;
        enemyPower = Random.Range(lowestHit, strongestHit);
        dmgPwr = enemyPower;    
        if (dmgPwr >= (strongestHit - (strongestHit - lowestHit)))
        {
            strongHit = true;
        }
    }

    private bool CheckHealthButton()
    {
        if (int.Parse(totalCoinsText.text) < int.Parse(healthPriceText.text))
        {
            unableHealth.GetComponent<Image>().color = Color.gray;
            unableHealth.GetComponent<Button>().enabled = false;
            return false;
        }

        return true;
    }

    private bool CheckPowerButton()
    {
        if (int.Parse(totalCoinsText.text) < int.Parse(powerPriceText.text))
        {
            unablePower.GetComponent<Image>().color = Color.gray;
            unablePower.GetComponent<Button>().enabled = false;
            return false;
        }

        return true;
    }

    public void PlayerGetDamage()
    {
        EnemyAttackPower();
        slapFeedbackPlayer?.PlayFeedbacks(playerTransform.position, dmgPwr);
        ActivateSlapParticles(dmgPwr, maxHealhPlayer, enemyHand);
        if (dmgPwr >= playerHealth)
        {
            playerHealth -= dmgPwr;
            playerHealthText.text = "0";
            playerHealthBar.UpdateBar(maxHealhPlayer,0,maxHealhPlayer);
            playerHealth = 0;
            playerLoose = true;
        }
        else
        {
            playerHealth -= dmgPwr;
            playerHealthBar.UpdateBar(playerHealth,0,maxHealhPlayer);
            playerHealthText.text = playerHealth.ToString();
        }
    }

    public IEnumerator EnemyGetDamage()
    {
        var playerHit = (int) Math.Round(playerPower * hitPower.CheckHitPowerSection(), 0);
        slapFeedbackEnemy?.PlayFeedbacks(enemyTransform.position, playerHit);
        ActivateSlapParticles(playerHit, maxHealhEnemy, playerHand);
        if (playerHit >= enemyHealth)
        {
            enemyHealthText.text = "0";
            enemyHealthBar.UpdateBar(maxHealhEnemy,0,maxHealhEnemy);
            enemyHealth = 0;
            playerWin = true;
            var nextLevel = 1 + PlayerPrefs.GetInt(StringKeys.level, 1);
            PlayerPrefs.SetInt(StringKeys.level, nextLevel);
        }
        else
        {
            playerPowerText.text = playerHit.ToString();
            enemyHealth -= playerHit;
            enemyHealthBar.UpdateBar(enemyHealth,0,maxHealhEnemy);
            enemyHealthText.text = enemyHealth.ToString();
            yield return new WaitForSeconds(5);
            playerPowerText.text = playerPower.ToString();
        }
    }

    private void Initialization()
    {
        totalCoins = PlayerPrefs.GetInt(StringKeys.totalCoins, 51);
        totalCoinsText.text = totalCoins.ToString();

        healthPrice = PlayerPrefs.GetInt(StringKeys.healthPrice, 25);
        healthPriceText.text = healthPrice.ToString();

        powerPrice = PlayerPrefs.GetInt(StringKeys.powerPrice, 25);
        powerPriceText.text = powerPrice.ToString();

        playerHealth = PlayerPrefs.GetInt(StringKeys.playerMaxHealth, 100);
        
        playerPower = PlayerPrefs.GetInt(StringKeys.playerMaxPower, 35);
        playerPowerText.text = playerPower.ToString();

        bonusHealthValue = PlayerPrefs.GetInt(StringKeys.bonusHealth, 5);
        bonusHealthText.text = "HEALTH(" + bonusHealthValue.ToString() + ")";

        bonusPowerValue = PlayerPrefs.GetInt(StringKeys.bonusPower, 10);
        bonusPowerText.text = "POWER(" + bonusPowerValue.ToString() + ")";
        playerHealthText.text = playerHealth.ToString();
    }

    private void BuyHealth()
    {
        if (CheckHealthButton())
        {
            totalCoins -= healthPrice;
            PlayerPrefs.SetInt(StringKeys.totalCoins, totalCoins);
            playerHealth += bonusHealthValue;
            PlayerPrefs.SetInt(StringKeys.playerMaxHealth, playerHealth);
            bonusHealthValue += 1;
            PlayerPrefs.SetInt(StringKeys.bonusHealth, bonusHealthValue);
            healthPrice += 50;
            PlayerPrefs.SetInt(StringKeys.healthPrice, healthPrice);
            Initialization();
            CheckHealthButton();
        }
    }

    private void BuyPower()
    {
        if (CheckPowerButton())
        {
            totalCoins -= powerPrice;
            PlayerPrefs.SetInt(StringKeys.totalCoins, totalCoins);
            playerPower += bonusPowerValue;
            PlayerPrefs.SetInt(StringKeys.playerMaxPower, playerPower);
            bonusHealthValue += 1;
            PlayerPrefs.SetInt(StringKeys.bonusPower, bonusPowerValue);
            powerPrice += 50;
            PlayerPrefs.SetInt(StringKeys.powerPrice, powerPrice);
            Initialization();
            CheckPowerButton();
        }
    }

    private void ActivateSlapParticles(int currentHit, int maxHit,Transform targetPosition)
    {
        ParticleSystem currentParticles;
        float percentage = (currentHit * 100) / maxHit;
        if (percentage <= 0.3f)
        {
            currentParticles = lowSlapList[Random.Range(0, lowSlapList.Count)];
        }
        else if (percentage > 0.3f && percentage <= 0.9f)
        {
            currentParticles = mediumSlapList[Random.Range(0, mediumSlapList.Count)];
        }
        else
        {
            currentParticles = highSlapList[Random.Range(0, highSlapList.Count)];
        }

        currentParticles.transform.position = targetPosition.position;
        currentParticles.Play();
    }

    private int GetDamagePercent(int max,int input)
    {
        return  (input * 100) / max;
    }
}