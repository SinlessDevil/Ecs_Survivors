using System.Collections.Generic;
using Code.Gameplay.Features.TargetCollection;
using Entitas;

namespace Code.Gameplay.Features.Boosters.Systems
{
    public class FinalizeBoosterDestroyProcessingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _boosters;
        private readonly List<GameEntity> _buffer = new(128);

        public FinalizeBoosterDestroyProcessingSystem(GameContext game)
        {
            _boosters = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Booster, 
                    GameMatcher.ApplyBooster
                    ));
        }

        public void Execute()
        {
            foreach (GameEntity booster in _boosters.GetEntities(_buffer))
            {
                booster.RemoveTargetCollectionComponents();
                booster.isDestructed = true;
            }
        }
    }
}