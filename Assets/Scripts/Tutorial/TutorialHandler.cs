using System.Collections.Generic;
using Infra;
using Save;
using UnityEngine;

namespace Tutorial
{
    public class TutorialHandler : MonoBehaviour
    {
        private TutorialManager _tutorialManager;

        private readonly List<ETutorialElementsType> _tutorialSteps = new ();

        private void Awake()
        {
            _tutorialManager = ServiceLocator.GetService<TutorialManager>();
            InitializeTutorialSteps();
        }

        private void InitializeTutorialSteps()
        {
            _tutorialSteps.Add(ETutorialElementsType.Clicker);
            _tutorialSteps.Add(ETutorialElementsType.Cheque);
        }

        public void ShowTutorial(ETutorialElementsType elementType, System.Action onComplete)
        {
            _tutorialManager?.ShowTutorialElement(elementType, onComplete);
        }

        public void HideTutorial(ETutorialElementsType elementType)
        {
            _tutorialManager?.HideTutorialElement(elementType ,() => TryRemoveTutorialStep(elementType));
        }
        
        private void TryRemoveTutorialStep(ETutorialElementsType elementType)
        {
            if (_tutorialSteps.Contains(elementType))
            {
                _tutorialSteps.Remove(elementType);
            }
            
            if (_tutorialSteps.Count == 0)
            {
                ServiceLocator.GetService<SaveManager>().Save(SaveKeys.COMPLETED_TUTORIAL, true);
            }
        }
    }
}