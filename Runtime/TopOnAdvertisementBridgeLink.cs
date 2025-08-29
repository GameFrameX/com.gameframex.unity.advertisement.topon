// GameFrameX 组织下的以及组织衍生的项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
// 
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE 文件。
// 
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace GameFrameX.Advertisement.TopOn.Runtime
{
    /// <summary>
    /// TopOn广告管理器
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public partial class TopOnAdvertisementBridgeLink : MonoBehaviour
    {
        private AndroidJavaObject _topOnActivity;
        public string m_appIdAndroid;
        public string m_appKeyAndroid;
        public string m_appIdiOS;
        public string m_appKeyiOS;

        private void Awake()
        {
            name = "TopOnAdvertisementBridgeLink";
            DontDestroyOnLoad(gameObject);
            InitializeAndroid();
        }


        private void InitializeAndroid()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                _topOnActivity = new AndroidJavaObject("com.alianblank.advertisement.topon.MainActivity");
            }
            catch (Exception e)
            {
                Debug.LogError($"TopOn初始化失败: {e.Message}");
            }
#endif
        }
#if UNITY_IOS
        /// <summary>
        /// 初始化TopOn SDK
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appKey">应用密钥</param>
        /// <param name="isTest">是否是测试模式</param>
        [DllImport("__Internal")]
        private static extern void __gfx_top_on_ad_init(string appId, string appKey, string isTest);
#endif
        /// <summary>
        /// 初始化TopOn SDK
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appKey">应用密钥</param>
        /// <param name="isTest">是否是测试模式</param>
        public void InitializeSDK(string appId, string appKey, bool isTest = false)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                _topOnActivity?.CallStatic("init", appId, appKey, isTest.ToString());
            }
            catch (Exception e)
            {
                Debug.LogError($"TopOn SDK初始化失败: {e.Message}");
            }
#elif UNITY_IOS && !UNITY_EDITOR
            try
            {
                __gfx_top_on_ad_init(appId, appKey, isTest.ToString());
            }
            catch (Exception e)
            {
                Debug.LogError($"TopOn SDK初始化失败: {e.Message}");
            }
#else
            Debug.Log($"TopOn SDK初始化 (编辑器模式): AppId={appId}, AppKey={appKey}, debug={isTest}");
#endif
        }


#if UNITY_IOS
        /// <summary>
        /// 加载激励视频广告
        /// </summary>
        /// <param name="placementId">广告位ID</param>
        /// <param name="customData">自定义数据</param>
        [DllImport("__Internal")]
        private static extern void __gfx_top_on_ad_load_rewarded(string placementId, string customData);
#endif

        private Action<string> rewardVideoLoadSuccess;
        private Action<string> rewardVideoLoadFail;

        /// <summary>
        /// 加载激励视频广告
        /// </summary>
        /// <param name="placementId">广告位ID</param>
        /// <param name="loadFail"></param>
        /// <param name="customData">自定义数据</param>
        /// <param name="loadSuccess"></param>
        [UnityEngine.Scripting.Preserve]
        public void LoadRewardedVideoAd(string placementId, Action<string> loadSuccess, Action<string> loadFail, string customData = "")
        {
            rewardVideoLoadSuccess = loadSuccess;
            rewardVideoLoadFail = loadFail;
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                _topOnActivity?.CallStatic("loadRewardVideo", placementId, customData);
            }
            catch (Exception e)
            {
                Debug.LogError($"加载激励视频广告失败: {e.Message}");
                rewardVideoLoadFail?.Invoke(e.Message);
            }
#elif UNITY_IOS && !UNITY_EDITOR
            try
            {
                __gfx_top_on_ad_load_rewarded(placementId, customData);
            }
            catch (Exception e)
            {
                Debug.LogError($"加载激励视频广告失败: {e.Message}");
                rewardVideoLoadFail?.Invoke(e.Message);
            }
#else
            Debug.Log($"加载激励视频广告 (编辑器模式): PlacementId={placementId}, CustomData={customData}");
#endif
        }


#if UNITY_IOS
        /// <summary>
        /// 显示激励视频广告
        /// </summary>
        /// <param name="placementId">广告位ID</param>
        /// <param name="customData">自定义数据</param>
        [DllImport("__Internal")]
        private static extern void __gfx_top_on_ad_show_rewarded(string placementId, string customData);
