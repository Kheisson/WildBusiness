using UnityEngine;

namespace Infra
{
    public static class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            GameInitializer.Instance.Initialize();
        }
    }
}