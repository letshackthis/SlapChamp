using System;
using UnityEngine;

namespace Customisation
{
    [Serializable]
    public abstract class BuyOption
    {
        [SerializeField] private int value;
        public abstract void ShowBuyOption();
        public abstract void Buy();
    }

    [Serializable]
    public class IAPBuyOption:BuyOption
    {
        public override void ShowBuyOption()
        {
        }

        public override void Buy()
        {
        
        }
    }

    [Serializable]
    public class MoneyBuyOption:BuyOption
    {
        [SerializeField] private int moneyAmount;
        
        public override void ShowBuyOption()
        {
        }

        public override void Buy()
        {
        
        }
    }

    [Serializable]
    public class BlueprintBuyOption:BuyOption
    {
        [SerializeField] private int blueprintAmount;
        
        public override void ShowBuyOption()
        {
        }

        public override void Buy()
        {
        
        }
    }
}