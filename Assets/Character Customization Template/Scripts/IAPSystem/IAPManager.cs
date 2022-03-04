using System;
using UnityEngine;
using UnityEngine.Purchasing;
using Utilities;

namespace IAPSystem
{
    public class IAPManager : Singleton<IAPManager>, IStoreListener
    {
        public static Action<ProductIdentifier> OnBuyIapItem;
        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;
       [SerializeField] private IAPItem[] iapItems;

        private ProductIdentifier currentProductIdentifier;
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            OnBuyIapItem += BuyIapItem;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnBuyIapItem -= BuyIapItem;
        }

        void Start()
        {
            if (m_StoreController == null)
            {
                InitializePurchasing();
            }
        }

        private void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            foreach (IAPItem iapItem in iapItems)
            {
                builder.AddProduct(iapItem.Key, iapItem.Type);
            }
            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }
        
        private void BuyIapItem(ProductIdentifier productIdentifier)
        {
#if UNITY_EDITOR
            Debug.Log("Come: "+productIdentifier);
            foreach (var item in iapItems)
            {
                Debug.Log("item: "+item.Identifier);
            }
            IAPItem iapItem = Array.Find(iapItems, e => e.Identifier == productIdentifier);
            currentProductIdentifier = productIdentifier;
            iapItem.GetReward();
#else

#if UNITY_ANDROID
             IAPItem iapItem = Array.Find(iapItems, e => e.Identifier == productIdentifier);
            currentProductIdentifier = productIdentifier;
            iapItem.GetReward();
  #else
            Debug.Log("Item: "+productIdentifier);
            IAPItem iapItem = Array.Find(iapItems, e => e.Identifier == productIdentifier);
            currentProductIdentifier = productIdentifier;
            BuyProductID(iapItem.Key);
#endif
#endif
         
        }

        private void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log(
                        "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");

                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) =>
                {
                    Debug.Log("RestorePurchases continuing: " + result +
                              ". If no further messages, no purchases available to restore.");
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            IAPItem iapItem = Array.Find(iapItems, e => e.Identifier== currentProductIdentifier );
            if (iapItem)
            {
                iapItem.GetReward();
            }
            else
            {
                Debug.Log("ProcessPurchase: IAPItem was not found ");
            }
            

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",
                product.definition.storeSpecificId, failureReason));
        }
    }
}