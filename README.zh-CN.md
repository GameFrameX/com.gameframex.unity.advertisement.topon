<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Unity Advertisement (TopOn)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 项目简介

Unity TopOn广告SDK集成插件，为 GameFrameX 提供完整的广告解决方案。

## 功能特性

- 支持多种广告类型（横幅、插屏、激励视频、原生广告）
- 支持Android和iOS平台
- 简单易用的API接口
- 完整的广告事件回调
- 内置错误处理和重试机制

## 支持的广告类型

- **横幅广告 (Banner)** - 页面底部或顶部的小型广告
- **插屏广告 (Interstitial)** - 全屏广告，适合在关卡间隙显示
- **激励视频 (Rewarded Video)** - 用户观看完整视频后获得奖励
- **原生广告 (Native)** - 与应用内容融合的自定义广告

## 架构概览

本包是 Game Frame X 广告核心 `BaseAdvertisementManager` 的**适配器实现**。通过 `TopOnAdvertisementBootstrap` 在 `BeforeSceneLoad` 阶段自注册进主包 `AdvertisementRegistry`（未安装本包时代码被裁剪，注册不会发生；引用即注册）；渠道初始化由 Bootstrap 工厂闭包注入强类型配置。

| 类 | 说明 |
|----|------|
| `TopOnAdvertisementManager` | 广告管理器 — 加载、展示及生命周期管理 |
| `TopOnAdvertisementConfig` | 广告配置，包含 appIdAndroid / appKeyAndroid / appIdiOS / appKeyiOS / adUnitId 字段，经 `TopOnAdvertisementBootstrap.Config` 在启动期注入 |
| `TopOnAdvertisementBootstrap` | 渠道启动注册 — 含启动期 `Config` 静态属性，`BeforeSceneLoad` 阶段自注册进 `AdvertisementRegistry` |
| `TopOnAdvertisementBridgeLink` | 原生桥接回调处理（仅保留桥接回调职责） |

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

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.advertisement.topon": "1.2.0"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.advertisement.topon": "https://github.com/gameframex/com.gameframex.unity.advertisement.topon.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.advertisement.topon.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。

### 渠道注册与配置

在启动期（首次广告调用前）对 `TopOnAdvertisementBootstrap.Config` 赋值：

```csharp
using GameFrameX.Advertisement.TopOn.Runtime;

TopOnAdvertisementBootstrap.Config = new TopOnAdvertisementConfig
{
    appIdAndroid = "你的 Android AppId",
    appKeyAndroid = "你的 Android AppKey",
    appIdiOS = "你的 iOS AppId",
    appKeyiOS = "你的 iOS AppKey",
    adUnitId = "你的广告位ID",
    isDebug = false,
};
```

- 本包通过 `TopOnAdvertisementBootstrap` 自注册进 `AdvertisementRegistry`，渠道常量 `ChannelId = "topon"`；
- 与 csj 渠道同装时，可用 `AdvertisementRegistry.ResolveManager("topon")` 显式路由做人工选型。
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

## 更新日志

### v2.0.0

- 渠道改为通过 `TopOnAdvertisementBootstrap` 在 `BeforeSceneLoad` 阶段自注册进主包 `AdvertisementRegistry`，配置经 `TopOnAdvertisementBootstrap.Config` 启动期注入
- **破坏性变更**：appId / appKey 已从场景 `TopOnAdvertisementBridgeLink` 序列化字段迁移到 `TopOnAdvertisementBootstrap.Config`，BridgeLink 仅保留桥接回调职责，既有工程需手动搬移配置

### v1.1.0

- 添加支持 AdvertisementPlayOption 参数的 Play 方法重载

### v1.0.0

- 初始版本发布
- 支持横幅、插屏、激励视频广告
- 支持Android和iOS平台


## 依赖

| 包 | 说明 |
|----|------|
| (无) | - |


## 文档与资源

- [官方文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ群: 467608841 / 233840761
## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
