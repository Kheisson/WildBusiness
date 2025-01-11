namespace Infra.Injector
{
    public interface IInjector
    {
        public void Inject();
        InjectionType InjectionTiming { get; }
    }
}