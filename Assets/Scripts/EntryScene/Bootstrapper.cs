using Core;
using Global;
using UnityEngine;
using Zenject;

namespace EntryScene
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private SceneReference initialScene;

        private SceneLoader _sceneLoader;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Initialize();
        }

        [Inject]
        private void Inject(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Initialize()
        {
            var initialSceneName = initialScene.GetSceneName();
            _sceneLoader.LoadSceneInstantly(initialSceneName);
        }
    }
}