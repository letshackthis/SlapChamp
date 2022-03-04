using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    [Header("Reward settings")]
    [SerializeField] private int basicReward = 50;

    [Header("Animation settings")] 
    [SerializeField] private float animDuration = 0.6f;

    [SerializeField] private LoopType loopType = LoopType.Yoyo;
    
    [SerializeField] private Vector3 animTarget = new Vector3(1.1f, 1.1f, 1.1f);

    private int level;
    private void Start()
    {
        level=ES3.Load(StringKeys.level, 1);
        
        GetComponent<Button>().onClick.AddListener(AddCoins);
        GetComponentInChildren<Text>().text =
            "GET +" + (basicReward * level) + "COINS";

        transform.DOScale(animTarget, animDuration).SetLoops(-1, loopType);
    }

    private void AddCoins()
    {
        Debug.Log("AddCoins");
        IronSourceManager.Instance.CallReward(RewardPlacement.REWARD_COINS.ToString(), () =>
        {
            Debug.Log("Added");
            gameObject.SetActive(false);
            GameWallet.Money += (50 * level);
        });
    }
}