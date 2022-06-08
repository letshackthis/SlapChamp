using System.Collections.Generic;
using Customisation.UI;
using GameAnalyticsSDK;
using Managers;
using UnityEngine;

namespace Customisation.Buy
{
    [CreateAssetMenu(fileName = "UnlockAds", menuName = "UnlockItem/UnlockAds")]
    public class UnlockAds : UnlockItem
    {
        [SerializeField] private int maxAdsToSee;

        private string key;
        private int currentAds;

        public override void ShowBuyOption(UnlockItemOption unlockItemOptionValue)
        {
            base.ShowBuyOption(unlockItemOptionValue);
            UnlockItemOption.Initialize(currentAds + "/" + maxAdsToSee,unlockName, unlockButtonText,unlockImage );
        }

        protected override void TryUnlock()
        {
            
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("placement", "UNLOCK_ITEM");
            GameAnalytics.NewDesignEvent("rewarded_start", fields); 
            
            IronSourceManager.Instance.CallReward(RewardPlacement.REWARD_COINS.ToString(), () =>
            {
                RewardAds();
                
                Dictionary<string, object> fields = new Dictionary<string, object>();
                fields.Add("placement", "UNLOCK_ITEM");
                GameAnalytics.NewDesignEvent("rewarded_shown", fields); 
            });
        
        }

        public override void Initialize(string keyValue)
        {
            key = keyValue;
            currentAds = ES3.Load(SaveKeys.UnlockAds+key, 0);
        }

        private void RewardAds()
        {
            currentAds++;
            ES3.Save(SaveKeys.UnlockAds+key, currentAds);
            
            if (currentAds >= maxAdsToSee)
            {
                Unlock();
                IndexData indexData = characterChannel.temporaryIndexData;
              //  GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "ads", maxAdsToSee, "Customisation", indexData.itemType+" "+indexData.currentIndex);
            }
            else
            {
                UnlockItemOption.PriceText.text = currentAds + "/" + maxAdsToSee;
            }
        }
    }
}