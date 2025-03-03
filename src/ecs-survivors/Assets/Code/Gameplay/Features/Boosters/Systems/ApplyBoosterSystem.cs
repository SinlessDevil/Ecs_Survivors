using System.Collections.Generic;
using Code.Gameplay.Features.LifeTime;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Boosters.Systems
{
    public class ApplyBoosterSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        
        private readonly IGroup<GameEntity> _boosters;
        
        private readonly List<GameEntity> _bufferBooster = new(128);

        public ApplyBoosterSystem(GameContext game)
        {
            _game = game;
            
            _boosters = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Booster, 
                    GameMatcher.TargetsBuffer)
                .NoneOf(GameMatcher.ApplyBooster));
        }

        public void Execute()
        {
            foreach (GameEntity booster in _boosters.GetEntities(_bufferBooster))
            foreach (int targetId in booster.TargetsBuffer)
            {
                GameEntity currentTarget = _game.GetEntityWithId(targetId);
                
                if (currentTarget.isHero)
                {
                    booster.isApplyBooster = true;
                }
            }
        }
    }
}