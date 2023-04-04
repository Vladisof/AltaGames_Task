using Core;
using Global;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Level.UI
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private SceneReference menuScene;

        private Button _menuButton;
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _menuButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(OpenMenu);
        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(OpenMenu);
        }

        [Inject]
        private void Inject(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OpenMenu()
        {
            _menuButton.interactable = false;
            _sceneLoader.LoadScene(menuScene.GetSceneName(), gameObject.scene.name);
        }
    }
}