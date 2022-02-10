using System;
using UnityEngine;

namespace IAPSystem
{
    [CreateAssetMenu(fileName = "Customisation", menuName = "IAP/ Customisation ")]
    public class IAPCustomisation : IAPItem
    {
        public Action OnUnlockCustomisationItem;
        
        public override void GetReward()
        {
            OnUnlockCustomisationItem?.Invoke();
        }
    }
}
