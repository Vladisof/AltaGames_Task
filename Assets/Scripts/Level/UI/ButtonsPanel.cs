using UnityEngine;
using Zenject;

namespace Level.UI
{
    public class ButtonsPanel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string panelActivityAnimatorParamName;

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
        private void Inject(LevelStage levelStage)
        {
            _levelStage = levelStage;
        }

        private void OnStageChanged(LevelStage.Stage stage)
        {
            switch (stage)
            {
                case LevelStage.Stage.Shooting:
                    animator.SetBool(panelActivityAnimatorParamName, true);
                    break;
                case LevelStage.Stage.VictoriousAdvance:
                    animator.SetBool(panelActivityAnimatorParamName, false);
                    break;
                case LevelStage.Stage.Defeat:
                    animator.SetBool(panelActivityAnimatorParamName, false);
                    break;
            }
        }
    }
}