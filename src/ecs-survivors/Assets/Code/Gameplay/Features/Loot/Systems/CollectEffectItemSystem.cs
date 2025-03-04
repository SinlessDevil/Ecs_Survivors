using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectEffectItemSystem : IExecuteSystem
    {
        private readonly IStatusApplier _statusApplier;
        private readonly IGroup<GameEntity> _collected;
        private readonly IGroup<GameEntity> _heroes;

        public CollectEffectItemSystem(GameContext game, IStatusApplier statusApplier)
        {
            _statusApplier = statusApplier;
            
            _collected = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Collected,
                    GameMatcher.StatusSetups));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Hero,
                    GameMatcher.Id,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity collect in _collected)
            foreach (GameEntity hero in _heroes)
            foreach (StatusSetup statusSetup in collect.StatusSetups)
            {
                _statusApplier.ApplyStatusOnProducer(statusSetup, hero.Id, hero.Id);
            }
        }
    }
}