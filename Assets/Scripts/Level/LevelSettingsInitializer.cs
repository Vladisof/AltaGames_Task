using System;
using LevelData;
using Menu;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelSettingsInitializer : MonoBehaviour
    {
        private LevelStage _levelStage;
        private LevelSettings _settings;
        public float MinCharacterSize => _settings.MinCharacterSize / 100f;
        public GameObject ObstaclesPrefab => _settings.ObstaclesPrefab;

        private void Awake()
        {
            LevelLoader.LevelLoaded += InitializeLevelSettings;
        }

        public float GetExplosionAreaRadius(float shellCharge)
        {
            return _settings.ShellExplosionRadiusSettings.GetExplosionRadius(shellCharge);
        }


        [Inject]
        private void Inject(LevelStage levelStage)
        {
            _levelStage = levelStage;
        }

        public event Action SettingsLoaded;

        private void InitializeLevelSettings(LevelSettings settings)
        {
            _settings = settings;
            SettingsLoaded?.Invoke();
            _levelStage.SetStage(LevelStage.Stage.Shooting);
            LevelLoader.LevelLoaded -= InitializeLevelSettings;
        }
    }
}