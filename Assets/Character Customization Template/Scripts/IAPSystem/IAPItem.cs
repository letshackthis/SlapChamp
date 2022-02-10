using UnityEngine;
using UnityEngine.Purchasing;

namespace IAPSystem
{
    public abstract class IAPItem : ScriptableObject
    {
        [SerializeField] private ProductType productType;
        [SerializeField] private ProductIdentifier productIdentifier;
        [SerializeField] private string key;
        [SerializeField] private string priceText;
        [SerializeField] private string rewardText;

        public ProductType Type => productType;

        public ProductIdentifier Identifier => productIdentifier;

        public string Key => key;

        public string PriceText => priceText;

        public string RewardText => rewardText;

        public abstract void GetReward();
    }
}
