using System.Collections;
using Level.CharacterShoot;
using UnityEngine;
using Zenject;

namespace Level
{
    public class DoorOpener : MonoBehaviour
    {
        [SerializeField] [Range(0f, 30f)] private float doorOpenTriggerRange;
        private Character _character;

        private Door _door;
        private LevelStage _levelStage;

        private void OnEnable()
        {
            _levelStage.StageChanged += OnStageChanged;
        }

        private void OnDisable()
        {
            _levelStage.StageChanged -= OnStageChanged;
        }

        [Inject]
        private void Inject(Door door, LevelStage levelStage, Character character)
        {
            _door = door;
            _levelStage = levelStage;
            _character = character;
        }

        private void OnStageChanged(LevelStage.Stage stage)
        {
            if (stage == LevelStage.Stage.VictoriousAdvance) StartCoroutine(OpenDoorRoutine());
        }

        private IEnumerator OpenDoorRoutine()
        {
            yield return new WaitUntil(() => Vector3.Distance(_door.FinalPoint, _character.transform.position) <= doorOpenTriggerRange);
            _door.Open();
        }
    }
}