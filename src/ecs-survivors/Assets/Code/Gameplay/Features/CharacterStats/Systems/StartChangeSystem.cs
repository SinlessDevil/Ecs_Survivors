using Entitas;

namespace Code.Gameplay.Features.CharacterStats.Systems
{
    public class StatChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statChanges;
        private readonly IGroup<GameEntity> _statOwners;

        public StatChangeSystem(GameContext game)
        {
            _statChanges = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.StatChange,
                    GameMatcher.TargetId,
                    GameMatcher.EffectValue));

            _statOwners = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Id,
                    GameMatcher.BaseStats,
                    GameMatcher.StatModifiers));
        }

        public void Execute()
        {
            foreach (GameEntity statOwner in _statOwners)
            foreach (Stats statsKey in statOwner.BaseStats.Keys)
            {
                statOwner.StatModifiers[statsKey] = 0;
                foreach (GameEntity statChange in _statChanges)
                {
                    if(statChange.TargetId == statOwner.Id)
                        statOwner.StatModifiers[statsKey] += statChange.EffectValue;
                }
            }
        }
    }
}