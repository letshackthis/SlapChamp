using System.Collections.Generic;
using GameAnalyticsSDK;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAdsButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int moneyAmount;

    private void Awake()
    {
        button.onClick.AddListener(GetMoney);
    }

    private void GetMoney()
    {
        Dictionary<string, object> fields = new Dictionary<string, object>();
        fields.Add("placement", "REWARD_COINS");
        GameAnalytics.NewDesignEvent("rewarded_start", fields); 
        
        IronSourceManager.Instance.CallReward(RewardPlacement.REWARD_COINS.ToString(), () =>
        {
            GameWallet.Money += moneyAmount;
            
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("placement", "REWARD_COINS");
            GameAnalytics.NewDesignEvent("rewarded_shown", fields); 
        });

    }
}
