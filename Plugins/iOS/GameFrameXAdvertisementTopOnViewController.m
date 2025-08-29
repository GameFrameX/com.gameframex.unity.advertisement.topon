//
//  GameFrameXAdvertisementTopOnViewController.m
//  Unity-iPhone
//
//  Created by jiangjiayi on 2025/8/25.
//

#import "GameFrameXAdvertisementTopOnViewController.h"
#import <AnyThinkSDK/AnyThinkSDK.h>
#import <AnyThinkRewardedVideo/AnyThinkRewardedVideo.h>

NS_ASSUME_NONNULL_BEGIN

// Unity通信相关常量定义
NSString * const kUnityGameObjectName = @"TopOnAdvertisementBridgeLink";
NSString * const kUnityMethodName = @"OnMessage";

// TopOn 激励视频广告消息类型常量定义
NSString * const kTopOnRewardVideoLoaded = @"RewardVideoLoaded";
NSString * const kTopOnRewardVideoLoadFailed = @"RewardVideoLoadFailed";
NSString * const kTopOnRewardVideoShow = @"RewardVideoShow";
NSString * const kTopOnRewardVideoShowFailed = @"RewardVideoShowFailed";
NSString * const kTopOnRewardVideoClose = @"RewardVideoClose";
NSString * const kTopOnRewardVideoReward = @"RewardVideoReward";
NSString * const kTopOnRewardVideoClick = @"RewardVideoClick";

@interface GameFrameXAdvertisementTopOnViewController () <ATRewardedVideoDelegate>

@property (nonatomic, assign) NSInteger retryAttempt;
@property (nonatomic, strong) NSString *currentPlacementId;
@property (nonatomic, strong) NSString *currentCustomData;

@end


@implementation GameFrameXAdvertisementTopOnViewController

- (void)initialize:(NSString*) appId withAppKey:(NSString *) appKey withDebug:(BOOL)isDebug{
    if(isDebug){
        //开启日志
        [ATAPI setLogEnabled:YES];//Turn on debug logs

        //开启日志后，调用检查集成情况方法
        [ATAPI integrationChecking];
    }
    [[ATAPI sharedInstance] startWithAppID:appId appKey:appKey error:nil];
}



#pragma mark - 激励视频广告方法

- (void)loadRewardedVideoAd:(NSString*)placementId withCustomData:(NSString*)customData {
    // 保存当前的placementId和customData
    self.currentPlacementId = placementId;
    self.currentCustomData = customData;
    
    NSMutableDictionary * loadConfigDict = [NSMutableDictionary dictionary];
    // 添加自定义数据
    if (customData && customData.length > 0) {	
        [loadConfigDict setValue:customData forKey:kATADDelegateExtraUserCustomData];
    }
    
    [[ATAdManager sharedManager] loadADWithPlacementID:placementId extra:loadConfigDict delegate:self];
}

- (void)showRewardedVideoAd:(NSString*)placementId withCustomData:(NSString*)customData {
    // 保存当前的placementId和customData
    self.currentPlacementId = placementId;
    self.currentCustomData = customData;
    
    // 检查广告是否准备好
    if ([[ATAdManager sharedManager] rewardedVideoReadyForPlacementID:placementId]) {
        // 获取当前的根视图控制器
        UIViewController *rootViewController = [UIApplication sharedApplication].keyWindow.rootViewController;
        while (rootViewController.presentedViewController) {
            rootViewController = rootViewController.presentedViewController;
        }
        
        // 创建展示配置
        ATShowConfig *config = [[ATShowConfig alloc] initWithScene:@"" showCustomExt:customData];
        
        [[ATAdManager sharedManager] showRewardedVideoWithPlacementID:placementId config:config inViewController:rootViewController delegate:self];
    } else {
        // 广告未准备好，发送失败消息给Unity
        [self sendUnityMessage:kTopOnRewardVideoShowFailed withPlacementId:placementId customData:customData success:NO error:@"Ad not ready"];
    }
}

