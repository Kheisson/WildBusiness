using Infra.Events;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    [RequireComponent(typeof(RectTransform), typeof(Button))]
    public class Cheque : MonoBehaviour
    {
        private const int DEFAULT_CHEQUE_VALUE = 10;
        
        private RectTransform _rectTransform;
        private Button _button;
        private TutorialHandler _tutorialHandler;

        private bool _isFirstRun = true;
        private float _shiftStep;
        private int _currentClickCount = 0;
        private int _numberOfClicks = 0;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnChequeButtonClick);
        }

        public void Initialize(RectTransform parent, int clickCount, TutorialHandler tutorialHandler)
        {
            _button.interactable = false;
            _tutorialHandler = tutorialHandler;
            _numberOfClicks = clickCount;
            _shiftStep = parent.rect.height / _numberOfClicks;
        }

        public void ShiftButton()
        {
            if (_currentClickCount < _numberOfClicks)
            {
                _rectTransform.anchoredPosition += Vector2.up * _shiftStep;
                _currentClickCount++;
            }

            if (_currentClickCount == _numberOfClicks)
            {
                _button.interactable = true;

                if (!_isFirstRun) return;

                _isFirstRun = false;
                StartTutorial();
            }
        }

        private void StartTutorial()
        {
            _button.interactable = false;
            _tutorialHandler.ShowTutorial(ETutorialElementsType.Cheque, () =>
            {
                _button.interactable = true;
            });
        }

        private void OnChequeButtonClick()
        {
            _tutorialHandler.HideTutorial(ETutorialElementsType.Cheque);
            EventManager.TriggerEvent(new ChequeCollectedEventArgs(DEFAULT_CHEQUE_VALUE));
            ResetCheque();
        }

        private void ResetCheque()
        {
            _rectTransform.anchoredPosition = Vector2.zero;
            _currentClickCount = 0;
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnChequeButtonClick);
            }
        }
    }
}
