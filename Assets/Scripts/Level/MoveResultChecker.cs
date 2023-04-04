using UnityEngine;
using Zenject;

namespace Level
{
    public class MoveResultChecker : MonoBehaviour
    {
        private ObstacleContainer _obstacleContainer;

        [Inject]
        private void Inject(ObstacleContainer obstacleContainer)
        {
            _obstacleContainer = obstacleContainer;
        }

        public bool IsPathClear()
        {
            return _obstacleContainer.FindHealthyObstacleCollision(transform.position) == null;
        }
    }
}