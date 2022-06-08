using System.Collections.Generic;
using Customisation.UI;
using GameAnalyticsSDK;
using UnityEngine;

namespace Customisation.Buy
{
    [CreateAssetMenu(fileName = "UnlockMoney", menuName = "UnlockItem/UnlockMoney")]
    public class UnlockMoney : UnlockItem
    {
        [SerializeField] private int moneyAmount;
    
        public override void ShowBuyOption(UnlockItemOption unlockItemOption)
        {
            base.ShowBuyOption(unlockItemOption);
            UnlockItemOption.Initialize(moneyAmount.ToString(),unlockName, unlockButtonText,unlockImage );
        }

        protected override void TryUnlock()
        {
            if (GameWallet.Money >= moneyAmount)
            {
                GameWallet.Money -= moneyAmount;
                Unlock();
                IndexData indexData = characterChannel.temporaryIndexData;
                //GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "coin", moneyAmount, "Customisation", indexData.itemType+" "+indexData.currentIndex);
                
                int currentSoftSpentCount = ES3.Load(global::SaveKeys.SoftSpentCount, 0);
                currentSoftSpentCount++;
                ES3.Save(global::SaveKeys.SoftSpentCount, currentSoftSpentCount);
                
                Dictionary<string, object> fields = new Dictionary<string, object>();
                
                fields.Add("type", "Customisation");
                fields.Add("currency", "coin");
                fields.Add("name", indexData.itemType+" "+indexData.currentIndex);
                fields.Add("amount", moneyAmount);
                fields.Add("count", currentSoftSpentCount);
        
                GameAnalytics.NewDesignEvent("soft_spent", fields); 
            }
        }

        public override void Initialize(string key)
        {
        
        }
    }
}