- (BOOL)isRewardedVideoAdReady:(NSString*)placementId {
    return [[ATAdManager sharedManager] rewardedVideoReadyForPlacementID:placementId];
}

#pragma mark - ATRewardedVideoDelegate 回调方法

// 激励视频广告加载成功
- (void)didFinishLoadingADWithPlacementID:(NSString *)placementID {
    NSLog(@"TopOn: 激励视频广告加载成功 - %@", placementID);
    [self sendUnityMessage:kTopOnRewardVideoLoaded withPlacementId:placementID customData:self.currentCustomData success:YES error:@""];
}

// 激励视频广告加载失败
- (void)didFailToLoadADWithPlacementID:(NSString*)placementID error:(NSError*)error {
    NSLog(@"TopOn: 激励视频广告加载失败 - %@, 错误: %@", placementID, error.localizedDescription);
    [self sendUnityMessage:kTopOnRewardVideoLoadFailed withPlacementId:placementID customData:self.currentCustomData success:NO error:error.localizedDescription];
}

// 激励视频广告开始播放
- (void)rewardedVideoDidStartPlayingForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra {
    NSLog(@"TopOn: 激励视频广告开始播放 - %@", placementID);
    [self sendUnityMessageWithAdInfo:kTopOnRewardVideoShow placementId:placementID customData:self.currentCustomData adInfo:extra];
}

// 激励视频广告播放结束
- (void)rewardedVideoDidEndPlayingForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra {
    NSLog(@"TopOn: 激励视频广告播放结束 - %@", placementID);
}

// 激励视频广告展示失败
- (void)didFailToShowRewardedVideoWithPlacementID:(NSString*)placementID error:(NSError*)error extra:(NSDictionary*)extra {
    NSLog(@"TopOn: 激励视频广告展示失败 - %@, 错误: %@", placementID, error.localizedDescription);
    [self sendUnityMessage:kTopOnRewardVideoShowFailed withPlacementId:placementID customData:self.currentCustomData success:NO error:error.localizedDescription];
}

// 激励视频广告关闭
- (void)rewardedVideoDidCloseForPlacementID:(NSString*)placementID rewarded:(BOOL)rewarded extra:(NSDictionary*)extra {
    NSLog(@"TopOn: 激励视频广告关闭 - %@, 是否获得奖励: %@", placementID, rewarded ? @"是" : @"否");
    [self sendUnityMessageWithAdInfo:kTopOnRewardVideoClose placementId:placementID customData:self.currentCustomData adInfo:extra];
}

// 激励视频广告点击
- (void)rewardedVideoDidClickForPlacementID:(NSString*)placementID extra:(NSDictionary*)extra {
    NSLog(@"TopOn: 激励视频广告被点击 - %@", placementID);
    [self sendUnityMessageWithAdInfo:kTopOnRewardVideoClick placementId:placementID customData:self.currentCustomData adInfo:extra];
}

// 激励视频广告奖励发放
- (void)rewardedVideoDidRewardSuccessForPlacemenID:(NSString*)placementID extra:(NSDictionary*)extra {
    NSLog(@"TopOn: 激励视频广告奖励发放成功 - %@", placementID);
    [self sendUnityMessageWithAdInfo:kTopOnRewardVideoReward placementId:placementID customData:self.currentCustomData adInfo:extra];
}

#pragma mark - Unity消息发送

// 发送简单消息到Unity（兼容旧版本）
- (void)sendMessageToUnity:(NSString*)messageType withData:(NSString*)data {
    NSString *message = [NSString stringWithFormat:@"%@|%@", messageType, data ? data : @""];
    UnitySendMessage([kUnityGameObjectName UTF8String], [kUnityMethodName UTF8String], [message UTF8String]);
}

