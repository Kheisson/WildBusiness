using UnityEngine;

namespace Ui.Tutorial
{
    [RequireComponent(typeof(RectTransform), typeof(TutorialScreenDarkener))]
    public class TutorialElement : MonoBehaviour
    {
        private TutorialScreenDarkener darkener;

        private void Awake()
        {
            darkener = GetComponent<TutorialScreenDarkener>();
        }

        public void Show()
        {
            darkener.SetHighlight(GetComponent<RectTransform>());
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}