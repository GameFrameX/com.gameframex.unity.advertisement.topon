<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Unity Advertisement (TopOn)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 項目簡介

Unity TopOn 廣告 SDK 整合插件，為 GameFrameX 提供完整的廣告解決方案。

## 功能特性

- 支持多種廣告類型（橫幅、插屏、激勵影片、原生廣告）
- 支持 Android 和 iOS 平台
- 簡單易用的 API 介面
- 完整的廣告事件回調
- 內建錯誤處理和重試機制

## 支持的廣告類型

- **橫幅廣告 (Banner)** - 頁面底部或頂部的小型廣告
- **插屏廣告 (Interstitial)** - 全屏廣告，適合在關卡間隙顯示
- **激勵影片 (Rewarded Video)** - 使用者觀看完整影片後獲得獎勵
- **原生廣告 (Native)** - 與應用內容融合的自訂廣告

## 安裝方式

### 透過 Unity Package Manager 安裝

1. 開啟 Unity 編輯器
2. 選擇 `Window` > `Package Manager`
3. 點擊左上角的 `+` 按鈕
4. 選擇 `Add package from git URL`
5. 輸入套件的 Git URL 或本地路徑

### 手動安裝

1. 下載或克隆此儲存庫
2. 將整個資料夾複製到你的 Unity 專案的 `Packages` 目錄下
3. Unity 會自動識別並匯入套件

## 快速開始

### 1. 初始化 SDK

```csharp
using GameFrameX.Advertisement.TopOn.Runtime;

// 在遊戲啟動時初始化
TopOnAdvertisement.Initialize("your_app_id", "your_app_key");
```

### 2. 載入和顯示橫幅廣告

```csharp
// 載入橫幅廣告
TopOnAdvertisement.LoadBanner("banner_placement_id");

// 顯示橫幅廣告
TopOnAdvertisement.ShowBanner();

// 隱藏橫幅廣告
TopOnAdvertisement.HideBanner();
```

### 3. 載入和顯示插屏廣告

```csharp
// 載入插屏廣告
TopOnAdvertisement.LoadInterstitial("interstitial_placement_id");

// 顯示插屏廣告
if (TopOnAdvertisement.IsInterstitialReady())
{
    TopOnAdvertisement.ShowInterstitial();
}
```

### 4. 載入和顯示激勵影片

```csharp
// 載入激勵影片
TopOnAdvertisement.LoadRewardedVideo("rewarded_placement_id");

// 顯示激勵影片
if (TopOnAdvertisement.IsRewardedVideoReady())
{
    TopOnAdvertisement.ShowRewardedVideo();
}
```

## 事件回調

```csharp
// 註冊廣告事件監聽
TopOnAdvertisement.OnBannerLoaded += OnBannerLoaded;
TopOnAdvertisement.OnBannerFailed += OnBannerFailed;
TopOnAdvertisement.OnInterstitialLoaded += OnInterstitialLoaded;
TopOnAdvertisement.OnInterstitialFailed += OnInterstitialFailed;
TopOnAdvertisement.OnRewardedVideoLoaded += OnRewardedVideoLoaded;
TopOnAdvertisement.OnRewardedVideoRewarded += OnRewardedVideoRewarded;

private void OnBannerLoaded()
{
    Debug.Log("橫幅廣告載入成功");
}

private void OnBannerFailed(string error)
{
    Debug.LogError($"橫幅廣告載入失敗: {error}");
}

private void OnRewardedVideoRewarded(string placementId, string rewardName, int rewardAmount)
{
    Debug.Log($"獲得獎勵: {rewardName} x {rewardAmount}");
    // 在這裡給玩家發放獎勵
}
```

## 平台配置

### Android 配置

插件已包含必要的 Android 依賴配置，位於 `Plugins/Android/AndroidDependencies.xml`。

確保你的專案設定：

- Target API Level: 28 或更高
- 啟用 `Custom Main Gradle Template`
- 啟用 `Custom Gradle Properties Template`

### iOS 配置

iOS 平台的配置檔案位於 `Plugins/iOS/` 目錄下。

確保在 Xcode 專案中：

- 添加必要的系統框架
- 配置 App Transport Security 設定
- 添加廣告標識符權限

## 測試模式

在開發階段，建議啟用測試模式：

```csharp
// 啟用測試模式（僅在開發時使用）
TopOnAdvertisement.SetTestMode(true);
```

**注意：發布前務必關閉測試模式！**

## 常見問題

### Q: 廣告無法顯示？

A: 請檢查：

- 網路連接是否正常
- 廣告位 ID 是否正確
- 是否在正確的時機呼叫顯示方法
- 是否已正確初始化 SDK

### Q: iOS 建構失敗？

A: 請確保：

- Xcode 版本相容
- 已添加必要的系統框架
- 檢查程式碼簽名設定

### Q: Android 建構失敗？

A: 請檢查：

- Gradle 版本相容性
- 依賴衝突問題
- ProGuard 配置

## 更新日誌

### v1.1.0

- 添加支持 AdvertisementPlayOption 參數的 Play 方法多載

### v1.0.0

- 初始版本發布
- 支持橫幅、插屏、激勵影片廣告
- 支持 Android 和 iOS 平台

## 開源協議

本專案採用 MIT 許可證 - 詳見 [LICENSE](LICENSE) 檔案
