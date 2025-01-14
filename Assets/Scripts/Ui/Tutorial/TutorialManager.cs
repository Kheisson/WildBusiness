using System;
using System.Collections.Generic;
using Infra;

namespace Ui.Tutorial
{
    public class TutorialManager
    {
        public event Action<ETutorialElementsType> OnShowTutorial;
        public event Action<ETutorialElementsType> OnHideTutorial;
        
        private readonly Dictionary<ETutorialElementsType, TutorialElement> _tutorialElements = new();
        private TutorialScreenDarkener _tutorialScreenDarkener;


        public void RegisterTutorialElement(TutorialElement tutorialElement)
        {
            _tutorialElements.TryAdd(tutorialElement.ElementType, tutorialElement);
        }
        
        public void ShowTutorialElement(ETutorialElementsType elementType)
        {
            if (_tutorialScreenDarkener == null)
            {
                _tutorialScreenDarkener = ServiceLocator.GetService<TutorialScreenDarkener>();
            }
            
            if (_tutorialElements.TryGetValue(elementType, out var tutorialElement))
            {
                tutorialElement.RectTransform.gameObject.SetActive(true);
                _tutorialScreenDarkener.SetHighlight(tutorialElement.RectTransform, 1f);
                OnShowTutorial?.Invoke(elementType);
            }
        }
        
        public void HideTutorialElement(ETutorialElementsType elementType)
        {
            _tutorialScreenDarkener.Hide(); 
            OnHideTutorial?.Invoke(elementType);
        }
    }
}
