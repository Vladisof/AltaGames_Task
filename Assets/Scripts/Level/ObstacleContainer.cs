using System.Collections.Generic;
using System.Linq;
using Level.CharacterShoot;
using UnityEngine;
using Zenject;

namespace Level
{
    public class ObstacleContainer : MonoBehaviour
    {
        private Character _character;
        private Door _door;
        private LevelSettingsInitializer _levelSettingsInitializer;

        private List<Obstacle> _obstacles = new();

        private void Awake()
        {
            _levelSettingsInitializer.SettingsLoaded += InitializeLevelObstacles;
        }

        private void OnEnable()
        {
            Obstacle.ObstacleDestroyed += OnObstacleDestroyed;
        }

        private void OnDisable()
        {
            Obstacle.ObstacleDestroyed -= OnObstacleDestroyed;
        }

        [Inject]
        private void Inject(Character character, LevelSettingsInitializer levelSettingsInitializer, Door door)
        {
            _levelSettingsInitializer = levelSettingsInitializer;
            _character = character;
            _door = door;
        }

        private void OnObstacleDestroyed(Obstacle obstacle)
        {
            _obstacles.Remove(obstacle);
        }

        private void InitializeLevelObstacles()
        {
            _levelSettingsInitializer.SettingsLoaded -= InitializeLevelObstacles;
            var obstaclesTransform = Instantiate(_levelSettingsInitializer.ObstaclesPrefab, transform).GetComponent<Transform>();

            for (var i = 0; i < obstaclesTransform.childCount; i++) _obstacles.Add(obstaclesTransform.GetChild(i).GetComponent<Obstacle>());

            _obstacles = _obstacles.OrderBy(obstacle => obstacle.transform.position.z).ToList();
        }

        public Transform FindHealthyObstacleCollision(Vector3 launchPosition)
        {
            Transform hitObstacle = null;
            foreach (var obstacle in _obstacles)
            {
                var pathAreaBorders = new Vector2(launchPosition.x - _character.ModelWidth / 2f, launchPosition.x + _character.ModelWidth / 2f);
                var collisionPointPosition = obstacle.CollisionPoint.position;
                var isInBorders = collisionPointPosition.x >= pathAreaBorders.x && collisionPointPosition.x <= pathAreaBorders.y;

                if (!obstacle.IsHealthy || !isInBorders || !(_door.FinalPoint.z > obstacle.CollisionPoint.position.z)) continue;
                hitObstacle = obstacle.CollisionPoint;
                break;
            }

            return hitObstacle;
        }

        public List<Obstacle> GetObstaclesInArea(Vector2 findCenterPosition, float radius)
        {
            List<Obstacle> hitObstacle = new();
            foreach (var obstacle in _obstacles)
            {
                var collisionObstaclePoint = obstacle.CollisionPoint.position;
                var obstaclePosition = new Vector2(collisionObstaclePoint.x, collisionObstaclePoint.z);
                if (Vector2.Distance(obstaclePosition, findCenterPosition) <= radius) hitObstacle.Add(obstacle);
            }

            return hitObstacle;
        }
    }
}