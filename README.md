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

## Architecture

This package is an **adapter implementation** of `BaseAdvertisementManager` from the Game Frame X Advertisement core. It registers itself into the core package's `AdvertisementRegistry` via `TopOnAdvertisementBootstrap` during the `BeforeSceneLoad` phase (if this package is not installed the code is stripped and no registration happens; referencing the package is the registration). Channel initialization is performed by the Bootstrap factory closure, which injects the strongly typed configuration.

| Class | Description |
|-------|-------------|
| `TopOnAdvertisementManager` | Ad manager — load, show, and lifecycle |
| `TopOnAdvertisementConfig` | Configuration with appIdAndroid / appKeyAndroid / appIdiOS / appKeyiOS / adUnitId fields, injected at startup via `TopOnAdvertisementBootstrap.Config` |
| `TopOnAdvertisementBootstrap` | Channel bootstrap registration — exposes a startup-time `Config` static property and self-registers into `AdvertisementRegistry` at `BeforeSceneLoad` |
| `TopOnAdvertisementBridgeLink` | Native bridge callback handling (bridge callbacks only) |

## Quick Start

### Installation

Choose one of the following methods:

1. Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:
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

   `scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

2. Add to `manifest.json` dependencies:
   ```json
   {
      "com.gameframex.unity.advertisement.topon": "https://github.com/gameframex/com.gameframex.unity.advertisement.topon.git"
   }
   ```
3. Use **Package Manager** in Unity with **Git URL**: `https://github.com/gameframex/com.gameframex.unity.advertisement.topon.git`
4. Clone the repository into your Unity project's `Packages` directory. It will be loaded automatically.

### Channel Registration & Configuration

Assign `TopOnAdvertisementBootstrap.Config` at startup (before the first ad call):

```csharp
using GameFrameX.Advertisement.TopOn.Runtime;

TopOnAdvertisementBootstrap.Config = new TopOnAdvertisementConfig
{
    appIdAndroid = "your Android AppId",
    appKeyAndroid = "your Android AppKey",
    appIdiOS = "your iOS AppId",
    appKeyiOS = "your iOS AppKey",
    adUnitId = "your ad unit id",
    isDebug = false,
};
```

- This package self-registers into `AdvertisementRegistry` via `TopOnAdvertisementBootstrap`; the channel constant is `ChannelId = "topon"`;
- When installed alongside the csj channel, use `AdvertisementRegistry.ResolveManager("topon")` for explicit routing / manual selection.
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

### v2.0.0

- The channel now self-registers into the core package's `AdvertisementRegistry` via `TopOnAdvertisementBootstrap` at `BeforeSceneLoad`; configuration is injected at startup via `TopOnAdvertisementBootstrap.Config`
- **Breaking change**: appId / appKey have been migrated from the scene `TopOnAdvertisementBridgeLink` serialized fields to `TopOnAdvertisementBootstrap.Config`; the BridgeLink now only keeps its bridge callback responsibilities, and existing projects must migrate the configuration manually

### v1.1.0

- Added Play method overload with AdvertisementPlayOption parameter

### v1.0.0

- Initial release
- Support for banner, interstitial, and rewarded video ads
- Support for Android and iOS platforms


## Dependencies

| Package | Description |
|---------|-------------|
| (无) | - |


## Documentation & Resources

- [Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: 467608841 / 233840761
## License

See [LICENSE.md](LICENSE.md) for license information.
