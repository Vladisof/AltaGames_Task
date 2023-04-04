using UnityEditor;

namespace Core.Editor
{
    [CustomEditor(typeof(SceneReference), true)]
    public class SceneReferenceEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var picker = target as SceneReference;
            if (picker != null)
            {
                var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.GetScenePath());

                serializedObject.Update();

                EditorGUI.BeginChangeCheck();
                var newScene = EditorGUILayout.ObjectField("Scene: ", oldScene, typeof(SceneAsset), false) as SceneAsset;

                if (EditorGUI.EndChangeCheck())
                {
                    var newPath = AssetDatabase.GetAssetPath(newScene);
                    var scenePathProperty = serializedObject.FindProperty("scenePath");
                    scenePathProperty.stringValue = newPath;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}