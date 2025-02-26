using Code.Common.Destruct;
using Code.Gameplay.Features.DamageApplication;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Hero;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.TargetCollection;
using Code.Gameplay.Features.TargetCollection.Systems;
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
            Add(systemFactory.Create<EnemyFeature>());
            Add(systemFactory.Create<MovementFeature>());
            Add(systemFactory.Create<CollectTargetFeature>());
            Add(systemFactory.Create<DamageApplicationFeature>());
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}