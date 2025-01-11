using System.Collections.Generic;
using Infra.Injector;

namespace Infra
{
    public class GameInitializer : SingletonMono<GameInitializer>
    {
        private List<IInjector> _injectors = new List<IInjector>();

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
    }
}