using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Scenes
{
    public class IntroManager : MonoBehaviour
    {
        [SerializeField] private VideoPlayer introVideo;
        [SerializeField] private Button skipButton;
        
        private Coroutine _showButtonCooldownCoroutine;

        private void Awake()
        {
            introVideo.loopPointReached += OnIntroFinished;
        }
        
        private void Start()
        {
            introVideo.Play();
            skipButton.onClick.AddListener(SkipIntro);
            skipButton.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (!Input.anyKeyDown) return;
            
            if (_showButtonCooldownCoroutine != null)
            {
                StopCoroutine(_showButtonCooldownCoroutine);
            }
                
            _showButtonCooldownCoroutine = StartCoroutine(ShowButtonCooldown());
        }
        
        private IEnumerator ShowButtonCooldown()
        {
            skipButton.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            skipButton.gameObject.SetActive(false);
        }

        private void OnIntroFinished(VideoPlayer vp)
        {
            LoadMainAndUI();
        }

        private void SkipIntro()
        {
            LoadMainAndUI();
        }

        private void LoadMainAndUI()
        {
            SceneManager.LoadScene(LoadingManager.MAIN_SCENE);
            SceneManager.LoadSceneAsync(LoadingManager.UI_SCENE, LoadSceneMode.Additive);
        }
    }
}