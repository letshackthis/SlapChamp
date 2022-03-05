using System;
using GameAnalyticsSDK;
using UnityEngine;

namespace IAPSystem
{
    [CreateAssetMenu(fileName = "Customisation", menuName = "IAP/ Customisation ")]
    public class IAPCustomisation : IAPItem
    {
        public Action OnUnlockCustomisationItem;
        
        public override void GetReward()
        {
            OnUnlockCustomisationItem?.Invoke();

            GameAnalytics.NewBusinessEvent("USD", price, productIdentifier.ToString(), "Customisation", "Customisation");
        }
    }
}
