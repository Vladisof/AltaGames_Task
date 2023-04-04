using System.Collections;
using TMPro;
using UnityEngine;

namespace Global
{
    public class LoadingScreenProgressAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI animatedProgressText;
        [SerializeField] [Range(0.1f, 2f)] private float animationFrameDelay;
        [SerializeField] private char animatedProgressSymbol;
        [SerializeField] [Range(3, 12)] private int animatedProgressSymbolsCount;

        private Coroutine _animationRoutine;

        private void Start()
        {
            animatedProgressText.text = string.Empty;
            _animationRoutine = StartCoroutine(PlayAnimationRoutine());
        }

        private void OnDestroy()
        {
            StopCoroutine(_animationRoutine);
        }

        private IEnumerator PlayAnimationRoutine()
        {
            while (true)
            {
                if (animatedProgressText.text.Length >= animatedProgressSymbolsCount) animatedProgressText.text = string.Empty;
                animatedProgressText.text += animatedProgressSymbol;
                yield return new WaitForSeconds(animationFrameDelay);
            }
        }
    }
}