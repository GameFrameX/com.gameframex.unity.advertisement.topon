<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Unity Advertisement (TopOn)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## プロジェクト概要

Unity TopOn広告SDK統合プラグイン。GameFrameX向けの完全な広告ソリューションを提供します。

## 機能特徴

- 複数の広告タイプに対応（バナー、インタースティシャル、リワードビデオ、ネイティブ広告）
- AndroidおよびiOSプラットフォーム対応
- シンプルで使いやすいAPIインターフェース
- 完全な広告イベントコールバック
- 内蔵エラー処理とリトライメカニズム

## 対応広告タイプ

- **バナー広告 (Banner)** - ページの上部または下部に表示される小規模広告
- **インタースティシャル広告 (Interstitial)** - フルスクリーン広告、レベル間の表示に適しています
- **リワードビデオ (Rewarded Video)** - ユーザーが動画を最後まで視聴すると報酬を獲得
- **ネイティブ広告 (Native)** - アプリコンテンツと融合するカスタム広告

## アーキテクチャ

本パッケージは Game Frame X 広告コアの `BaseAdvertisementManager` の**アダプター実装**です。`TopOnAdvertisementBootstrap` が `BeforeSceneLoad` フェーズでコアパッケージの `AdvertisementRegistry` に自己登録します（本パッケージが未インストールの場合はコードがストリッピングされ、登録は行われません。参照することが登録です）。チャネルの初期化は Bootstrap のファクトリクロージャーにより厳密に型指定された設定を注入して行われます。

| クラス | 説明 |
|--------|------|
| `TopOnAdvertisementManager` | 広告マネージャー — ロード、表示、ライフサイクル管理 |
| `TopOnAdvertisementConfig` | appIdAndroid / appKeyAndroid / appIdiOS / appKeyiOS / adUnitId フィールドを含む広告設定。起動時に `TopOnAdvertisementBootstrap.Config` 経由で注入 |
| `TopOnAdvertisementBootstrap` | チャネル起動登録 — 起動時 `Config` 静的プロパティを持ち、`BeforeSceneLoad` で `AdvertisementRegistry` に自己登録 |
| `TopOnAdvertisementBridgeLink` | ネイティブブリッジコールバック処理（ブリッジコールバックの役割のみ保持） |

## インストール方法

### Unity Package Manager経由でインストール

1. Unityエディタを開く
2. `Window` > `Package Manager` を選択
3. 左上の `+` ボタンをクリック
4. `Add package from git URL` を選択
5. パッケージのGit URLまたはローカルパスを入力

### 手動インストール

1. このリポジトリをダウンロードまたはクローン
2. フォルダ全体をUnityプロジェクトの `Packages` ディレクトリにコピー
3. Unityが自動的にパッケージを認識してインポートします

## クイックスタート

### インストール

以下のいずれかの方法を選択してください：

1. Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：
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

   `scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

2. `manifest.json` の `dependencies` に直接追加：
   ```json
   {
      "com.gameframex.unity.advertisement.topon": "https://github.com/gameframex/com.gameframex.unity.advertisement.topon.git"
   }
   ```
3. Unity の **Package Manager** で **Git URL** を使用して追加：`https://github.com/gameframex/com.gameframex.unity.advertisement.topon.git`
4. リポジトリを Unity プロジェクトの `Packages` ディレクトリにクローンしてください。自動的に読み込まれます。

### チャネル登録と設定

起動時（初回の広告呼び出し前）に `TopOnAdvertisementBootstrap.Config` を設定します：

```csharp
using GameFrameX.Advertisement.TopOn.Runtime;

TopOnAdvertisementBootstrap.Config = new TopOnAdvertisementConfig
{
    appIdAndroid = "あなたの Android AppId",
    appKeyAndroid = "あなたの Android AppKey",
    appIdiOS = "あなたの iOS AppId",
    appKeyiOS = "あなたの iOS AppKey",
    adUnitId = "あなたの広告プレースメントID",
    isDebug = false,
};
```

