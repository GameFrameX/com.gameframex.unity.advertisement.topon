//
//  GameFrameXAdvertisementTopOnViewController.h
//
//  Created by GameFrameX(AlianBlank) on 2025/8/25.
//  https://github.com/gameframex
//  https://github.com/alianblank
//


#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

// TopOn 激励视频广告消息类型常量
extern NSString * const kTopOnRewardVideoLoaded;
extern NSString * const kTopOnRewardVideoLoadFailed;
extern NSString * const kTopOnRewardVideoShow;
extern NSString * const kTopOnRewardVideoShowFailed;
extern NSString * const kTopOnRewardVideoClose;
extern NSString * const kTopOnRewardVideoReward;
extern NSString * const kTopOnRewardVideoClick;

@interface GameFrameXAdvertisementTopOnViewController : NSObject

/**
  初始化SDK 
 @param appId 应用ID
 @param appKey 应用Key
 @param isDebug 是否是调试模式
 */
- (void)initialize:(NSString*) appId withAppKey:(NSString *) appKey withDebug:(BOOL)isDebug;

/**
 加载激励视频广告
 @param placementId 广告位ID
 @param customData 自定义数据
 */
- (void)loadRewardedVideoAd:(NSString*)placementId withCustomData:(NSString*)customData;

/**
 展示激励视频广告
 @param placementId 广告位ID
 @param customData 自定义数据
 */
- (void)showRewardedVideoAd:(NSString*)placementId withCustomData:(NSString*)customData;

/**
 检查激励视频广告是否准备好
 @param placementId 广告位ID
 @return 是否准备好
 */
- (BOOL)isRewardedVideoAdReady:(NSString*)placementId;

@end

NS_ASSUME_NONNULL_END
