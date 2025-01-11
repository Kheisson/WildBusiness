using UnityEngine;

namespace Infra.Injector
{
    public abstract class InjectorMono : MonoBehaviour, IInjector
    {
        public abstract void Inject();
        public abstract InjectionType InjectionTiming { get; }
    }
}