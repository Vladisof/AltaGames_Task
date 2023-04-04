using UnityEngine;
using Zenject;

namespace Global
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen loadingScreen;
        [SerializeField] private SceneLoader sceneLoader;

        public override void InstallBindings()
        {
            Container.Bind<LoadingScreen>().FromComponentInNewPrefab(loadingScreen).AsSingle();
            Container.Bind<SceneLoader>().FromComponentInNewPrefab(sceneLoader).AsSingle();
        }
    }
}