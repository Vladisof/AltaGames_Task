using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        private CanvasScaler _canvasScaler;
        private RectTransform _rectTransform;
        private Rect _safeArea;


        protected void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasScaler = GetComponentInParent<CanvasScaler>();
        }


#if UNITY_EDITOR
        protected void Update()
        {
            UpdateSafeArea();
        }
#endif


        protected void OnEnable()
        {
            Canvas.willRenderCanvases += UpdateSafeArea;
        }


        protected void OnDisable()
        {
            Canvas.willRenderCanvases -= UpdateSafeArea;
        }


        private void UpdateSafeArea()
        {
            var safeArea = Screen.safeArea;
            if (safeArea == _safeArea) return;

            _safeArea = safeArea;

            var bottomPixels = Screen.safeArea.y;
            var topPixel = Screen.currentResolution.height - (Screen.safeArea.y + Screen.safeArea.height);

            var bottomRatio = bottomPixels / Screen.currentResolution.height;
            var topRatio = topPixel / Screen.currentResolution.height;

            var referenceResolution = _canvasScaler.referenceResolution;
            var bottomUnits = referenceResolution.y * bottomRatio;
            var topUnits = referenceResolution.y * topRatio;

            _rectTransform.offsetMin = new Vector2(_rectTransform.offsetMin.x, bottomUnits);
            _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, -topUnits);
        }
    }
}