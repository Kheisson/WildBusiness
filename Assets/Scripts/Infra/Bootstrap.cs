using Save;
using Tutorial;
using UnityEngine;

namespace Infra
{
    public static class Bootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            InitializeSaveSystem();
            InitializeTutorialManager();
        }
        
        private static void InitializeSaveSystem()
        {
            var saveSystem = new SaveSystemInitializer();
            saveSystem.Inject();
        }
        
        private static void InitializeTutorialManager()
        {
            var tutorialManager = new TutorialManager();
            ServiceLocator.RegisterService(tutorialManager);
        }
    }
}