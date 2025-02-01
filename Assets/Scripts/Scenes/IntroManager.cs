using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Scenes
{
    public class IntroManager : MonoBehaviour, ISceneReady
    {
        [SerializeField] private VideoPlayer introVideo;
        [SerializeField] private Button skipButton;

        public bool IsReady { get; private set; }
        public event Action OnSceneReady;  

        private bool isIntroFinished;

        private void Awake()
        {
            introVideo.loopPointReached += OnIntroFinished;
        }

        private async void Start()
        {
            skipButton.onClick.AddListener(SkipIntro);
            skipButton.gameObject.SetActive(false);

            await SetupVideoAndPlayAsync();

            IsReady = true;
            OnSceneReady?.Invoke();  

            await WaitForIntroToFinishAsync();

            LoadMainAndUI();
        }

        private void OnDestroy()
        {
            introVideo.loopPointReached -= OnIntroFinished;
        }

        private async UniTask SetupVideoAndPlayAsync()
        {
            var videoPath = Application.streamingAssetsPath + "/Intro.mp4";

            introVideo.url = videoPath;

            var videoPrepared = new UniTaskCompletionSource();

            introVideo.prepareCompleted += _ => videoPrepared.TrySetResult();
            introVideo.Prepare();

            await videoPrepared.Task;

            introVideo.Play();
        }

        private async UniTask WaitForIntroToFinishAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            skipButton.gameObject.SetActive(true);

            while (!isIntroFinished)
            {
                await UniTask.Yield();  
            }
        }

        private void OnIntroFinished(VideoPlayer vp)
        {
            isIntroFinished = true; 
        }

        private void SkipIntro()
        {
            isIntroFinished = true;  
        }

        private void LoadMainAndUI()
        {
            SceneManager.LoadScene(LoadingManager.MAIN_SCENE);
            SceneManager.LoadSceneAsync(LoadingManager.UI_SCENE, LoadSceneMode.Additive);
        }
    }
}
