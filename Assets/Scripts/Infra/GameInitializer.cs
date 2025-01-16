using System.Collections.Generic;
using Infra.Injector;
using Ui.Tutorial;

namespace Infra
{
    public class GameInitializer : SingletonMono<GameInitializer>
    {
        private readonly List<IInjector> _injectors = new List<IInjector>();
        private TutorialManager _tutorialManager;

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
        
        private void InitializeTutorialManager()
        {
            _tutorialManager = new TutorialManager();
            ServiceLocator.RegisterService(_tutorialManager);
        }

        public void Initialize()
        {
            InitializeTutorialManager();
        }
    }
}