<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Unity Advertisement (TopOn)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>
## Project Overview

Unity TopOn Ad SDK integration plugin, providing a complete advertising solution for GameFrameX.

## Features

- Support for multiple ad types (Banner, Interstitial, Rewarded Video, Native)
- Android and iOS platform support
- Simple and easy-to-use API
- Complete ad event callbacks
- Built-in error handling and retry mechanism

## Supported Ad Types

- **Banner** - Small ads at the top or bottom of the page
- **Interstitial** - Full-screen ads, suitable for displaying between levels
- **Rewarded Video** - Users receive rewards after watching the complete video
- **Native** - Custom ads that blend with app content

## Installation

### Install via Unity Package Manager

1. Open Unity Editor
2. Select `Window` > `Package Manager`
3. Click the `+` button in the top left
4. Select `Add package from git URL`
5. Enter the package Git URL or local path

### Manual Installation

1. Download or clone this repository
2. Copy the entire folder to the `Packages` directory of your Unity project
3. Unity will automatically recognize and import the package

## Quick Start

### 1. Initialize SDK

```csharp
using GameFrameX.Advertisement.TopOn.Runtime;

// Initialize when the game starts
TopOnAdvertisement.Initialize("your_app_id", "your_app_key");
```

### 2. Load and Display Banner Ads

```csharp
// Load banner ad
TopOnAdvertisement.LoadBanner("banner_placement_id");

// Show banner ad
TopOnAdvertisement.ShowBanner();

// Hide banner ad
TopOnAdvertisement.HideBanner();
```

### 3. Load and Display Interstitial Ads

```csharp
// Load interstitial ad
TopOnAdvertisement.LoadInterstitial("interstitial_placement_id");

// Show interstitial ad
if (TopOnAdvertisement.IsInterstitialReady())
{
    TopOnAdvertisement.ShowInterstitial();
}
```

### 4. Load and Display Rewarded Video

```csharp
// Load rewarded video
TopOnAdvertisement.LoadRewardedVideo("rewarded_placement_id");

// Show rewarded video
if (TopOnAdvertisement.IsRewardedVideoReady())
{
    TopOnAdvertisement.ShowRewardedVideo();
}
```

## Event Callbacks

```csharp
// Register ad event listeners
TopOnAdvertisement.OnBannerLoaded += OnBannerLoaded;
TopOnAdvertisement.OnBannerFailed += OnBannerFailed;
TopOnAdvertisement.OnInterstitialLoaded += OnInterstitialLoaded;
TopOnAdvertisement.OnInterstitialFailed += OnInterstitialFailed;
TopOnAdvertisement.OnRewardedVideoLoaded += OnRewardedVideoLoaded;
TopOnAdvertisement.OnRewardedVideoRewarded += OnRewardedVideoRewarded;

private void OnBannerLoaded()
{
    Debug.Log("Banner ad loaded successfully");
}

private void OnBannerFailed(string error)
{
    Debug.LogError($"Banner ad failed to load: {error}");
}

private void OnRewardedVideoRewarded(string placementId, string rewardName, int rewardAmount)
{
    Debug.Log($"Reward earned: {rewardName} x {rewardAmount}");
    // Grant reward to the player here
}
```

## Platform Configuration

### Android Configuration

The plugin includes necessary Android dependency configuration in `Plugins/Android/AndroidDependencies.xml`.

Ensure your project settings:

- Target API Level: 28 or higher
- Enable `Custom Main Gradle Template`
- Enable `Custom Gradle Properties Template`

### iOS Configuration

iOS platform configuration files are located in the `Plugins/iOS/` directory.

Ensure in your Xcode project:

- Add necessary system frameworks
- Configure App Transport Security settings
- Add ad identifier permission

## Test Mode

During development, it is recommended to enable test mode:

```csharp
// Enable test mode (only use during development)
TopOnAdvertisement.SetTestMode(true);
```

**Note: Make sure to disable test mode before release!**

## FAQ

### Q: Ads not displaying?

A: Please check:

- Network connection is normal
- Ad placement ID is correct
- Display method is called at the right time
- SDK is properly initialized

### Q: iOS build failed?

A: Please ensure:

- Xcode version is compatible
- Necessary system frameworks are added
- Check code signing settings

### Q: Android build failed?

A: Please check:

- Gradle version compatibility
- Dependency conflicts
- ProGuard configuration

## Changelog

### v1.1.0

- Added Play method overload with AdvertisementPlayOption parameter

### v1.0.0

- Initial release
- Support for banner, interstitial, and rewarded video ads
- Support for Android and iOS platforms

## License

This project is licensed under the MIT License - see [LICENSE](LICENSE) for details.
