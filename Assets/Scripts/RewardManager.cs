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
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(AddCoins);
        GetComponentInChildren<Text>().text =
            "GET +" + (basicReward * PlayerPrefs.GetInt(StringKeys.level, 1)).ToString() + "COINS";

        transform.DOScale(animTarget, animDuration).SetLoops(-1, loopType);
    }

    private void AddCoins()
    {
        IronSourceManager.Instance.CallReward(RewardPlacement.REWARD_COINS.ToString(), () =>
        {
            PlayerPrefs.SetInt(StringKeys.totalCoins,
                PlayerPrefs.GetInt(StringKeys.totalCoins) + 50 * PlayerPrefs.GetInt(StringKeys.level, 1));
        });
    }
}