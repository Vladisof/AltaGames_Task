using UnityEngine;
using Zenject;

namespace Level.UI
{
    public class LevelResultPanelSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Transform panelContainer;

        private LevelStage _levelStage;

        private void OnEnable()
        {
            _levelStage.StageChanged += LevelStageOnStageChanged;
        }

        private void OnDisable()
        {
            _levelStage.StageChanged -= LevelStageOnStageChanged;
        }


        [Inject]
        private void Inject(LevelStage levelStage)
        {
            _levelStage = levelStage;
        }

        private void LevelStageOnStageChanged(LevelStage.Stage stage)
        {
            switch (stage)
            {
                case LevelStage.Stage.WinResult:
                    ShowWinPanel();
                    break;
                case LevelStage.Stage.DefeatResult:
                    ShowLosePanel();
                    break;
            }
        }

        private void ShowWinPanel()
        {
            Instantiate(winPanel, panelContainer);
        }

        private void ShowLosePanel()
        {
            Instantiate(losePanel, panelContainer);
        }
    }
}