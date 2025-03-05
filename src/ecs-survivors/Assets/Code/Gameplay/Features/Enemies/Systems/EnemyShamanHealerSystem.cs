using System.Linq;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyShamanHealerSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly ITimeService _timeService;

        private readonly IGroup<GameEntity> _healers;
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _timers;

        public EnemyShamanHealerSystem(GameContext game, 
            IEffectFactory effectFactory,
            ITimeService timeService)
        {
            _effectFactory = effectFactory;
            _timeService = timeService;

            _healers = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Enemy,
                    GameMatcher.WorldPosition,
                    GameMatcher.EnemyShaman,
                    GameMatcher.RadiusToFindEnemy,
                    GameMatcher.CreateEffectInterval));

            _enemies = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Enemy,
                    GameMatcher.WorldPosition)
                .NoneOf(GameMatcher.EnemyShaman));
            
            _timers = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.BuffTimer));
        }

        public void Execute()
        {
            foreach (GameEntity healer in _healers)
            foreach (GameEntity timer in _timers)
            {
                if (timer.BuffTimer > 0)
                {
                    healer.isMoving = healer.isReloadingTimer != true;
                    
                    timer.ReplaceBuffTimer(timer.BuffTimer - _timeService.DeltaTime);
                    continue;
                }

                var enemiesInRange = _enemies.GetEntities()
                    .Where(enemy => Vector3.Distance(healer.WorldPosition, enemy.WorldPosition) <= healer.RadiusToFindEnemy)
                    .ToList();

                if (enemiesInRange.Count == 0)
                    continue;

                foreach (GameEntity enemy in enemiesInRange)
                {
                    foreach (EffectSetup effectSetup in healer.EffectSetups)
                    {
                        _effectFactory.CreateEffect(effectSetup, healer.Id, enemy.Id);
                    }
                }

                timer.ReplaceBuffTimer(healer.CreateEffectInterval);
                
                healer.isReloadingTimer = healer.isReloadingTimer == false;
            }
        }
    }
}