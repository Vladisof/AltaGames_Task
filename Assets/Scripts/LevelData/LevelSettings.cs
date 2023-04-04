using UnityEngine;

namespace LevelData
{
    [CreateAssetMenu(fileName = "Level Settings", menuName = "Custom/Level/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [SerializeField] private GameObject obstaclesPrefab;
        [SerializeField] [Range(0f, 100f)] private float minCharacterSize;
        [SerializeField] private ShellExplosionRadiusSettings shellExplosionRadiusSettings;

        public GameObject ObstaclesPrefab => obstaclesPrefab;
        public float MinCharacterSize => minCharacterSize;
        public ShellExplosionRadiusSettings ShellExplosionRadiusSettings => shellExplosionRadiusSettings;
    }
}