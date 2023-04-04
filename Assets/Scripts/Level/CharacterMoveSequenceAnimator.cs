using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Level
{
    public class CharacterMoveSequenceAnimator : MonoBehaviour
    {
        [SerializeField] private Button moveButton;
        [SerializeField] private Animation characterAnimation;

        [Header("Particles")] [SerializeField] private GameObject hitParticlesPrefab;

        [SerializeField] private float explosionParticlesAnimationDuration;
        [SerializeField] private Vector3 explosionParticlesSpawnPosition;

        [Header("Animation Settings")] [SerializeField] [Range(0f, 4f)]
        private float winMoveDuration = 2.5f;

        [SerializeField] [Range(0f, 4f)] private float loseMoveDuration = 1f;
        [SerializeField] [Range(0f, 4f)] private float loseModelHideDuration = 0.2f;

        private Door _door;
        private LevelStage _levelStage;
        private MoveResultChecker _moveResultChecker;
        private ObstacleContainer _obstacleContainer;


        private void OnEnable()
        {
            moveButton.onClick.AddListener(Move);
        }

        private void OnDisable()
        {
            moveButton.onClick.RemoveListener(Move);
        }

        [Inject]
        private void Inject(Door door, LevelStage levelStage, MoveResultChecker moveResultChecker, ObstacleContainer obstacleContainer)
        {
            _door = door;
            _levelStage = levelStage;
            _moveResultChecker = moveResultChecker;
            _obstacleContainer = obstacleContainer;
        }

        private void Move()
        {
            moveButton.interactable = false;
            if (_moveResultChecker.IsPathClear())
            {
                _levelStage.SetStage(LevelStage.Stage.VictoriousAdvance);
                transform.DOMoveZ(_door.JumpPoint.z, winMoveDuration).OnComplete(() => StartCoroutine(PlayJumpInDoorAnimation()));
            }
            else
            {
                PlayLoseHitAnimation(_obstacleContainer.FindHealthyObstacleCollision(transform.position).position);
            }
        }

        private IEnumerator PlayJumpInDoorAnimation()
        {
            characterAnimation.Play();
            yield return new WaitWhile(() => characterAnimation.isPlaying);
            _levelStage.SetStage(LevelStage.Stage.WinResult);
        }

        private void PlayLoseHitAnimation(Vector3 obstaclePosition)
        {
            _levelStage.SetStage(LevelStage.Stage.Defeat);
            transform.DOMoveZ(obstaclePosition.z - transform.localScale.z / 2f, loseMoveDuration).SetEase(Ease.InBack).OnComplete(HideCharacterModel);
        }

        private void HideCharacterModel()
        {
            SpawnHitParticlesPrefab();
            transform.DOScale(Vector3.zero, loseModelHideDuration).SetEase(Ease.OutCirc);
            _levelStage.SetStage(LevelStage.Stage.DefeatResult);
        }

        private void SpawnHitParticlesPrefab()
        {
            var explosionParticles = Instantiate(hitParticlesPrefab, transform.position + explosionParticlesSpawnPosition, Quaternion.identity);
            Destroy(explosionParticles, explosionParticlesAnimationDuration);
        }
    }
}