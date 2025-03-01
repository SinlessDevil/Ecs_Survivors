using Code.Common.Extensions;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Enemies.Factory;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemySpawnSystem : IExecuteSystem
    {
        private const float SpawnDistanceGap = 0.5f;
        private const float EnemySpawnTimer = 1;
        
        private readonly ITimeService _timeService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ICameraProvider _cameraProvider;
        
        private readonly IGroup<GameEntity> _timers;
        private readonly IGroup<GameEntity> _heroes;
        
        public EnemySpawnSystem(GameContext game, 
            ITimeService timeService, 
            IEnemyFactory enemyFactory,
            ICameraProvider cameraProvider)
        {
            _timeService = timeService;
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;
            
            _timers = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.SpawnTimer));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            foreach (GameEntity timer in _timers)
            {
                timer.ReplaceSpawnTimer(timer.SpawnTimer - _timeService.DeltaTime);
                if (timer.SpawnTimer <= 0)
                {
                    timer.ReplaceSpawnTimer(EnemySpawnTimer);
                    _enemyFactory.CreateEnemy(EnemyTypeId.Goblin,at: RandomSpawnPosition(hero.WorldPosition), level: 1);
                }
            }
        }
        
        private Vector2 RandomSpawnPosition(Vector2 aroundPosition)
        {
            bool startWithHorizontal = Random.Range(0, 2) == 0;

            return startWithHorizontal
                ? HorizontalSpawnPosition(aroundPosition)
                : VerticalSpawnPosition(aroundPosition);
        }

        private Vector2 HorizontalSpawnPosition(Vector2 aroundPosition)
        {
            Vector2[] horizontalDirections = { Vector2.left, Vector2.right };
            Vector2 primaryDirection = horizontalDirections.PickRandom();

            float horizontalOffsetDistance = _cameraProvider.WorldScreenWidth / 2 + SpawnDistanceGap;
            float verticalRandomOffset = Random.Range(-_cameraProvider.WorldScreenHeight / 2, _cameraProvider.WorldScreenHeight / 2);

            return aroundPosition + primaryDirection * horizontalOffsetDistance + Vector2.up * verticalRandomOffset;
        }

        private Vector2 VerticalSpawnPosition(Vector2 aroundPosition)
        {
            Vector2[] verticalDirections = { Vector2.up, Vector2.down };
            Vector2 primaryDirection = verticalDirections.PickRandom();

            float verticalOffsetDistance = _cameraProvider.WorldScreenHeight / 2 + SpawnDistanceGap;
            float horizontalRandomOffset = Random.Range(-_cameraProvider.WorldScreenWidth / 2, _cameraProvider.WorldScreenWidth / 2);

            return aroundPosition + primaryDirection * verticalOffsetDistance + Vector2.right * horizontalRandomOffset;
        }
    }
}