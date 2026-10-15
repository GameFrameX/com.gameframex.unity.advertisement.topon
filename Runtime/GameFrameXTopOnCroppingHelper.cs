using UnityEngine;

namespace GameFrameX.Advertisement.TopOn.Runtime
{
    [UnityEngine.Scripting.Preserve]
    public class GameFrameXTopOnCroppingHelper : MonoBehaviour
    {
        [UnityEngine.Scripting.Preserve]
        private void Start()
        {
            _ = typeof(TopOnAdvertisementManager);
            _ = typeof(TopOnAdvertisementBridgeLink);
            _ = typeof(TopOnAdvertisementConfig);
            _ = typeof(TopOnAdvertisementBootstrap);
        }
    }
}