using Infra;
using Player;
using Ui.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ShiftButtonClicker: MonoBehaviour
    {
        private PlayerProfileController _playerProfileController;
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(Shift);
            _playerProfileController = ServiceLocator.GetService<PlayerProfileController>();
        }
        
        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(Shift);
        }
        
        private void Shift()
        {
            darkener.SetHighlight(targetUIElement);
        }
        
        public TutorialScreenDarkener darkener;
        public RectTransform targetUIElement;
        
    }
}