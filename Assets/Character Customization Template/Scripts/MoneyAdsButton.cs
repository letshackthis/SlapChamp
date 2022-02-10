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
        GameWallet.Money += moneyAmount;
    }
}
