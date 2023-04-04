using UnityEngine;

namespace Debug
{
    public class FpsDisplay : MonoBehaviour
    {
        [SerializeField] private bool showVersion = true;

        [Header("Style")] [SerializeField] private Font font;

        [SerializeField] private Color fontColor = new(1f, 1f, 0.49f);
        [SerializeField] [Range(5, 60)] private int fontSize = 40;

        private float _deltaTime;
        private float _topSafeAreaOffset;

        private void Start()
        {
            UpdateSafeAreaOffset();
        }

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
#if DEVELOPMENT_BUILD
            var style = new GUIStyle();
            var position = new Vector2(80, 5 + _topSafeAreaOffset);
            var size = new Vector2(Screen.width, Screen.height);
            var rect = new Rect(position, size);
            
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = fontSize;
            style.normal.textColor = fontColor;
            style.font = font;
            
            var fps = 1.0f / _deltaTime;
            var textVersion = $"  v.{Application.version}";
            var text = $"{fps:0.} FPS ";
            
            GUI.Label(rect, showVersion ? text + textVersion : text, style);
#endif
        }

        private void UpdateSafeAreaOffset()
        {
            _topSafeAreaOffset = Screen.currentResolution.height - (Screen.safeArea.y + Screen.safeArea.height);
        }
    }
}