using System.Collections;
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

        private void Start()
        {
            versionText.text = $"Version: {Application.version}";

            _saveManager = Infra.ServiceLocator.GetService<SaveManager>();

            StartCoroutine(LoadGame());
        }

        private IEnumerator LoadGame()
        {
            yield return new WaitForSeconds(0.5f);

            var tutorialCompleted = _saveManager.Exists(SaveKeys.COMPLETED_TUTORIAL) &&
                                    _saveManager.Load<bool>(SaveKeys.COMPLETED_TUTORIAL);

            if (tutorialCompleted)
            {
                yield return StartCoroutine(LoadSceneWithDependencies(MAIN_SCENE, UI_SCENE));
            }
            else
            {
                yield return StartCoroutine(LoadSceneAsync(INTRO_MOVIE_SCENE));
            }
        }

        private IEnumerator LoadSceneWithDependencies(string mainScene, string uiScene)
        {
            var mainSceneLoad = SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
            var uiSceneLoad = SceneManager.LoadSceneAsync(uiScene, LoadSceneMode.Additive);

            if (mainSceneLoad != null && uiSceneLoad != null)
            {
                mainSceneLoad.allowSceneActivation = false;
                uiSceneLoad.allowSceneActivation = false;

                while (mainSceneLoad.progress < 0.9f || uiSceneLoad.progress < 0.9f)
                {
                    var combinedProgress = Mathf.Clamp01((mainSceneLoad.progress + uiSceneLoad.progress) / 1.8f);
                    progressBar.value = combinedProgress;

                    LlamaLog.LogInfo($"Main Scene Progress: {mainSceneLoad.progress}, UI Scene Progress: {uiSceneLoad.progress}");

                    yield return null; 
                }

                LlamaLog.LogInfo("Both scenes are ready to be activated.");

                mainSceneLoad.allowSceneActivation = true;
                uiSceneLoad.allowSceneActivation = true;

                while (!mainSceneLoad.isDone || !uiSceneLoad.isDone)
                {
                    yield return null;
                }

                LlamaLog.LogInfo("Both scenes are successfully activated.");
            }
            else
            {
                LlamaLog.LogError("Scene loading failed.");
            }

            yield return new WaitForEndOfFrame();

            SceneManager.UnloadSceneAsync(LOADING_SCENE);
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress; 
                yield return null; 
            }
        }
    }
}
