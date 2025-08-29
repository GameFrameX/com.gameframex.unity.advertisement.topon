# GameFrameX Unity Advertisement TopOn

Unity TopOn广告SDK集成插件，提供完整的广告解决方案。

## 功能特性

- 🎯 支持多种广告类型（横幅、插屏、激励视频、原生广告）
- 📱 支持Android和iOS平台
- 🔧 简单易用的API接口
- 📊 完整的广告事件回调
- 🛡️ 内置错误处理和重试机制

## 支持的广告类型

- **横幅广告 (Banner)** - 页面底部或顶部的小型广告
- **插屏广告 (Interstitial)** - 全屏广告，适合在关卡间隙显示
- **激励视频 (Rewarded Video)** - 用户观看完整视频后获得奖励
- **原生广告 (Native)** - 与应用内容融合的自定义广告

## 安装方式

### 通过Unity Package Manager安装

1. 打开Unity编辑器
2. 选择 `Window` > `Package Manager`
3. 点击左上角的 `+` 按钮
4. 选择 `Add package from git URL`
5. 输入包的Git URL或本地路径

### 手动安装

1. 下载或克隆此仓库
2. 将整个文件夹复制到你的Unity项目的 `Packages` 目录下
3. Unity会自动识别并导入包

## 快速开始

### 1. 初始化SDK

```csharp
using GameFrameX.Advertisement.TopOn.Runtime;

// 在游戏启动时初始化
TopOnAdvertisement.Initialize("your_app_id", "your_app_key");
```

### 2. 加载和显示横幅广告

```csharp
// 加载横幅广告
TopOnAdvertisement.LoadBanner("banner_placement_id");

// 显示横幅广告
TopOnAdvertisement.ShowBanner();

// 隐藏横幅广告
TopOnAdvertisement.HideBanner();
```

### 3. 加载和显示插屏广告

```csharp
// 加载插屏广告
TopOnAdvertisement.LoadInterstitial("interstitial_placement_id");

// 显示插屏广告
if (TopOnAdvertisement.IsInterstitialReady())
{
    TopOnAdvertisement.ShowInterstitial();
}
```

### 4. 加载和显示激励视频

```csharp
// 加载激励视频
TopOnAdvertisement.LoadRewardedVideo("rewarded_placement_id");

// 显示激励视频
if (TopOnAdvertisement.IsRewardedVideoReady())
{
    TopOnAdvertisement.ShowRewardedVideo();
}
```

## 事件回调

```csharp
// 注册广告事件监听
TopOnAdvertisement.OnBannerLoaded += OnBannerLoaded;
TopOnAdvertisement.OnBannerFailed += OnBannerFailed;
TopOnAdvertisement.OnInterstitialLoaded += OnInterstitialLoaded;
TopOnAdvertisement.OnInterstitialFailed += OnInterstitialFailed;
TopOnAdvertisement.OnRewardedVideoLoaded += OnRewardedVideoLoaded;
TopOnAdvertisement.OnRewardedVideoRewarded += OnRewardedVideoRewarded;

private void OnBannerLoaded()
{
    Debug.Log("横幅广告加载成功");
}

private void OnBannerFailed(string error)
{
    Debug.LogError($"横幅广告加载失败: {error}");
}

private void OnRewardedVideoRewarded(string placementId, string rewardName, int rewardAmount)
{
    Debug.Log($"获得奖励: {rewardName} x {rewardAmount}");
    // 在这里给玩家发放奖励
}
```

## 平台配置

### Android配置

插件已包含必要的Android依赖配置，位于 `Plugins/Android/AndroidDependencies.xml`。

确保你的项目设置：
- Target API Level: 28或更高
- 启用 `Custom Main Gradle Template`
- 启用 `Custom Gradle Properties Template`

### iOS配置

iOS平台的配置文件位于 `Plugins/iOS/` 目录下。

确保在Xcode项目中：
- 添加必要的系统框架
- 配置App Transport Security设置
- 添加广告标识符权限

## 测试模式

在开发阶段，建议启用测试模式：

```csharp
// 启用测试模式（仅在开发时使用）
TopOnAdvertisement.SetTestMode(true);
```

**注意：发布前务必关闭测试模式！**

## 常见问题

### Q: 广告无法显示？
A: 请检查：
- 网络连接是否正常
- 广告位ID是否正确
- 是否在正确的时机调用显示方法
- 是否已正确初始化SDK

### Q: iOS构建失败？
A: 请确保：
- Xcode版本兼容
- 已添加必要的系统框架
- 检查代码签名设置

### Q: Android构建失败？
A: 请检查：
- Gradle版本兼容性
- 依赖冲突问题
- ProGuard配置

## API文档

详细的API文档请参考：[API Documentation](./Documentation~/API.md)

## 更新日志

### v1.0.0
- 初始版本发布
- 支持横幅、插屏、激励视频广告
- 支持Android和iOS平台

## 技术支持

如果遇到问题或需要技术支持，请：

1. 查看[常见问题](#常见问题)部分
2. 提交Issue到项目仓库
3. 联系技术支持团队

## 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE) 文件

## 贡献指南

欢迎提交Pull Request来改进这个项目！

1. Fork本仓库
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 开启Pull Request

---

**GameFrameX Unity Advertisement TopOn** - 让广告集成变得简单高效！