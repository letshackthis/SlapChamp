using System.Collections.Generic;
using Customisation.UI;
using GameAnalyticsSDK;
using UnityEngine;

namespace Customisation.Buy
{
    [CreateAssetMenu(fileName = "UnlockBlueprint", menuName = "UnlockItem/UnlockBlueprint")]
    public class UnlockBlueprint : UnlockItem
    {
        [SerializeField] private int blueprintAmount;
        
        public override void ShowBuyOption(UnlockItemOption unlockItemOption)
        {
            base.ShowBuyOption(unlockItemOption);
            UnlockItemOption.Initialize(blueprintAmount.ToString(),unlockName, unlockButtonText,unlockImage );
        }
        protected override void TryUnlock()
        {
            if (GameWallet.Blueprint >= blueprintAmount)
            {
                GameWallet.Blueprint -= blueprintAmount;
                Unlock();
                IndexData indexData = characterChannel.temporaryIndexData;
               // GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "blueprint", blueprintAmount, "Customisation", indexData.itemType+" "+indexData.currentIndex);
                
                
                
                int currentSoftSpentCount = ES3.Load(global::SaveKeys.SoftSpentCount, 0);
                currentSoftSpentCount++;
                ES3.Save(global::SaveKeys.SoftSpentCount, currentSoftSpentCount);
                
                Dictionary<string, object> fields = new Dictionary<string, object>();
                
                fields.Add("type", "Customisation");
                fields.Add("currency", "blueprint");
                fields.Add("name", indexData.itemType+" "+indexData.currentIndex);
                fields.Add("amount", blueprintAmount);
                fields.Add("count", currentSoftSpentCount);
        
                GameAnalytics.NewDesignEvent("soft_spent", fields); 
            }
       
        }

        public override void Initialize(string key)
        {
        
        }
    }
}
