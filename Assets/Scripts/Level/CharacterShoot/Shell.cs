using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Level.CharacterShoot
{
    public class Shell : MonoBehaviour
    {
        [SerializeField] private Transform shellModel;
        [SerializeField] private AnimationCurve launchSpeedByCharge;

        [Header("Particles")] [SerializeField] private ParticleSystem shellParticles;

        [SerializeField] private GameObject explosionParticlesPrefab;
        [SerializeField] private float explosionParticlesAnimationDuration;
       
        private Door _door;
        private LevelSettingsInitializer _levelSettingsInitializer;
        private ObstacleContainer _obstacleContainer;

        private float _shellCharge;

        public static event Action<ExplosionData> ShellExplode;

        [Inject]
        private void Inject(LevelSettingsInitializer levelSettingsInitializer, ObstacleContainer obstacleContainer, Door door)
        {
            _levelSettingsInitializer = levelSettingsInitializer;
            _obstacleContainer = obstacleContainer;
            _door = door;
        }

        public void Launch()
        {
            var launchDuration = launchSpeedByCharge.Evaluate(_shellCharge);
            var collisionObstacle = _obstacleContainer.FindHealthyObstacleCollision(transform.position);
            var collisionObstaclePosition = collisionObstacle != null ? collisionObstacle.position : _door.FinalPoint;
            PlayLaunchAnimation(launchDuration, collisionObstaclePosition);
        }

        public void Boost(float boostValue)
        {
            _shellCharge += boostValue;
            shellModel.localScale = Vector3.one * _shellCharge;
        }

        private void Explode()
        {
            transform.DOKill();
            var explodeArea = _levelSettingsInitializer.GetExplosionAreaRadius(_shellCharge);

            SpawnParticlesPrefab();

            ShellExplode?.Invoke(new ExplosionData(transform.position, explodeArea));
            transform.localScale = Vector3.zero;
            Destroy(gameObject, shellParticles.main.startLifetime.constantMax);
        }

        private void SpawnParticlesPrefab()
        {
            var explosionParticles = Instantiate(explosionParticlesPrefab, transform.position, Quaternion.identity);
            
            Destroy(explosionParticles, explosionParticlesAnimationDuration);
        }

        private void PlayLaunchAnimation(float launchDuration, Vector3 collisionObstaclePosition)
        {
            transform.DOLocalMoveZ(collisionObstaclePosition.z, launchDuration).SetEase(Ease.OutCubic).OnComplete(Explode);
            transform.DOLocalMoveX(collisionObstaclePosition.x, launchDuration);
            transform.DOLocalMoveY(2f, launchDuration * 0.3f).SetEase(Ease.OutCubic).OnComplete(
                () => { transform.DOLocalMoveY(0f, launchDuration * 0.7f).SetEase(Ease.InCubic); }
            );
        }

        public struct ExplosionData
        {
            public readonly Vector3 Position;
            public readonly float Radius;

            public ExplosionData(Vector3 position, float radius)
            {
                Position = position;
                Radius = radius;
            }
        }
    }
}