- 本パッケージは `TopOnAdvertisementBootstrap` 経由で `AdvertisementRegistry` に自己登録します。チャネル定数は `ChannelId = "topon"` です；
- csj チャネルと併用する場合は、`AdvertisementRegistry.ResolveManager("topon")` で明示的にルーティングし手動選択できます。
## イベントコールバック

```csharp
// 広告イベントリスナーを登録
TopOnAdvertisement.OnBannerLoaded += OnBannerLoaded;
TopOnAdvertisement.OnBannerFailed += OnBannerFailed;
TopOnAdvertisement.OnInterstitialLoaded += OnInterstitialLoaded;
TopOnAdvertisement.OnInterstitialFailed += OnInterstitialFailed;
TopOnAdvertisement.OnRewardedVideoLoaded += OnRewardedVideoLoaded;
TopOnAdvertisement.OnRewardedVideoRewarded += OnRewardedVideoRewarded;

private void OnBannerLoaded()
{
    Debug.Log("バナー広告の読み込みに成功しました");
}

private void OnBannerFailed(string error)
{
    Debug.LogError($"バナー広告の読み込みに失敗しました: {error}");
}

private void OnRewardedVideoRewarded(string placementId, string rewardName, int rewardAmount)
{
    Debug.Log($"報酬を獲得: {rewardName} x {rewardAmount}");
    // ここでプレイヤーに報酬を付与
}
```

## プラットフォーム設定

### Android設定

プラグインに必要なAndroid依存関係設定が `Plugins/Android/AndroidDependencies.xml` に含まれています。

プロジェクト設定を確認：

- Target API Level: 28以上
- `Custom Main Gradle Template` を有効化
- `Custom Gradle Properties Template` を有効化

### iOS設定

iOSプラットフォームの設定ファイルは `Plugins/iOS/` ディレクトリにあります。

Xcodeプロジェクトで以下を確認：

- 必要なシステムフレームワークを追加
- App Transport Security設定を構成
- 広告識別子の権限を追加

## テストモード

開発段階では、テストモードを有効にすることを推奨します：

```csharp
// テストモードを有効化（開発時のみ使用）
TopOnAdvertisement.SetTestMode(true);
```

**注意：リリース前に必ずテストモードを無効にしてください！**

## よくある質問

### Q: 広告が表示されない？

A: 以下を確認してください：

- ネットワーク接続が正常か
- 広告プレースメントIDが正しいか
- 適切なタイミングで表示メソッドを呼び出しているか
- SDKが正しく初期化されているか

### Q: iOSビルドが失敗する？

A: 以下を確認してください：

- Xcodeバージョンの互換性
- 必要なシステムフレームワークが追加されているか
- コード署名設定を確認

### Q: Androidビルドが失敗する？

A: 以下を確認してください：

- Gradleバージョンの互換性
- 依存関係の競合
- ProGuard設定

## 変更履歴

### v2.0.0

- チャネルは `TopOnAdvertisementBootstrap` により `BeforeSceneLoad` フェーズでコアパッケージの `AdvertisementRegistry` に自己登録する方式に変更。設定は起動時に `TopOnAdvertisementBootstrap.Config` 経由で注入
- **破壊的変更**：appId / appKey をシーン上の `TopOnAdvertisementBridgeLink` のシリアライズフィールドから `TopOnAdvertisementBootstrap.Config` へ移行。BridgeLink はブリッジコールバックの役割のみ保持し、既存プロジェクトでは設定の手動移行が必要です

### v1.1.0

- AdvertisementPlayOptionパラメータを受け取るPlayメソッドのオーバーロードを追加

### v1.0.0

- 初版リリース
- バナー、インタースティシャル、リワードビデオ広告に対応
- AndroidおよびiOSプラットフォームに対応


## 依存関係

| パッケージ | 説明 |
|----------|------|
| (无) | - |


## ドキュメントとリソース

- [ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQグループ: 467608841 / 233840761
## ライセンス

詳しくは [LICENSE.md](LICENSE.md) をご参照ください。
