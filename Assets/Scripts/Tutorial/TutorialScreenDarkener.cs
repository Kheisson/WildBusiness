using System;
using Cysharp.Threading.Tasks;
using Infra;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    [RequireComponent(typeof(Image))]
    public class TutorialScreenDarkener : MonoBehaviour
    {
        private static readonly int MaskCenter = Shader.PropertyToID("_MaskCenter");
        private static readonly int MaskSize = Shader.PropertyToID("_MaskSize");
        private static readonly int OverlayColor = Shader.PropertyToID("_OverlayColor");
        
        private static readonly Color InitialColor = new Color(0, 0, 0, 0);
        private static readonly Color HighlightColor = new Color(0, 0, 0, 0.9f);
        
        private const string SHADER_NAME = "Unlit/Blackhole";
        
        private RectTransform _canvasRectTransform;
        private Camera _camera;
        private Material _material;
        private Image _darkenImage;
        private bool _isHighlighted;

        public GameObject handPrefab;  // Assign this in the Inspector
        private GameObject _handInstance; // Store the instantiated hand object

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
            _darkenImage = GetComponent<Image>();
            _canvasRectTransform = _darkenImage.canvas.GetComponent<RectTransform>();
            _material = new Material(Shader.Find(SHADER_NAME));
            _darkenImage.material = _material;
            _camera = Camera.main;
            InitializeDefaultEffect();
        }

        private void InitializeDefaultEffect()
        {
            _material.SetVector(MaskCenter, new Vector4(0.5f, 0.5f, 0, 0));
            _material.SetVector(MaskSize, new Vector4(1.0f, 1.0f, 0, 0));
            _material.SetColor(OverlayColor, InitialColor);
        }

        public void SetHighlight(RectTransform target, float delay = 0)
        {
            if (delay > 0)
            {
                _ = UniTask.Delay(TimeSpan.FromSeconds(delay)).ContinueWith(() => Show(target));
            }
            else
            {
                Show(target);
            }
        }
        
        public void Hide()
        {
            if (!_isHighlighted) return;
            
            _darkenImage.color = InitialColor;
            _material.SetColor(OverlayColor, InitialColor);

            if (_handInstance != null)
            {
                Destroy(_handInstance);
            }
            
            _isHighlighted = false;
        }

        private void Show(RectTransform target)
        {
            if (_isHighlighted) return;

            var screenPoint = RectTransformUtility.WorldToScreenPoint(_camera, target.position);

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, screenPoint, _camera,
                out var localPoint))
            {
                return;
            }

            var pivotAdjusted = new Vector2(localPoint.x / _canvasRectTransform.rect.width + 0.5f,
                localPoint.y / _canvasRectTransform.rect.height + 0.5f);

            var sizeViewport = new Vector2(target.rect.width / _canvasRectTransform.rect.width,
                target.rect.height / _canvasRectTransform.rect.height);

            _material.SetVector(MaskCenter, new Vector4(pivotAdjusted.x, pivotAdjusted.y, 0, 0));
            _material.SetVector(MaskSize, new Vector4(sizeViewport.x, sizeViewport.y, 0, 0));
            _material.SetColor(OverlayColor, HighlightColor);

            _darkenImage.color = HighlightColor;
            _isHighlighted = true;

            if (handPrefab == null) return;
            
            localPoint.x += 25;
            localPoint.y -= 25;
            _handInstance = Instantiate(handPrefab, _canvasRectTransform);
            _handInstance.GetComponent<RectTransform>().anchoredPosition = localPoint;
        }
    }
}
