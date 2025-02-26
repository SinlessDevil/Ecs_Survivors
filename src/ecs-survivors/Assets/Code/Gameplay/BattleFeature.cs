using Code.Common.Destruct;
using Code.Gameplay.Features.Hero;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<InputFeature>());
            Add(systemFactory.Create<HeroFeature>());
            Add(systemFactory.Create<MovementFeature>());
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}