//
//  GameFrameXAdvertisementTopBridgeLink.m
//
//  Created by GameFrameX(AlianBlank) on 2025/8/25.
//  https://github.com/gameframex
//  https://github.com/alianblank
//


#import "GameFrameXAdvertisementTopOnViewController.h"

// 全局实例
static GameFrameXAdvertisementTopOnViewController *topOnController = nil;

// 获取或创建TopOn控制器实例
GameFrameXAdvertisementTopOnViewController* getTopOnController() {
    if (topOnController == nil) {
        topOnController = [[GameFrameXAdvertisementTopOnViewController alloc] init];
    }
    return topOnController;
}

/**
 * 初始化TopOn SDK
 * @param appid - TopOn平台申请的应用ID
 * @param appKey - TopOn平台申请的应用密钥
 * @param debug - 是否开启调试模式，传入"true"或"false"
 * @note 在使用SDK其他功能前必须先调用此方法进行初始化
 */
extern "C" void __gfx_top_on_ad_init(char * appid, char * appKey, char * debug) {
    NSString *appIdStr = [NSString stringWithUTF8String:appid];
    NSString *appKeyStr = [NSString stringWithUTF8String:appKey];
    NSString *debugStr = [NSString stringWithUTF8String:debug];
    BOOL isDebug = [debugStr boolValue];
    
    GameFrameXAdvertisementTopOnViewController *controller = getTopOnController();
    [controller initialize:appIdStr withAppKey:appKeyStr withDebug:isDebug];
}

/**
 * 加载激励视频广告
 * @param placementId - 广告位ID
 * @param customData - 自定义数据，可传入空字符串
 * @note 建议在展示广告前预加载，以提供更好的用户体验
 */
extern "C" void __gfx_top_on_ad_load_rewarded(char * placementId, char * customData) {
    NSString *placementIdStr = [NSString stringWithUTF8String:placementId];
    NSString *customDataStr = customData ? [NSString stringWithUTF8String:customData] : @"";
    
    GameFrameXAdvertisementTopOnViewController *controller = getTopOnController();
    [controller loadRewardedVideoAd:placementIdStr withCustomData:customDataStr];
}

/**
 * 展示激励视频广告
 * @param placementId - 广告位ID
 * @param customData - 自定义数据，可传入空字符串
 * @note 在展示前请先调用isReady方法检查广告是否准备就绪
 */
extern "C" void __gfx_top_on_ad_show_rewarded(char * placementId, char * customData) {
    NSString *placementIdStr = [NSString stringWithUTF8String:placementId];
    NSString *customDataStr = customData ? [NSString stringWithUTF8String:customData] : @"";
    
    GameFrameXAdvertisementTopOnViewController *controller = getTopOnController();
    [controller showRewardedVideoAd:placementIdStr withCustomData:customDataStr];
}

/**
 * 检查激励视频广告是否准备好
 * @param placementId - 广告位ID
 * @return 返回"true"表示广告准备就绪，可以展示；返回"false"表示广告未准备好
 * @note 返回的字符串需要在使用完后手动释放内存
 */
extern "C" char* __gfx_top_on_ad_is_ready_rewarded(char * placementId) {
    NSString *placementIdStr = [NSString stringWithUTF8String:placementId];
    
    GameFrameXAdvertisementTopOnViewController *controller = getTopOnController();
    BOOL isReady = [controller isRewardedVideoAdReady:placementIdStr];
    
    NSString *result = isReady ? @"true" : @"false";
    char* cString = (char*)malloc(strlen([result UTF8String]) + 1);
    strcpy(cString, [result UTF8String]);
    return cString;
}
