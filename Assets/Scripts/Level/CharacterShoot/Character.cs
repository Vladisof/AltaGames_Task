using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Level.CharacterShoot
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private ShellCreator shellCreator;
        [SerializeField] private GameObject sphereModel;
        [SerializeField] private AnimationCurve chargeTransferSpeedByTime;
        [SerializeField] private Gradient sphereColorByCharge;

        [Header("Particles")] [SerializeField] private GameObject loseParticlesPrefab;

        [SerializeField] private float loseParticlesAnimationDuration;
        [SerializeField] private Vector3 loseParticlesSpawnPosition;

        [Header("Animations")] [SerializeField] [Range(0f, 4f)]
        private float loseModelHideDuration = 0.2f;

        private float _charge = 1f;
        private Coroutine _chargeRoutine;

        private float _chargingTime;
        private LevelSettingsInitializer _levelSettingsInitializer;
        private LevelStage _levelStage;

        private Shell _shell;

        private ShootSystem _shootSystem;
        private MeshRenderer _sphereMesh;


        public float ModelWidth => sphereModel.transform.localScale.x;

        private void Start()
        {
            _sphereMesh = sphereModel.GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            _shootSystem.ActiveStateChanged += OnActiveStateChanged;
        }

        private void OnDisable()
        {
            _shootSystem.ActiveStateChanged -= OnActiveStateChanged;
        }

        public event Action CriticalChargeLevelReached;
        public event Action<Vector3> SphereSizeChanged;

        [Inject]
        private void Inject(ShootSystem shootSystem, LevelSettingsInitializer levelSettingsInitializer, LevelStage levelStage)
        {
            _shootSystem = shootSystem;
            _levelSettingsInitializer = levelSettingsInitializer;
            _levelStage = levelStage;
        }

        private void OnActiveStateChanged(ShootSystem.ActiveShootState state)
        {
            if (state == ShootSystem.ActiveShootState.Charge)
            {
                ActivateChargeMode();
            }
            else if (_shell != null)
            {
                StopCoroutine(_chargeRoutine);
                _shell.Launch();
            }
        }

        private void ActivateChargeMode()
        {
            _chargingTime = 0;
            _shell = shellCreator.CreateShell();
            _chargeRoutine = StartCoroutine(ChargeRoutine());
        }

        private IEnumerator ChargeRoutine()
        {
            while (_levelStage.CurrentStage == LevelStage.Stage.Shooting)
            {
                TransferCharge();
                yield return null;
                _chargingTime += Time.deltaTime;
            }
        }

        private void TransferCharge()
        {
            var chargeValue = chargeTransferSpeedByTime.Evaluate(_chargingTime);
            ReduceCharacterCharge(chargeValue);
            _shell.Boost(chargeValue);
        }

        private void ReduceCharacterCharge(float chargeValue)
        {
            _charge -= chargeValue;

            sphereModel.transform.localScale = Vector3.one * _charge;

            var currentColorValue = (1f - _levelSettingsInitializer.MinCharacterSize) * _charge + _levelSettingsInitializer.MinCharacterSize;
            _sphereMesh.material.color = sphereColorByCharge.Evaluate(currentColorValue);

            SphereSizeChanged?.Invoke(sphereModel.transform.localScale);

            if (!(_charge <= _levelSettingsInitializer.MinCharacterSize)) return;
            CriticalChargeLevelReached?.Invoke();
            _levelStage.SetStage(LevelStage.Stage.DefeatResult);
            transform.DOScale(Vector3.zero, loseModelHideDuration).SetEase(Ease.OutCirc);
            SpawnLoseParticlesPrefab();
        }

        private void SpawnLoseParticlesPrefab()
        {
            var explosionParticles = Instantiate(loseParticlesPrefab, transform.position + loseParticlesSpawnPosition, Quaternion.identity);
            Destroy(explosionParticles, loseParticlesAnimationDuration);
        }
    }
}