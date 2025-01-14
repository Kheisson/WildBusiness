using Infra;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Tutorial
{
    [RequireComponent(typeof(RectTransform))]
    public class TutorialElement : MonoBehaviour
    {
        [FormerlySerializedAs("_element")] [SerializeField] private ETutorialElementsType elementType;
        private RectTransform _rectTransform;
        
        public ETutorialElementsType ElementType => elementType;
        public RectTransform RectTransform => _rectTransform;
        
        private void Start() 
        {
            _rectTransform = GetComponent<RectTransform>();
            ServiceLocator.GetService<TutorialManager>().RegisterTutorialElement(this);
        }
    }
}