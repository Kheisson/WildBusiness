using System;
using Cysharp.Threading.Tasks;
using Infra;
using Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes
{
    public class LoadingManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider progressBar;
        [SerializeField] private TextMeshProUGUI versionText;

        public const string MAIN_SCENE = "Main";
        public const string UI_SCENE = "Ui";
        private const string INTRO_MOVIE_SCENE = "IntroMovie";
        private const string LOADING_SCENE = "Loading";

        private SaveManager _saveManager;

        private async void Start()
        {
            versionText.text = $"Version: {Application.version}";

            _saveManager = ServiceLocator.GetService<SaveManager>();

            await LoadGameAsync();
        }

        private async UniTask LoadGameAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));

            var tutorialCompleted = _saveManager.Exists(SaveKeys.COMPLETED_TUTORIAL) &&
                                    _saveManager.Load<bool>(SaveKeys.COMPLETED_TUTORIAL);

            if (tutorialCompleted)
            {
                await LoadSceneWithDependenciesAsync(MAIN_SCENE, UI_SCENE);
            }
            else
            {
                await LoadSceneAsync(INTRO_MOVIE_SCENE);
            }
        }

        private async UniTask LoadSceneWithDependenciesAsync(string mainScene, string uiScene)
        {
            var mainSceneLoad = SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
            var uiSceneLoad = SceneManager.LoadSceneAsync(uiScene, LoadSceneMode.Additive);

            while (mainSceneLoad != null && uiSceneLoad != null && (!mainSceneLoad.isDone || !uiSceneLoad.isDone))
            {
                var combinedProgress = Mathf.Clamp01(
                    (mainSceneLoad.progress + uiSceneLoad.progress) / 2f
                );

                progressBar.value = combinedProgress;

                await UniTask.Yield();
            }

            await mainSceneLoad.ToUniTask();
            await uiSceneLoad.ToUniTask();

            await WaitForSceneReadyAsync(mainScene);

            await SceneManager.UnloadSceneAsync(LOADING_SCENE);
        }

        private async UniTask LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);

            while (operation is { isDone: false })
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;
                await UniTask.Yield();
            }

            await WaitForSceneReadyAsync(sceneName);
        }

        private async UniTask WaitForSceneReadyAsync(string sceneName)
        {
            var loadedScene = SceneManager.GetSceneByName(sceneName);

            if (!loadedScene.isLoaded)
            {
                LlamaLog.LogError($"Scene {sceneName} is not loaded yet.");
                return;
            }

            var rootObjects = loadedScene.GetRootGameObjects();
            foreach (var obj in rootObjects)
            {
                var sceneReadyComponent = obj.GetComponent<ISceneReady>();
                if (sceneReadyComponent != null)
                {
                    if (!sceneReadyComponent.IsReady)
                    {
                        var taskCompletionSource = new UniTaskCompletionSource();
                        sceneReadyComponent.OnSceneReady += () => taskCompletionSource.TrySetResult();

                        await taskCompletionSource.Task;  
                    }

                    LlamaLog.LogInfo($"Scene {sceneName} is ready.");
                    return;
                }
            }

            LlamaLog.LogWarning($"No ISceneReady implementation found in scene {sceneName}. Proceeding anyway.");
        }
    }
}
