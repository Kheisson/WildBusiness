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
        private bool _firstRun = true;
        private Action _onShiftFinished;
        private RectTransform _rectTransform;
        private Button _button;
        private float _moveStep;
        private int _currentClickCount = 0;
        private int _numberOfClicks = 0;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnChequeButtonClick);
        }

        public void Initialize(RectTransform parent, int clickCount, Action onShiftFinished)
        {
            _numberOfClicks = clickCount;
            _moveStep = parent.rect.height / _numberOfClicks;
            _button.interactable = false;
            _onShiftFinished = onShiftFinished;
        }

        public void ShiftButtonClicked()
        {
            if (_currentClickCount < _numberOfClicks)
            {
                _rectTransform.anchoredPosition += Vector2.up * _moveStep;
                _currentClickCount++;
            }

            if (_currentClickCount != _numberOfClicks) return;
            
            _button.interactable = true;

            if (!_firstRun) return;
            
            ServiceLocator.GetService<TutorialManager>().ShowTutorialElement(ETutorialElementsType.Cheque);
            _firstRun = false;
        }

        private void OnChequeButtonClick()
        {
            ServiceLocator.GetService<TutorialManager>().HideTutorialElement(ETutorialElementsType.Cheque);
            _rectTransform.anchoredPosition = Vector2.zero;
            _button.interactable = false;
            _currentClickCount = 0;
            _onShiftFinished?.Invoke();
        }
    }
}