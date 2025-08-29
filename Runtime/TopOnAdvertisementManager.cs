using System;
using GameFrameX.Advertisement.Runtime;
using UnityEngine;

namespace GameFrameX.Advertisement.TopOn.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public class TopOnAdvertisementManager : BaseAdvertisementManager
    {
        [UnityEngine.Scripting.Preserve] private TopOnAdvertisementBridgeLink instance;
        [UnityEngine.Scripting.Preserve] private string m_adUnitId;

        [UnityEngine.Scripting.Preserve]
        public override void Initialize(string adUnitId, bool isTest = false)
        {
            m_adUnitId = adUnitId;
            instance = UnityEngine.Object.FindObjectOfType<TopOnAdvertisementBridgeLink>();
#if UNITY_IOS && !UNITY_EDITOR
            instance.InitializeSDK(instance.m_appIdiOS, instance.m_appKeyiOS, isTest);
#elif UNITY_ANDROID && !UNITY_EDITOR
            instance.InitializeSDK(instance.m_appIdAndroid, instance.m_appKeyAndroid, isTest);
#else
            Debug.LogWarning("TopOn Advertisement not support in Editor");
#endif
        }

        [UnityEngine.Scripting.Preserve]
        public override void Show(Action<string> success, Action<string> fail, Action<bool> onShowResult, string customData = null)
        {
            instance.ShowRewardedVideoAd(m_adUnitId, success, fail, onShowResult, customData);
        }

        [UnityEngine.Scripting.Preserve]
        public override void Load(Action<string> success, Action<string> fail, string customData = null)
        {
            instance.LoadRewardedVideoAd(m_adUnitId, success, fail, customData);
        }
    }
}