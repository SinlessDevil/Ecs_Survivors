using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectExperienceSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _collected;
        private readonly IGroup<GameEntity> _heroes;

        public CollectExperienceSystem(GameContext game)
        {
            _collected = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Collected,
                    GameMatcher.Experience));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Hero));
        }

        public void Execute()
        {
            foreach (GameEntity collect in _collected)
            foreach (GameEntity hero in _heroes)
            {
                hero.ReplaceExperience(hero.Experience + collect.Experience);
            }
        }
    }
}