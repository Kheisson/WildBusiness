using Infra;
using UnityEngine;

namespace Tutorial
{
    public class TutorialHandler : MonoBehaviour
    {
        private TutorialManager _tutorialManager;

        private void Awake()
        {
            _tutorialManager = ServiceLocator.GetService<TutorialManager>();
        }

        public void ShowTutorial(ETutorialElementsType elementType, System.Action onComplete)
        {
            _tutorialManager?.ShowTutorialElement(elementType, onComplete);
        }

        public void HideTutorial(ETutorialElementsType elementType)
        {
            _tutorialManager?.HideTutorialElement(elementType, null);
        }
    }
}