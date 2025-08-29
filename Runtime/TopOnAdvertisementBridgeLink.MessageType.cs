namespace GameFrameX.Advertisement.TopOn.Runtime
{
    public partial class TopOnAdvertisementBridgeLink
    {
        /// <summary>
        /// TopOn消息类型
        /// </summary>
        public static class TopOnMessageType
        {
            // 激励视频相关消息
            public const string REWARD_VIDEO_LOADED = "RewardVideoLoaded";
            public const string REWARD_VIDEO_LOAD_FAILED = "RewardVideoLoadFailed";
            public const string REWARD_VIDEO_SHOW = "RewardVideoShow";
            public const string REWARD_VIDEO_SHOW_FAILED = "RewardVideoShowFailed";
            public const string REWARD_VIDEO_CLOSE = "RewardVideoClose";
            public const string REWARD_VIDEO_REWARD = "RewardVideoReward";
            public const string REWARD_VIDEO_CLICK = "RewardVideoClick";

            // 横幅广告相关消息
            public const string BANNER_LOADED = "BannerLoaded";
            public const string BANNER_LOAD_FAILED = "BannerLoadFailed";
            public const string BANNER_SHOW = "BannerShow";
            public const string BANNER_SHOW_FAILED = "BannerShowFailed";
            public const string BANNER_CLICK = "BannerClick";
            public const string BANNER_CLOSE = "BannerClose";

            // 插屏广告相关消息
            public const string INTERSTITIAL_LOADED = "InterstitialLoaded";
            public const string INTERSTITIAL_LOAD_FAILED = "InterstitialLoadFailed";
            public const string INTERSTITIAL_SHOW = "InterstitialShow";
            public const string INTERSTITIAL_SHOW_FAILED = "InterstitialShowFailed";
            public const string INTERSTITIAL_CLICK = "InterstitialClick";
            public const string INTERSTITIAL_CLOSE = "InterstitialClose";

            // SDK相关消息
            public const string SDK_INIT_SUCCESS = "SDKInitSuccess";
            public const string SDK_INIT_FAILED = "SDKInitFailed";
        }
    }
}