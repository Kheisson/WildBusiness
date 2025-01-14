using System;
using DG.Tweening;
using UnityEngine;

namespace Ui.Animation
{
    public class HandHintAnimation : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Sequence _handSequence;
        public float scaleAmount = 1.2f; 
        public float duration = 1f;     

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            AnimateHand();
        }

        private void AnimateHand()
        {
            _handSequence = DOTween.Sequence();

            _handSequence.Append(_rectTransform.DOScale(Vector3.one * scaleAmount, duration / 2).SetEase(Ease.InOutQuad))
                .Append(_rectTransform.DOScale(Vector3.one, duration / 2).SetEase(Ease.InOutQuad));

            _handSequence.SetLoops(-1);
        }

        private void OnDestroy()
        {
            _handSequence?.Kill();
        }
    }
}