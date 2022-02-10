using Customisation.UI;
using IAPSystem;
using UnityEngine;

namespace Customisation.Buy
{
    [CreateAssetMenu(fileName = "UnlockIAP", menuName = "UnlockItem/UnlockIAP")]
    public class UnlockIAP : UnlockItem
    {
        [SerializeField] private IAPCustomisation iapCustomisation;

        public override void ShowBuyOption(UnlockItemOption unlockItemOption)
        {
            base.ShowBuyOption(unlockItemOption);
            UnlockItemOption.Initialize(iapCustomisation.PriceText, unlockName, unlockButtonText, unlockImage);
        }

        protected override void TryUnlock()
        {
            IAPManager.OnBuyIapItem?.Invoke(iapCustomisation.Identifier);
        }

        protected override void Unlock()
        {
            base.Unlock();
            
        }

        public override void Initialize(string key)
        {
            iapCustomisation.OnUnlockCustomisationItem = Unlock;
        }
    }
}