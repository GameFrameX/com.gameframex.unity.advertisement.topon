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

编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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
  ]
}
```

`scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement.topon": "1.2.0"
  }
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

## 更新日志

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
