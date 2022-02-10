using Customisation.UI;
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
            }
        }

        public override void Initialize(string key)
        {
        
        }
    }
}
