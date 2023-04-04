using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Global
{
    public class SceneLoader : MonoBehaviour
    {
        private const float SceneLoadingCompleteProgress = 0.87f;
        private AsyncOperation _levelSceneLoading;
        private LoadingScreen _loadingScreen;

        [Inject]
        private void Inject(LoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }

        public void LoadSceneInstantly(string sceneName)
        {
            if (SceneManager.GetActiveScene().name != sceneName) SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(string sceneName, string previousScene, Action sceneLoadedCallback = null)
        {
            if (sceneName != previousScene && SceneManager.GetActiveScene().name != sceneName) StartCoroutine(LoadSceneRoutine(sceneName, sceneLoadedCallback, previousScene));
        }

        private IEnumerator LoadSceneRoutine(string sceneName, Action sceneLoadedCallback = null, string previousScene = "")
        {
            _levelSceneLoading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _levelSceneLoading.allowSceneActivation = false;

            yield return _loadingScreen.ShowLoadingScreenRoutine();
            yield return new WaitWhile(() => _levelSceneLoading.progress < SceneLoadingCompleteProgress);

            _levelSceneLoading.allowSceneActivation = true;
            var loadingScene = SceneManager.GetSceneByName(sceneName);

            yield return new WaitUntil(() => loadingScene.isLoaded);
            yield return _loadingScreen.HideLoadingScreenProgressViewRoutine();

            sceneLoadedCallback?.Invoke();

            if (previousScene != string.Empty)
                yield return UnloadMenuScene(previousScene);
            else
                yield return _loadingScreen.HideLoadingScreenRoutine();
        }

        private IEnumerator UnloadMenuScene(string previousSceneName)
        {
            var previousScene = SceneManager.GetSceneByName(previousSceneName);
            var menuSceneUnloading = SceneManager.UnloadSceneAsync(previousScene);
            yield return new WaitUntil(() => menuSceneUnloading.isDone);
            yield return _loadingScreen.HideLoadingScreenRoutine();
        }
    }
}