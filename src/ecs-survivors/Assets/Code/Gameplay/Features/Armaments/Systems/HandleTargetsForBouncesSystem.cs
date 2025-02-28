using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Systems
{
    public class HandleTargetsForBouncesSystem  : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _armaments;
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _heroes;
        
        private readonly GameContext _game;
        
        private readonly List<GameEntity> _bufferHero = new(1);
        private readonly List<GameEntity> _bufferArmaments = new(16);

        public HandleTargetsForBouncesSystem (GameContext game)
        {
            _game = game;
            
            _armaments = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Armament,
                    GameMatcher.Target,
                    GameMatcher.BounceRate,
                    GameMatcher.TargetsBuffer));
            
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
            
            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes.GetEntities(_bufferHero))
            foreach (GameEntity armament in _armaments.GetEntities(_bufferArmaments))
            foreach (int targetId in armament.TargetsBuffer)
            {
                if(armament.BounceRate <= 0)
                    continue;
                
                if (armament.hasTarget)
                {
                    GameEntity currentTarget = _game.GetEntityWithId(targetId);
                    if (currentTarget != null && currentTarget.hasWorldPosition)
                    {
                        GameEntity newTarget = FindNearestTarget(currentTarget);

                        if (newTarget != null)
                        {
                            armament.ReplaceTarget(newTarget)
                                .ReplaceBounceRate(armament.BounceRate - 1)
                                .ReplaceDirection((armament.Target.WorldPosition - hero.WorldPosition).normalized);
                        }
                    }
                }
            }
        }
        
        private GameEntity FindNearestTarget(GameEntity currentTarget)
        {
            float nearestDistance = float.MaxValue;
            GameEntity nearestTarget = null;
            
            foreach (GameEntity enemy in _enemies)
            {
                if (enemy == currentTarget)
                    continue;

                float distance = Vector3.Distance(enemy.WorldPosition,
                    currentTarget.WorldPosition);
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = enemy;
                }
            }

            return nearestTarget;
        }
    }
}