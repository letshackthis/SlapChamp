using Customisation.UI;
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
            //call from IronSourceManager
            RewardAds();
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
            }
            else
            {
                UnlockItemOption.PriceText.text = currentAds + "/" + maxAdsToSee;
            }
        }
    }
}