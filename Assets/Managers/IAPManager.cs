// using System;
// using System.Collections.Generic;
// using Facebook.MiniJSON;
// using GameAnalyticsSDK;
// using UnityEngine;
// using UnityEngine.Purchasing;
//
// namespace Managers
// {
//     public class IAPManager : MonoBehaviour, IStoreListener
//     {
//         public static Action<string> OnBuyIapItem;
//         
//         private static IStoreController m_StoreController;         
//         private static IExtensionProvider m_StoreExtensionProvider;
//         
//         private static string moneyPack1 ="com.spacechallenge.moneypack1";   
//         private static string moneyPack2 ="com.spacechallenge.moneypack2";   
//         private static string moneyPack3 ="com.spacechallenge.moneypack3";   
//         
//         private static string shipPack1 ="com.spacechallenge.spaceship1";   
//         private static string shipPack2 ="com.spacechallenge.spaceship2";   
//         private static string shipPack3 ="com.spacechallenge.spaceship3";
//         
//         private static string removeAds ="com.spacechallenge.removeads";
//
//         private void Awake()
//         {
//             OnBuyIapItem += BuyProductID;
//         }
//
//         private void OnDestroy()
//         {
//             OnBuyIapItem -= BuyProductID;
//         }
//
//         void Start()
//         {
//             if (m_StoreController == null)
//             {
//                 InitializePurchasing();
//             }
//         }
//
//         public void InitializePurchasing() 
//         {
//             if (IsInitialized())
//             {
//                 return;
//             }
//
//             var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//             
//             builder.AddProduct(moneyPack1, ProductType.Consumable);
//             builder.AddProduct(moneyPack2, ProductType.Consumable);
//             builder.AddProduct(moneyPack3, ProductType.Consumable);
//             
//             builder.AddProduct(shipPack1, ProductType.Consumable);
//             builder.AddProduct(shipPack2, ProductType.Consumable);
//             builder.AddProduct(shipPack3, ProductType.Consumable);
//             
//             builder.AddProduct(removeAds, ProductType.Consumable);
//             
//             UnityPurchasing.Initialize(this, builder);
//         }
//
//
//         private bool IsInitialized()
//         {
//             return m_StoreController != null && m_StoreExtensionProvider != null;
//         }
//
//
//        private void BuyProductID(string productId)
//         {
//             if (IsInitialized())
//             {
//                 Product product = m_StoreController.products.WithID(productId);
//
//                 if (product != null && product.availableToPurchase)
//                 {
//                     Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//                     m_StoreController.InitiatePurchase(product);
//                 }
//                 else
//                 {
//                     Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//                 }
//             }
//             else
//             {
//                 Debug.Log("BuyProductID FAIL. Not initialized.");
//             }
//         }
//
//         public void RestorePurchases()
//         {
//             if (!IsInitialized())
//             {
//                 Debug.Log("RestorePurchases FAIL. Not initialized.");
//                 return;
//             }
//
//             if (Application.platform == RuntimePlatform.IPhonePlayer || 
//                 Application.platform == RuntimePlatform.OSXPlayer)
//             {
//                 Debug.Log("RestorePurchases started ...");
//
//                 var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//                 apple.RestoreTransactions((result) => {
//                     Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//                 });
//             }
//             else
//             {
//                 Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//             }
//         }
//         
//         public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//         {
//             Debug.Log("OnInitialized: PASS");
//             m_StoreController = controller;
//             m_StoreExtensionProvider = extensions;
//         }
//
//
//         public void OnInitializeFailed(InitializationFailureReason error)
//         {
//             Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
//         }
//
//
//         public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//         {
//             if (String.Equals(args.purchasedProduct.definition.id, moneyPack1, StringComparison.Ordinal))
//             {
//                 GameAnalytics.NewBusinessEvent("USD ",  50, "money pack", args.purchasedProduct.definition.id.ToString(), "Cash");
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                 
//                 DataSave.Instance.NumberOfStars += 50;
//                 PlayerPrefs.SetInt("Coins", DataSave.Instance.NumberOfStars);
//             }
//             else  if (String.Equals(args.purchasedProduct.definition.id, moneyPack2, StringComparison.Ordinal))
//             {
//                 GameAnalytics.NewBusinessEvent("USD ",  150, "money pack", args.purchasedProduct.definition.id.ToString(), "Cash");
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                 
//                 DataSave.Instance.NumberOfStars += 150;
//                 PlayerPrefs.SetInt("Coins", DataSave.Instance.NumberOfStars);
//             }
//             else  if (String.Equals(args.purchasedProduct.definition.id, moneyPack3, StringComparison.Ordinal))
//             {
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                 
//                 GameAnalytics.NewBusinessEvent("USD ",  300, "money pack", args.purchasedProduct.definition.id.ToString(), "Cash");
//                 DataSave.Instance.NumberOfStars += 300;
//                 PlayerPrefs.SetInt("Coins", DataSave.Instance.NumberOfStars);
//             }
//             
//             
//             else  if (String.Equals(args.purchasedProduct.definition.id, shipPack1, StringComparison.Ordinal))
//             {
//                 GameAnalytics.NewBusinessEvent("USD ",  1, "ship pack", args.purchasedProduct.definition.id.ToString(), "Cash");
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                 PlayerPrefs.SetInt("10" + "State", 1);
//                 Shop.OnBuyIapShip?.Invoke("10");
//             }
//             else  if (String.Equals(args.purchasedProduct.definition.id, shipPack2, StringComparison.Ordinal))
//             {
//                 GameAnalytics.NewBusinessEvent("USD ",  1, "ship pack", args.purchasedProduct.definition.id.ToString(), "Cash");
//
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                 PlayerPrefs.SetInt("11" + "State", 1);
//                 Shop.OnBuyIapShip?.Invoke("11");
//             }
//             else  if (String.Equals(args.purchasedProduct.definition.id, shipPack3, StringComparison.Ordinal))
//             {
//                 GameAnalytics.NewBusinessEvent("USD ",  1, "ship pack", args.purchasedProduct.definition.id.ToString(), "Cash");
//
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                
//                 PlayerPrefs.SetInt("12" + "State", 1);
//                 Shop.OnBuyIapShip?.Invoke("12");
//             }
//             else  if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
//             {
//                 GameAnalytics.NewBusinessEvent("USD ",  1, "remove ads", args.purchasedProduct.definition.id.ToString(), "Cash");
//             
//                 Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//                
//                 PlayerPrefs.SetInt("RemovedAds", 1);
//             }
//             else 
//             {
//                 Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//             }
//             return PurchaseProcessingResult.Complete;
//         }
//         
//         public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//         {
//             Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//         }
//         
//         
//         
//         public static void OnProcessPurchase(PurchaseEventArgs purchaseEventArgs) {
//             var price = purchaseEventArgs.purchasedProduct.metadata.localizedPrice;
//             double lPrice = decimal.ToDouble(price);
//             var currencyCode = purchaseEventArgs.purchasedProduct.metadata.isoCurrencyCode;
//
//             var wrapper = Json.Deserialize(purchaseEventArgs.purchasedProduct.receipt) as Dictionary<string, object>;  // https://gist.github.com/darktable/1411710
//             if (null == wrapper) {
//                 return;
//             }
//
//             var payload   = (string)wrapper["Payload"]; // For Apple this will be the base64 encoded ASN.1 receipt
//             var productId = purchaseEventArgs.purchasedProduct.definition.id;
//
// #if UNITY_ANDROID
//
//             var gpDetails = Json.Deserialize(payload) as Dictionary<string, object>;
//             var gpJson    = (string)gpDetails["json"];
//             var gpSig     = (string)gpDetails["signature"];
//
//             CompletedAndroidPurchase(productId, currencyCode, 1, lPrice, gpJson, gpSig);
//
// #elif UNITY_IOS
//
//   var transactionId = purchaseEventArgs.purchasedProduct.transactionID;
//
//   CompletedIosPurchase(productId, currencyCode, 1, lPrice , transactionId, payload);
//
// #endif
//
//         }
//
//         private static void CompletedAndroidPurchase(string ProductId, string CurrencyCode, int Quantity, double UnitPrice, string Receipt, string Signature)
//         {
//             BaseTenjin instance = Tenjin.getInstance("API_KEY");
//             instance.Transaction(ProductId, CurrencyCode, Quantity, UnitPrice, null, Receipt, Signature);
//         }
//
//         private static void CompletedIosPurchase(string ProductId, string CurrencyCode, int Quantity, double UnitPrice, string TransactionId, string Receipt)
//         {
//             BaseTenjin instance = Tenjin.getInstance("API_KEY");
//             instance.Transaction(ProductId, CurrencyCode, Quantity, UnitPrice, TransactionId, Receipt, null);
//         }
//     }
// }