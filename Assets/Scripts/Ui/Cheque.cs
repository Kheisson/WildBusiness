using System;
using Infra;
using Ui.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    [RequireComponent(typeof(RectTransform))]
    public class Cheque : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private TutorialManager _tutorialManager;
        private Action _onShiftFinished;
        private Button _button;

        private bool _isFirstRun = true;
        private float _shiftStep;
        private int _currentClickCount = 0;
        private int _numberOfClicks = 0;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            _tutorialManager = ServiceLocator.GetService<TutorialManager>();

            _button.onClick.AddListener(OnChequeButtonClick);
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnChequeButtonClick);
            }
        }

        public void Initialize(RectTransform parent, int clickCount, Action onShiftFinished)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Parent RectTransform cannot be null.");
            }

            if (clickCount <= 0)
            {
                throw new ArgumentException("Click count must be greater than zero.", nameof(clickCount));
            }

            _numberOfClicks = clickCount;
            _shiftStep = parent.rect.height / _numberOfClicks;
            _button.interactable = false;
            _onShiftFinished = onShiftFinished;
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
            _ = _tutorialManager.ShowTutorialElement(ETutorialElementsType.Cheque, () =>
            {
                _button.interactable = true;
            });
        }

        private void OnChequeButtonClick()
        {
            _tutorialManager.HideTutorialElement(ETutorialElementsType.Cheque, ResetCheque);
        }

        private void ResetCheque()
        {
            _rectTransform.anchoredPosition = Vector2.zero;
            _button.interactable = false;
            _currentClickCount = 0;
            _onShiftFinished?.Invoke();
        }
    }
}
