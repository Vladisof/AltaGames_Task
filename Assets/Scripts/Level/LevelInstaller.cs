using Level.CharacterShoot;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelStage levelStage;
        [SerializeField] private LevelSettingsInitializer levelSettingsInitializer;
        [SerializeField] private ShootSystem shootSystem;
        [SerializeField] private Character character;
        [SerializeField] private ObstacleContainer obstacleContainer;
        [SerializeField] private CorruptionSystem corruptionSystem;
        [SerializeField] private Door door;
        [SerializeField] private MoveResultChecker moveResultChecker;

        public override void InstallBindings()
        {
            Container.Bind<LevelSettingsInitializer>().FromInstance(levelSettingsInitializer);
            Container.Bind<LevelStage>().FromInstance(levelStage);
            Container.Bind<ShootSystem>().FromInstance(shootSystem);
            Container.Bind<Character>().FromInstance(character);
            Container.Bind<ObstacleContainer>().FromInstance(obstacleContainer);
            Container.Bind<CorruptionSystem>().FromInstance(corruptionSystem);
            Container.Bind<Door>().FromInstance(door);
            Container.Bind<MoveResultChecker>().FromInstance(moveResultChecker);
        }
    }
}