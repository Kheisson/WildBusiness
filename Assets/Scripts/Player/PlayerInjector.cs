using Infra;
using Infra.Injector;
using Ui;
using UnityEngine;

namespace Player
{
    public class PlayerInjector : InjectorMono
    {
        [SerializeField] private PlayerProfileSo _playerProfileData;
        [SerializeField] private PlayerProfileView _playerProfileView;
        
        public override InjectionType InjectionTiming => InjectionType.Instantly;

        private void Awake()
        {
            GameInitializer.Instance.RegisterInjector(this, InjectionType.Instantly);
        }

        public override void Inject()
        {
            var controller = new PlayerProfileController(_playerProfileData, _playerProfileView);
            ServiceLocator.RegisterService(controller);
        }
    }
}