#endif

        private Action<bool> rewardVideoShowResult;
        private Action<string> rewardVideoShowSuccess;
        private Action<string> rewardVideoShowFail;

        /// <summary>
        /// 显示激励视频广告
        /// </summary>
        /// <param name="placementId">广告位ID</param>
        /// <param name="onShowResult"></param>
        /// <param name="customData">自定义数据</param>
        /// <param name="showSuccess"></param>
        /// <param name="showFail"></param>
        [UnityEngine.Scripting.Preserve]
        public void ShowRewardedVideoAd(string placementId, Action<string> showSuccess, Action<string> showFail, Action<bool> onShowResult, string customData = "")
        {
            rewardVideoShowSuccess = showSuccess;
            rewardVideoShowFail = showFail;
            rewardVideoShowResult = onShowResult;
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                _topOnActivity?.CallStatic("showRewardVideo", placementId, customData);
            }
            catch (Exception e)
            {
                Debug.LogError($"显示激励视频广告失败: {e.Message}");
                rewardVideoShowFail?.Invoke(e.Message);
                rewardVideoShowResult?.Invoke(false);
            }
#elif UNITY_IOS && !UNITY_EDITOR
            try
            {
                __gfx_top_on_ad_show_rewarded(placementId, customData);
            }
            catch (Exception e)
            {
                Debug.LogError($"显示激励视频广告失败: {e.Message}");
                rewardVideoShowFail?.Invoke(e.Message);
                rewardVideoShowResult?.Invoke(false);
            }
#else
            Debug.Log($"显示激励视频广告 (编辑器模式): PlacementId={placementId}, CustomData={customData}");
#endif
        }


#if UNITY_IOS
        /// <summary>
        /// 检查广告是否已准备好
        /// </summary>
        /// <param name="placementId">广告位ID</param>
        [DllImport("__Internal")]
        private static extern string __gfx_top_on_ad_is_ready_rewarded(string placementId);
#endif

        /// <summary>
        /// 检查广告是否已准备好
        /// </summary>
        /// <param name="placementId">广告位ID</param>
        /// <param name="adType">广告类型</param>
        /// <returns>是否准备好</returns>
        [UnityEngine.Scripting.Preserve]
        public bool IsAdReady(string placementId, string adType)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                if (adType == "reward")
                {
                    return _topOnActivity?.CallStatic<bool>("isRewardVideoReady", placementId) ?? false;
                }
                return false;
            }
            catch (Exception e)
            {
                Debug.LogError($"检查广告状态失败: {e.Message}");
                return false;
            }
#elif UNITY_IOS && !UNITY_EDITOR
            try
            {
                if (adType == "reward")
                {
                    return Convert.ToBoolean(__gfx_top_on_ad_is_ready_rewarded(placementId));
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.LogError($"检查广告状态失败: {e.Message}");
                return false;
            }

#else
            Debug.Log($"检查广告状态 (编辑器模式): PlacementId={placementId}, AdType={adType}");
            return true;
#endif
        }

        /// <summary>
        /// 统一消息接收器 - 由Android端调用
        /// </summary>
        /// <param name="message">消息内容，格式: "messageType|data"</param>
        [UnityEngine.Scripting.Preserve]
        public void OnMessage(string message)
        {
            try
            {
                string[] parts = message.Split('|');
                if (parts.Length >= 2)
                {
                    string messageType = parts[0];
                    string data = parts[1];

                    Debug.Log($"TopOn统一回调: {messageType} - {data}");

                    // 分发到具体的管理器
                    DispatchMessage(messageType, data);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"处理TopOn回调失败: {e.Message}");
            }
        }

        /// <summary>
        /// 消息分发器
        /// </summary>
        /// <param name="messageType">消息类型</param>
        /// <param name="data">消息数据</param>
        private void DispatchMessage(string messageType, string data)
        {
            switch (messageType)
            {
                // 激励视频相关消息分发到TopOnRewardVideoManager
                case TopOnMessageType.REWARD_VIDEO_LOADED:
                    rewardVideoLoadSuccess?.Invoke(data);
                    break;
                case TopOnMessageType.REWARD_VIDEO_LOAD_FAILED:
                    rewardVideoLoadFail?.Invoke(data);
                    break;
                case TopOnMessageType.REWARD_VIDEO_SHOW:
                    rewardVideoShowSuccess?.Invoke(data);
                    break;
                case TopOnMessageType.REWARD_VIDEO_SHOW_FAILED:
                    rewardVideoShowFail?.Invoke(data);
                    rewardVideoShowResult?.Invoke(false);
                    break;
                case TopOnMessageType.REWARD_VIDEO_CLOSE:
                    // rewardVideoShowClose?.Invoke(data);
                    // TopOnRewardVideoManager.Instance.OnRewardVideoClose(data);
                    break;
                case TopOnMessageType.REWARD_VIDEO_REWARD:
                    rewardVideoShowResult?.Invoke(true);
                    break;
                case TopOnMessageType.REWARD_VIDEO_CLICK:
                    // TopOnRewardVideoManager.Instance.OnRewardVideoClick(data);
                    break;

                // 其他广告类型的消息可以在这里添加
                default:
                    Debug.LogWarning($"未处理的消息类型: {messageType}");
                    break;
            }
        }
    }
}