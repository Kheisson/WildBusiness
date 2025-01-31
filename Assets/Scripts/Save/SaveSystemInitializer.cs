using Infra;
using Infra.Injector;
using UnityEngine.Device;

namespace Save
{
    public class SaveSystemInitializer : IInjector
    {
        public InjectionType InjectionTiming => InjectionType.Instantly;
        
        public void Inject()
        {
            var saveSerializer = new JsonSaveSerializer();
            #if UNITY_WEBGL
            var saveStorage = new PlayerPrefsSaveStorage();
            #else
            var saveStorage = new FileSaveStorage(Application.persistentDataPath);
            #endif
            
            var saveSystem = new SaveManager(saveSerializer, saveStorage);
            
            ServiceLocator.RegisterService<ISaveSerializer>(saveSerializer);
            ServiceLocator.RegisterService<ISaveStorage>(saveStorage);
            ServiceLocator.RegisterService(saveSystem);
        }
    }
}