using UnityEngine;
using UnityEngine.UI;

namespace Ui.Tutorial
{
    [RequireComponent(typeof(Image))]
    public class TutorialScreenDarkener : MonoBehaviour
    {
        private static readonly int MaskCenter = Shader.PropertyToID("_MaskCenter");
        private static readonly int MaskSize = Shader.PropertyToID("_MaskSize");
        private static readonly int OverlayColor = Shader.PropertyToID("_OverlayColor");
        
        private const string SHADER_NAME = "Unlit/Blackhole";
        
        private Material _material;
        private Image _darkenImage;

        private void Awake()
        {
            _darkenImage = GetComponent<Image>();
            _material = new Material(Shader.Find(SHADER_NAME));
            _darkenImage.material = _material;
            InitializeDefaultEffect();
        }

        private void InitializeDefaultEffect()
        {
            _material.SetVector(MaskCenter, new Vector4(0.5f, 0.5f, 0, 0));
            _material.SetVector(MaskSize, new Vector4(1.0f, 1.0f, 0, 0));
            _material.SetColor(OverlayColor, new Color(0, 0, 0, 0.5f));
        }

        public void SetHighlight(RectTransform target)
        {
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);

            var canvasRectTransform = _darkenImage.canvas.GetComponent<RectTransform>();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, Camera.main, out var localPoint))
            {
                var pivotAdjusted = new Vector2(localPoint.x / canvasRectTransform.rect.width + 0.5f,
                                                    localPoint.y / canvasRectTransform.rect.height + 0.5f);
                
                var sizeViewport = new Vector2(target.rect.width / canvasRectTransform.rect.width,
                                                    target.rect.height / canvasRectTransform.rect.height);
                
                _material.SetVector(MaskCenter, new Vector4(pivotAdjusted.x, pivotAdjusted.y, 0, 0));
                _material.SetVector(MaskSize, new Vector4(sizeViewport.x, sizeViewport.y, 0, 0));
            }
            
            _darkenImage.color = new Color(0, 0, 0, 0.5f);
        }
    }
}
