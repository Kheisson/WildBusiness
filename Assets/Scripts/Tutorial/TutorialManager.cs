using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infra;

namespace Tutorial
{
    public class TutorialManager
    {
        private readonly Dictionary<ETutorialElementsType, TutorialElement> _tutorialElements = new();
        private TutorialScreenDarkener _tutorialScreenDarkener;
        private ETutorialElementsType _currentTutorialElement;

        public void RegisterTutorialElement(TutorialElement tutorialElement)
        {
            _tutorialElements.TryAdd(tutorialElement.ElementType, tutorialElement);
        }
        
        public async Task ShowTutorialElement(ETutorialElementsType elementType, Action onTutorialShown, float delay = 1f)
        {
            if (_tutorialScreenDarkener == null)
            {
                _tutorialScreenDarkener = ServiceLocator.GetService<TutorialScreenDarkener>();
            }
            
            if (_tutorialElements.TryGetValue(elementType, out var tutorialElement))
            {
                tutorialElement.RectTransform.gameObject.SetActive(true);
                _tutorialScreenDarkener.SetHighlight(tutorialElement.RectTransform, delay);
                _currentTutorialElement = elementType;
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            onTutorialShown?.Invoke();
        }
        
        public void HideTutorialElement(ETutorialElementsType elementType, Action onTutorialHidden = null)
        {
            if (_currentTutorialElement == ETutorialElementsType.None) return;
            
            if (_currentTutorialElement != elementType)
            {
                LlamaLog.LogWarning("Tutorial element is not the current element. Current: " + _currentTutorialElement + " Requested: " + elementType);
                return;
            }
            
            _tutorialScreenDarkener.Hide(); 
            onTutorialHidden?.Invoke();
            _currentTutorialElement = ETutorialElementsType.None;
        }
    }
}