// 发送JSON格式消息到Unity（与Java侧保持一致）
- (void)sendUnityMessage:(NSString*)messageType withPlacementId:(NSString*)placementId customData:(NSString*)customData success:(BOOL)success error:(NSString*)error {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    [jsonDict setObject:(placementId ? placementId : @"") forKey:@"placementId"];
    [jsonDict setObject:(customData ? customData : @"") forKey:@"customData"];
    [jsonDict setObject:@(success) forKey:@"success"];
    
    if (error && error.length > 0) {
        [jsonDict setObject:error forKey:@"error"];
    }
    
    NSError *jsonError;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonDict options:0 error:&jsonError];
    
    if (jsonError) {
        NSLog(@"TopOn: JSON序列化失败 - %@", jsonError.localizedDescription);
        return;
    }
    
    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    
    // 调用已有的sendMessageToUnity函数，避免代码重复
    [self sendMessageToUnity:messageType withData:jsonString];
    NSLog(@"TopOn: 发送Unity统一消息: %@ -> %@", messageType, jsonString);
}

// 发送带广告信息的JSON消息到Unity
- (void)sendUnityMessageWithAdInfo:(NSString*)messageType placementId:(NSString*)placementId customData:(NSString*)customData adInfo:(NSDictionary*)adInfo {
    NSMutableDictionary *jsonDict = [NSMutableDictionary dictionary];
    [jsonDict setObject:(placementId ? placementId : @"") forKey:@"placementId"];
    [jsonDict setObject:(customData ? customData : @"") forKey:@"customData"];
    [jsonDict setObject:@(YES) forKey:@"success"];
    
    // 打印adInfo内容以便查看key-value对
    if (adInfo) {
        NSLog(@"TopOn: adInfo内容: %@", adInfo);
        for (NSString *key in adInfo.allKeys) {
            NSLog(@"TopOn: adInfo[%@] = %@", key, adInfo[key]);
        }
    } else {
        NSLog(@"TopOn: adInfo为空");
    }
    
    // 添加广告信息（模拟Java侧的adInfo结构）
    if (adInfo) {
        // 基础广告信息
        [jsonDict setObject:(adInfo[@"networkName"] ? adInfo[@"networkName"] : @"unknown") forKey:@"networkName"];
        [jsonDict setObject:(placementId ? placementId : @"") forKey:@"adId"];
        [jsonDict setObject:@0 forKey:@"ecpm"];
        [jsonDict setObject:@"" forKey:@"currency"];
        [jsonDict setObject:@"" forKey:@"country"];
        
        // 网络相关信息
        [jsonDict setObject:@0 forKey:@"networkType"];
        [jsonDict setObject:@"" forKey:@"networkPlacementId"];
        [jsonDict setObject:@0 forKey:@"adNetworkType"];
        [jsonDict setObject:@0 forKey:@"adNetworkFirmId"];
        
        // 广告格式和类型
        [jsonDict setObject:@"" forKey:@"format"];
        [jsonDict setObject:@"" forKey:@"adType"];
        [jsonDict setObject:@"" forKey:@"adSourceId"];
        
        // 收益和竞价信息
        [jsonDict setObject:@0.0 forKey:@"publisherRevenue"];
        [jsonDict setObject:@"" forKey:@"ecpmPrecision"];
        
        // 扩展信息
        [jsonDict setObject:@"{}" forKey:@"ext"];
        
        // 请求和响应信息
        [jsonDict setObject:@"" forKey:@"requestId"];
        [jsonDict setObject:@"" forKey:@"topOnAdFormat"];
        
        // 高级功能信息
        [jsonDict setObject:@(NO) forKey:@"isHeaderBidding"];
        [jsonDict setObject:@"" forKey:@"segment"];
        [jsonDict setObject:@"" forKey:@"scenario"];
    }
    
    NSError *jsonError;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:jsonDict options:0 error:&jsonError];
    
    if (jsonError) {
        NSLog(@"TopOn: JSON序列化失败 - %@", jsonError.localizedDescription);
        return;
    }
    
    NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    
    // 调用已有的sendMessageToUnity函数，避免代码重复
    [self sendMessageToUnity:messageType withData:jsonString];
    NSLog(@"TopOn: 发送Unity统一消息: %@ -> %@", messageType, jsonString);
}

@end

NS_ASSUME_NONNULL_END
