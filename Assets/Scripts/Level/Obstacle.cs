using System;
using System.Collections;
using UnityEngine;

namespace Level
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Transform collisionPoint;

        [Header("Animations")] [SerializeField]
        private Animator animator;

        [SerializeField] [Range(0.5f, 2f)] private float corruptAnimationDuration;
        [SerializeField] private string corruptTriggerName;
        [SerializeField] private string destroyTriggerName;
        [SerializeField] private string destroyedStateName;

        private State _activeState = State.Healthy;

        public bool IsHealthy => _activeState == State.Healthy;
        public Transform CollisionPoint => collisionPoint;

        public static event Action<Obstacle> ObstacleDestroyed;

        public void Corrupt()
        {
            StartCoroutine(CorruptRoutine());
        }

        private IEnumerator CorruptRoutine()
        {
            _activeState = State.Corrupted;
            animator.SetTrigger(corruptTriggerName);
            yield return new WaitForSeconds(corruptAnimationDuration);

            animator.SetTrigger(destroyTriggerName);
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(destroyedStateName));

            ObstacleDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private enum State
        {
            Healthy,
            Corrupted
        }
    }
}