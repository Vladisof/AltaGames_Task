using System.IO;
using Core.Attribute;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "SceneReference.asset", menuName = "Custom/Scenes/Scene Reference")]
    public class SceneReference : ScriptableObject
    {
        [SerializeField] [ReadOnly] private string scenePath;

        public string GetSceneName()
        {
            var result = GetScenePath();
            if (!string.IsNullOrEmpty(result)) result = Path.GetFileNameWithoutExtension(result);

            return result;
        }


        public string GetScenePath()
        {
            return scenePath;
        }
    }
}