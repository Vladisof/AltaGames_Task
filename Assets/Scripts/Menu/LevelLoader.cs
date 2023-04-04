using System;
using Core;
using Global;
using LevelData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private LevelSettings levelSettings;
        [SerializeField] private SceneReference levelScene;

        [SerializeField] private Button levelLoadButton;

        private SceneLoader _sceneLoader;

        private void OnEnable()
        {
            OnLevelLoadingStarted += LevelLoadingStarted;
            levelLoadButton.onClick.AddListener(LoadLevel);
        }

        private void OnDisable()
        {
            OnLevelLoadingStarted -= LevelLoadingStarted;
            levelLoadButton.onClick.RemoveListener(LoadLevel);
        }

        [Inject]
        private void Inject(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public static event Action<LevelSettings> LevelLoaded;
        private static event Action OnLevelLoadingStarted;

        private void LoadLevel()
        {
            OnLevelLoadingStarted?.Invoke();
            _sceneLoader.LoadScene(levelScene.GetSceneName(), gameObject.scene.name, () => LevelLoaded?.Invoke(levelSettings));
        }


        private void LevelLoadingStarted()
        {
            levelLoadButton.interactable = false;
        }
    }
}