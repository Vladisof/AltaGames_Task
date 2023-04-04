using System;
using UnityEngine;

namespace Level
{
    public class LevelStage : MonoBehaviour
    {
        public enum Stage
        {
            Shooting,
            VictoriousAdvance,
            WinResult,
            Defeat,
            DefeatResult
        }

        public Stage CurrentStage { get; private set; }

        public event Action<Stage> StageChanged;

        public void SetStage(Stage newStage)
        {
            CurrentStage = newStage;
            StageChanged?.Invoke(CurrentStage);
        }
    }
}