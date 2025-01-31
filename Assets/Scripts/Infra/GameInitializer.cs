using System.Collections.Generic;
using Infra.Injector;
using Save;
using Tutorial;

namespace Infra
{
    public class GameInitializer : SingletonMono<GameInitializer>
    {
        private readonly List<IInjector> _injectors = new List<IInjector>();

        public void RegisterInjector(IInjector injector, InjectionType injectionType)
        {
            _injectors.Add(injector);

            if (injectionType == InjectionType.Instantly)
            {
                injector.Inject();
            }
        }

        private void Awake()
        {
            foreach (var injector in _injectors)
            {
                if (injector.InjectionTiming == InjectionType.Awake)
                {
                    injector.Inject();
                }
            }
        }

        private void Start()
        {
            foreach (var injector in _injectors)
            {
                if (injector.InjectionTiming == InjectionType.Start)
                {
                    injector.Inject();
                }
            }
        }

        private void OnEnable()
        {
            foreach (var injector in _injectors)
            {
                if (injector.InjectionTiming == InjectionType.OnEnable)
                {
                    injector.Inject();
                }
            }
        }
        
        private void InitializeSaveSystem()
        {
            var saveSystem = new SaveSystemInitializer();
            RegisterInjector(saveSystem, saveSystem.InjectionTiming);
        }
        
        private void InitializeTutorialManager()
        {
            var tutorialManager = new TutorialManager();
            ServiceLocator.RegisterService(tutorialManager);
        }

        public void Initialize()
        {
            InitializeSaveSystem();
            InitializeTutorialManager();
        }
    }
}