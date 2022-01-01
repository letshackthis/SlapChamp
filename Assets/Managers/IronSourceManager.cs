using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public enum RewardPlacement
    {
        REWARD_COINS
    }

    public enum InterstitialPlacement
    {
        LEVEL_FINISHED
    }

    public class IronSourceManager : MonoBehaviour
    {
        Action tempReward;

        private static IronSourceManager _instance;

        public static IronSourceManager Instance { get { return _instance; } }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void CallReward(string placement, Action rewardFunction)
        {
            tempReward = rewardFunction;

            IronSource.Agent.showRewardedVideo(placement);

#if UNITY_EDITOR
            tempReward();
#endif
        }

        public void CallInterstitial(string placement)
        {
            if (PlayerPrefs.GetInt("RemovedAds", 0) == 1)
            {
                return;
            }

            if (!IronSource.Agent.isInterstitialReady())
                IronSource.Agent.loadInterstitial();
            else
                IronSource.Agent.showInterstitial(placement);
        }

        public void Start()
        {
            IronSource.Agent.loadInterstitial();
        }

        void OnEnable()
        {
            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;

            IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
            IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
            IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += RewardedVideoAdLoadedDemandOnlyEvent;
            IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
            IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
            IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += RewardedVideoAdLoadFailedDemandOnlyEvent;

            IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
            IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
            IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
            IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
            IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
            IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;

            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            IronSourceEvents.onInterstitialAdReadyDemandOnlyEvent += InterstitialAdReadyDemandOnlyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedDemandOnlyEvent += InterstitialAdLoadFailedDemandOnlyEvent;
            IronSourceEvents.onInterstitialAdShowFailedDemandOnlyEvent += InterstitialAdShowFailedDemandOnlyEvent;
            IronSourceEvents.onInterstitialAdClickedDemandOnlyEvent += InterstitialAdClickedDemandOnlyEvent;
            IronSourceEvents.onInterstitialAdOpenedDemandOnlyEvent += InterstitialAdOpenedDemandOnlyEvent;
            IronSourceEvents.onInterstitialAdClosedDemandOnlyEvent += InterstitialAdClosedDemandOnlyEvent;

            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

            IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
        }


        void OnApplicationPause(bool isPaused)
        {
            Debug.Log("unity-script: OnApplicationPause = " + isPaused);
            IronSource.Agent.onApplicationPause(isPaused);
        }

        #region RewardedAd callback handlers

        void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
        {
            Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
        }

        void RewardedVideoAdOpenedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
        }

        void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
        {
            Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() +
                      " name = " + ssp.getRewardName());
            Debug.Log("Reward for placement " + ssp.getPlacementName());

            tempReward?.Invoke();
        }

        void RewardedVideoAdClosedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
        }

        void RewardedVideoAdStartedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
        }

        void RewardedVideoAdEndedEvent()
        {
            Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
        }

        void RewardedVideoAdShowFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() +
                      ", description : " + error.getDescription());
        }

        void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
        {
            Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
        }

        /************* RewardedVideo DemandOnly Delegates *************/

        void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
        }

        void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
        {
            Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId +
                      ", code :  " + error.getCode() + ", description : " + error.getDescription());
        }

        void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
        }

        void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
        }

        void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
        }

        void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
        {
            Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId +
                      ", code :  " + error.getCode() + ", description : " + error.getDescription());
        }

        void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
        }

        #endregion


        #region Interstitial callback handlers

        void InterstitialAdReadyEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdReadyEvent");
        }

        void InterstitialAdLoadFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() +
                      ", description : " + error.getDescription());
        }

        void InterstitialAdShowSucceededEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
        }

        void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() +
                      ", description : " + error.getDescription());
        }

        void InterstitialAdClickedEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdClickedEvent");
        }

        void InterstitialAdOpenedEvent()
        {
            Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
        }

        void InterstitialAdClosedEvent()
        {
            IronSource.Agent.loadInterstitial();

            Debug.Log("unity-script: I got InterstitialAdClosedEvent");
        }

        /************* Interstitial DemandOnly Delegates *************/

        void InterstitialAdReadyDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got InterstitialAdReadyDemandOnlyEvent for instance: " + instanceId);
        }

        void InterstitialAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
        {
            Debug.Log("unity-script: I got InterstitialAdLoadFailedDemandOnlyEvent for instance: " + instanceId +
                      ", error code: " + error.getCode() + ",error description : " + error.getDescription());
        }

        void InterstitialAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
        {
            Debug.Log("unity-script: I got InterstitialAdShowFailedDemandOnlyEvent for instance: " + instanceId +
                      ", error code :  " + error.getCode() + ",error description : " + error.getDescription());
        }

        void InterstitialAdClickedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got InterstitialAdClickedDemandOnlyEvent for instance: " + instanceId);
        }

        void InterstitialAdOpenedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got InterstitialAdOpenedDemandOnlyEvent for instance: " + instanceId);
        }

        void InterstitialAdClosedDemandOnlyEvent(string instanceId)
        {
            Debug.Log("unity-script: I got InterstitialAdClosedDemandOnlyEvent for instance: " + instanceId);
        }

        #endregion

        #region Banner callback handlers

        void BannerAdLoadedEvent()
        {
            Debug.Log("unity-script: I got BannerAdLoadedEvent");
        }

        void BannerAdLoadFailedEvent(IronSourceError error)
        {
            Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " +
                      error.getDescription());
        }

        void BannerAdClickedEvent()
        {
            Debug.Log("unity-script: I got BannerAdClickedEvent");
        }

        void BannerAdScreenPresentedEvent()
        {
            Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
        }

        void BannerAdScreenDismissedEvent()
        {
            Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
        }

        void BannerAdLeftApplicationEvent()
        {
            Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
        }

        #endregion


        #region Offerwall callback handlers

        void OfferwallOpenedEvent()
        {
            Debug.Log("I got OfferwallOpenedEvent");
        }

        void OfferwallClosedEvent()
        {
            Debug.Log("I got OfferwallClosedEvent");
        }

        void OfferwallShowFailedEvent(IronSourceError error)
        {
            Debug.Log("I got OfferwallShowFailedEvent, code :  " + error.getCode() + ", description : " +
                      error.getDescription());
        }

        void OfferwallAdCreditedEvent(Dictionary<string, object> dict)
        {
            Debug.Log("I got OfferwallAdCreditedEvent, current credits = " + dict["credits"] + " totalCredits = " +
                      dict["totalCredits"]);
        }

        void GetOfferwallCreditsFailedEvent(IronSourceError error)
        {
            Debug.Log("I got GetOfferwallCreditsFailedEvent, code :  " + error.getCode() + ", description : " +
                      error.getDescription());
        }

        void OfferwallAvailableEvent(bool canShowOfferwal)
        {
            Debug.Log("I got OfferwallAvailableEvent, value = " + canShowOfferwal);
        }

        #endregion

        #region ImpressionSuccess callback handler

        void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
        {
            Debug.Log("unity - script: I got ImpressionSuccessEvent ToString(): " + impressionData.ToString());
            Debug.Log("unity - script: I got ImpressionSuccessEvent allData: " + impressionData.allData);
        }

        #endregion
    }
}