using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.Advertisement.TopOn.Runtime
{
    [Preserve]
    public class GameFrameXTopOnCroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
            _ = typeof(TopOnAdvertisementManager);
            _ = typeof(TopOnAdvertisementBridgeLink);
        }
    }
}