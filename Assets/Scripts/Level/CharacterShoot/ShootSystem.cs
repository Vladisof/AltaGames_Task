using System;
using UnityEngine;
using Zenject;

namespace Level.CharacterShoot
{
    public class ShootSystem : MonoBehaviour
    {
        public enum ActiveShootState
        {
            Idle,
            Charge
        }

        [SerializeField] private ShootPointerArea shootPointerArea;

        private ActiveShootState _activeShootState;

        private LevelStage _levelStage;

        private void OnEnable()
        {
            shootPointerArea.OnTapStarted += ShootPointerAreaOnOnTapStarted;
            shootPointerArea.OnTapFinished += ShootPointerAreaOnOnTapFinished;
        }

        private void OnDisable()
        {
            shootPointerArea.OnTapStarted -= ShootPointerAreaOnOnTapStarted;
            shootPointerArea.OnTapFinished -= ShootPointerAreaOnOnTapFinished;
        }

        [Inject]
        private void Inject(LevelStage levelStage)
        {
            _levelStage = levelStage;
        }

        public event Action<ActiveShootState> ActiveStateChanged;

        private void CompleteCharging()
        {
            _activeShootState = ActiveShootState.Idle;
            ActiveStateChanged?.Invoke(_activeShootState);
        }

        private void StartCharging()
        {
            _activeShootState = ActiveShootState.Charge;
            ActiveStateChanged?.Invoke(_activeShootState);
        }

        private void ShootPointerAreaOnOnTapStarted()
        {
            if (_activeShootState == ActiveShootState.Idle && _levelStage.CurrentStage == LevelStage.Stage.Shooting) StartCharging();
        }

        private void ShootPointerAreaOnOnTapFinished()
        {
            if (_activeShootState == ActiveShootState.Charge) CompleteCharging();
        }
    }
}