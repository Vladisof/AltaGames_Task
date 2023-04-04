using System.Collections;
using UnityEngine;

namespace Global
{
    public class LoadingScreen : MonoBehaviour
    {
        private static readonly int Loaded = Animator.StringToHash("Loaded");
        private static readonly int Hide = Animator.StringToHash("Hide");

        [SerializeField] private GameObject loadingScreenPrefab;
        [SerializeField] private string showAnimatorStateName;
        [SerializeField] private string loadedAnimatorStateName;
        [SerializeField] private string hideAnimatorStateName;
        private bool _isLoadingScreenSpawned;

        private Animator _loadingScreen;


        public IEnumerator ShowLoadingScreenRoutine()
        {
            if (_isLoadingScreenSpawned) yield break;
            _loadingScreen = Instantiate(loadingScreenPrefab).GetComponent<Animator>();
            DontDestroyOnLoad(_loadingScreen.gameObject);

            _isLoadingScreenSpawned = true;
            yield return new WaitUntil(() => _loadingScreen.GetCurrentAnimatorStateInfo(0).IsName(showAnimatorStateName));
        }

        public IEnumerator HideLoadingScreenProgressViewRoutine()
        {
            if (!_isLoadingScreenSpawned) yield break;
            _loadingScreen.SetTrigger(Loaded);
            yield return new WaitUntil(() => _loadingScreen.GetCurrentAnimatorStateInfo(0).IsName(loadedAnimatorStateName));
        }


        public IEnumerator HideLoadingScreenRoutine()
        {
            if (!_isLoadingScreenSpawned) yield break;
            _loadingScreen.SetTrigger(Hide);
            yield return new WaitUntil(() => _loadingScreen.GetCurrentAnimatorStateInfo(0).IsName(hideAnimatorStateName));

            DestroyLoadingScreen();
        }

        private void DestroyLoadingScreen()
        {
            Destroy(_loadingScreen.gameObject);
            _isLoadingScreenSpawned = false;
        }
    }
}