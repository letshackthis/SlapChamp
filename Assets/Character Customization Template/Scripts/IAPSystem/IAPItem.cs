using UnityEngine;
using UnityEngine.Purchasing;

namespace IAPSystem
{
    public abstract class IAPItem : ScriptableObject
    {
        [SerializeField] protected ProductType productType;
        [SerializeField] protected ProductIdentifier productIdentifier;
        [SerializeField] protected string key;
        [SerializeField] protected string priceText;
        [SerializeField] protected string rewardText;
        [SerializeField] protected int price;
        public ProductType Type => productType;

        public ProductIdentifier Identifier => productIdentifier;

        public string Key => key;

        public string PriceText => priceText;

        public string RewardText => rewardText;

        public abstract void GetReward();
    }
}
