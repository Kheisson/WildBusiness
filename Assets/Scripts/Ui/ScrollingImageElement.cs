using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ScrollingImageElement : MonoBehaviour
    {
        private static readonly int CustomTime = Shader.PropertyToID("_CustomTime");
        private static readonly int ScrollSpeed = Shader.PropertyToID("_ScrollSpeed");
    
        private Material _material;
        private float _customTime;
    
        [SerializeField] private float _scrollSpeed = 0.1f;

        private void Awake()
        {
            _material = GetComponent<Image>().material;
        }

        private void Update()
        {
            _customTime += Time.deltaTime;
            _material.SetFloat(CustomTime, _customTime);
            _material.SetFloat(ScrollSpeed, _scrollSpeed);
        }
    }
}