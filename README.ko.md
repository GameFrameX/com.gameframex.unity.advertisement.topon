<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Unity Advertisement (TopOn)

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.advertisement.topon)](https://github.com/GameFrameX/com.gameframex.unity.advertisement.topon/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 프로젝트 개요

Unity TopOn 광고 SDK 통합 플러그인. GameFrameX를 위한 완전한 광고 솔루션을 제공합니다.

## 기능 특징

- 다양한 광고 유형 지원 (배너, 전면, 리워드 영상, 네이티브 광고)
- Android 및 iOS 플랫폼 지원
- 간단하고 사용하기 쉬운 API 인터페이스
- 완전한 광고 이벤트 콜백
- 내장 오류 처리 및 재시도 메커니즘

## 지원 광고 유형

- **배너 광고 (Banner)** - 페이지 상단 또는 하단의 소형 광고
- **전면 광고 (Interstitial)** - 전체 화면 광고, 레벨 사이에 표시하기 적합
- **리워드 영상 (Rewarded Video)** - 사용자가 전체 영상을 시청한 후 보상 획득
- **네이티브 광고 (Native)** - 앱 콘텐츠와 융합되는 맞춤형 광고

## 설치 방법

### Unity Package Manager를 통한 설치

1. Unity 에디터 열기
2. `Window` > `Package Manager` 선택
3. 좌측 상단의 `+` 버튼 클릭
4. `Add package from git URL` 선택
5. 패키지 Git URL 또는 로컬 경로 입력

### 수동 설치

1. 이 저장소를 다운로드하거나 클론
2. 전체 폴더를 Unity 프로젝트의 `Packages` 디렉토리에 복사
3. Unity가 자동으로 패키지를 인식하고 가져옵니다

## 빠른 시작

### 설치

Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:

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

`scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.advertisement.topon": "1.2.0"
  }
}
```

## 이벤트 콜백

```csharp
// 광고 이벤트 리스너 등록
TopOnAdvertisement.OnBannerLoaded += OnBannerLoaded;
TopOnAdvertisement.OnBannerFailed += OnBannerFailed;
TopOnAdvertisement.OnInterstitialLoaded += OnInterstitialLoaded;
TopOnAdvertisement.OnInterstitialFailed += OnInterstitialFailed;
TopOnAdvertisement.OnRewardedVideoLoaded += OnRewardedVideoLoaded;
TopOnAdvertisement.OnRewardedVideoRewarded += OnRewardedVideoRewarded;

private void OnBannerLoaded()
{
    Debug.Log("배너 광고 로드 성공");
}

private void OnBannerFailed(string error)
{
    Debug.LogError($"배너 광고 로드 실패: {error}");
}

private void OnRewardedVideoRewarded(string placementId, string rewardName, int rewardAmount)
{
    Debug.Log($"보상 획득: {rewardName} x {rewardAmount}");
    // 여기서 플레이어에게 보상 지급
}
```

## 플랫폼 설정

### Android 설정

플러그인에 필요한 Android 종속성 설정이 `Plugins/Android/AndroidDependencies.xml`에 포함되어 있습니다.

프로젝트 설정 확인:

- Target API Level: 28 이상
- `Custom Main Gradle Template` 활성화
- `Custom Gradle Properties Template` 활성화

### iOS 설정

iOS 플랫폼 설정 파일은 `Plugins/iOS/` 디렉토리에 있습니다.

Xcode 프로젝트에서 확인:

- 필요한 시스템 프레임워크 추가
- App Transport Security 설정 구성
- 광고 식별자 권한 추가

## 테스트 모드

개발 단계에서는 테스트 모드를 활성화하는 것을 권장합니다:

```csharp
// 테스트 모드 활성화 (개발 시에만 사용)
TopOnAdvertisement.SetTestMode(true);
```

**참고: 릴리스 전에 반드시 테스트 모드를 비활성화하세요!**

## 자주 묻는 질문

### Q: 광고가 표시되지 않나요?

A: 다음을 확인하세요:

- 네트워크 연결이 정상인지
- 광고 배치 ID가 올바른지
- 올바른 시점에 표시 메서드를 호출했는지
- SDK가 올바르게 초기화되었는지

### Q: iOS 빌드가 실패하나요?

A: 다음을 확인하세요:

- Xcode 버전 호환성
- 필요한 시스템 프레임워크가 추가되었는지
- 코드 서명 설정 확인

### Q: Android 빌드가 실패하나요?

A: 다음을 확인하세요:

- Gradle 버전 호환성
- 종속성 충돌
- ProGuard 설정

## 변경 로그

### v1.1.0

- AdvertisementPlayOption 매개변수를 받는 Play 메서드 오버로드 추가

### v1.0.0

- 초기 버전 출시
- 배너, 전면, 리워드 영상 광고 지원
- Android 및 iOS 플랫폼 지원


## 의존성

| 패키지 | 설명 |
|--------|------|
| (无) | - |


## 문서 및 자료

- [문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: 467608841 / 233840761
## 라이선스

자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.
