using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance { get; private set; }

        public event Action<List<RectTransform>> OnShowTutorial;
        public event Action<List<RectTransform>> OnHideTutorial;

        private HashSet<TutorialElement> activeElements = new HashSet<TutorialElement>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShowTutorial(params RectTransform[] elements)
        {
            List<RectTransform> newElements = new List<RectTransform>();
            foreach (var element in elements)
            {
                var tutorialElement = element.GetComponent<TutorialElement>();
                if (tutorialElement != null && !activeElements.Contains(tutorialElement))
                {
                    tutorialElement.Show();
                    activeElements.Add(tutorialElement);
                    newElements.Add(element);
                }
            }
            OnShowTutorial?.Invoke(newElements);
        }

        public void HideTutorial(params RectTransform[] elements)
        {
            List<RectTransform> hiddenElements = new List<RectTransform>();
            foreach (var element in elements)
            {
                var tutorialElement = element.GetComponent<TutorialElement>();
                if (tutorialElement != null && activeElements.Contains(tutorialElement))
                {
                    tutorialElement.Hide();
                    activeElements.Remove(tutorialElement);
                    hiddenElements.Add(element);
                }
            }
            OnHideTutorial?.Invoke(hiddenElements);
        }

        public void HideAllTutorials()
        {
            List<RectTransform> allHiddenElements = new List<RectTransform>();
            foreach (var tutorialElement in activeElements)
            {
                tutorialElement.Hide();
                allHiddenElements.Add(tutorialElement.GetComponent<RectTransform>());
            }
            activeElements.Clear();
            OnHideTutorial?.Invoke(allHiddenElements);
        }
    }
}
