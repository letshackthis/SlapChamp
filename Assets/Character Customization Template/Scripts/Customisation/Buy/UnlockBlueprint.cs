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
                GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "blueprint", blueprintAmount, "Customisation", indexData.itemType+" "+indexData.currentIndex);
            }
       
        }

        public override void Initialize(string key)
        {
        
        }
    }
}
