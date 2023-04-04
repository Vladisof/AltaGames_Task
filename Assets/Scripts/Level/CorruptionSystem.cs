using Level.CharacterShoot;
using UnityEngine;
using Zenject;

namespace Level
{
    public class CorruptionSystem : MonoBehaviour
    {
        private ObstacleContainer _obstacleContainer;

        private void OnEnable()
        {
            Shell.ShellExplode += OnShellExplode;
        }

        private void OnDisable()
        {
            Shell.ShellExplode -= OnShellExplode;
        }

        [Inject]
        private void Inject(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        private void OnShellExplode(Shell.ExplosionData explosionData)
        {
            var findCenterPosition = new Vector2(explosionData.Position.x, explosionData.Position.z);
            var corruptedObstacles = _obstacleContainer.GetObstaclesInArea(findCenterPosition, explosionData.Radius);
            foreach (var corruptedObstacle in corruptedObstacles) corruptedObstacle.Corrupt();
        }
    }
}