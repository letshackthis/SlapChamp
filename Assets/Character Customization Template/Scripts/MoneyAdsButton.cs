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
        IronSourceManager.Instance.CallReward(RewardPlacement.REWARD_COINS.ToString(), () =>
        {
            GameWallet.Money += moneyAmount;
        });

    }
}
