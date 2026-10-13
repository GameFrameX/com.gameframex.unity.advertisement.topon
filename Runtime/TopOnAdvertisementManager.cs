using System;
using GameFrameX.Advertisement.Runtime;
using GameFrameX.Runtime;
using UnityEngine;

namespace GameFrameX.Advertisement.TopOn.Runtime
{
    /// <summary>
    /// TopOn（AnyThink）聚合广告管理器。
    /// </summary>
    /// <remarks>
    /// 渠道身份常量 <see cref="ChannelId"/> 供注册表显式路由与人工选型使用；初始化统一走
    /// <see cref="Initialize(AdvertisementConfig)"/> 的强类型 <see cref="TopOnAdvertisementConfig"/>
    /// （由 TopOnAdvertisementBootstrap 工厂闭包注入，appId / appKey 已从场景 BridgeLink 迁移到配置）。
    /// </remarks>
    [UnityEngine.Scripting.Preserve]
    public class TopOnAdvertisementManager : BaseAdvertisementManager
    {
        /// <summary>
        /// TopOn 渠道唯一标识。
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public const string ChannelId = "topon";

        [UnityEngine.Scripting.Preserve] private TopOnAdvertisementBridgeLink instance;
        [UnityEngine.Scripting.Preserve] private string m_adUnitId;
        [UnityEngine.Scripting.Preserve] private string m_appId;
        [UnityEngine.Scripting.Preserve] private string m_appKey;

        /// <summary>
        /// 使用强类型配置初始化 TopOn 广告管理器（appId / appKey 取自配置，场景 BridgeLink 不再承载应用配置）。
        /// </summary>
        /// <param name="config">TopOn 渠道配置</param>
        [UnityEngine.Scripting.Preserve]
        public override void Initialize(AdvertisementConfig config)
        {
            var topOnConfig = config as TopOnAdvertisementConfig;
            if (topOnConfig == null)
            {
                throw new ArgumentException("Config must be TopOnAdvertisementConfig", nameof(config));
            }

            GameFrameworkGuard.NotNullOrEmpty(topOnConfig.adUnitId, nameof(topOnConfig.adUnitId));
            m_adUnitId = topOnConfig.adUnitId;
            instance = UnityEngine.Object.FindObjectOfType<TopOnAdvertisementBridgeLink>();
#if UNITY_IOS && !UNITY_EDITOR
            m_appId = topOnConfig.appIdiOS;
            m_appKey = topOnConfig.appKeyiOS;
            instance.InitializeSDK(m_appId, m_appKey, topOnConfig.isDebug);
#elif UNITY_ANDROID && !UNITY_EDITOR
            m_appId = topOnConfig.appIdAndroid;
            m_appKey = topOnConfig.appKeyAndroid;
            instance.InitializeSDK(m_appId, m_appKey, topOnConfig.isDebug);
#else
            m_appId = topOnConfig.appIdAndroid;
            m_appKey = topOnConfig.appKeyAndroid;
            Debug.LogWarning("TopOn Advertisement not support in Editor");
#endif
        }

        /// <summary>
        /// 使用广告位ID初始化 TopOn 广告管理器（旧版接口：appId / appKey 取自 <see cref="TopOnAdvertisementBootstrap"/> 的启动期配置）。
        /// </summary>
        /// <param name="adUnitId">广告位ID</param>
        /// <param name="isTest">是否是测试模式</param>
        [Obsolete("Use Initialize(AdvertisementConfig) instead.")]
        [UnityEngine.Scripting.Preserve]
        public override void Initialize(string adUnitId, bool isTest = false)
        {
#pragma warning disable CS0618
            var bootstrapConfig = TopOnAdvertisementBootstrap.Config;
            Initialize(new TopOnAdvertisementConfig
            {
                appIdAndroid = bootstrapConfig != null ? bootstrapConfig.appIdAndroid : null,
                appKeyAndroid = bootstrapConfig != null ? bootstrapConfig.appKeyAndroid : null,
                appIdiOS = bootstrapConfig != null ? bootstrapConfig.appIdiOS : null,
                appKeyiOS = bootstrapConfig != null ? bootstrapConfig.appKeyiOS : null,
                adUnitId = adUnitId,
                isDebug = isTest,
            });
#pragma warning restore CS0618
        }

        [UnityEngine.Scripting.Preserve]
        public override void Play(Action<bool> playResult, string customData = null)
        {
            instance.PlayRewardedVideoAd(m_adUnitId, playResult, customData);
        }

        public override void Play(AdvertisementPlayOption option)
        {
            instance.PlayRewardedVideoAd(m_adUnitId, option.OnShowResult, option.customData);
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