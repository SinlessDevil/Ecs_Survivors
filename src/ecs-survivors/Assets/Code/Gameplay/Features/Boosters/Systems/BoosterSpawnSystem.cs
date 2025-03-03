using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Boosters.Factroy;
using Entitas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Features.Boosters.Systems
{
    public class BoosterSpawnSystem : IExecuteSystem
    {
        private const float SpawnDistanceGap = 0.5f;
        private const float BoosterSpawnTimer = 10;
        
        private readonly ITimeService _timeService;
        private readonly IBoosterFactory _boosterFactory;
        private readonly ICameraProvider _cameraProvider;
        
        private readonly IGroup<GameEntity> _timers;
        private readonly IGroup<GameEntity> _heroes;
        
        public BoosterSpawnSystem(GameContext game, 
            ITimeService timeService, 
            IBoosterFactory boosterFactory,
            ICameraProvider cameraProvider)
        {
            _timeService = timeService;
            _boosterFactory = boosterFactory;
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
                    timer.ReplaceSpawnTimer(BoosterSpawnTimer);
                    _boosterFactory.CreateBooster(BoosterTypeId.MaxHPUpBooster, at: RandomSpawnPosition(hero.WorldPosition));
                   // _boosterFactory.CreateBooster(BoosterTypeId.InvulnerabilityBooster, at: RandomSpawnPosition(hero.WorldPosition)); //TODO : Create logic InvulnerabilityBooster
                }
            }
        }
        
        private Vector2 RandomSpawnPosition(Vector2 aroundPosition)
        {
            float xMin = aroundPosition.x - _cameraProvider.WorldScreenWidth / 2 + SpawnDistanceGap;
            float xMax = aroundPosition.x + _cameraProvider.WorldScreenWidth / 2 - SpawnDistanceGap;
            float yMin = aroundPosition.y - _cameraProvider.WorldScreenHeight / 2 + SpawnDistanceGap;
            float yMax = aroundPosition.y + _cameraProvider.WorldScreenHeight / 2 - SpawnDistanceGap;

            float randomX = Random.Range(xMin, xMax);
            float randomY = Random.Range(yMin, yMax);

            return new Vector2(randomX, randomY);
        }
    }
}
