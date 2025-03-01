using Code.Common.Destruct;
using Code.Gameplay.Features.Abilities.Systems;
using Code.Gameplay.Features.Armaments;
using Code.Gameplay.Features.CharacterStats.Systems;
using Code.Gameplay.Features.EffectApplication;
using Code.Gameplay.Features.Effects.Systems;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Hero;
using Code.Gameplay.Features.LifeTime;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.TargetCollection;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<InputFeature>());
            Add(systemFactory.Create<BindViewFeature>());
            
            Add(systemFactory.Create<HeroFeature>());
            Add(systemFactory.Create<EnemyFeature>());
            Add(systemFactory.Create<DeathFeature>());
            
            Add(systemFactory.Create<MovementFeature>());
            
            Add(systemFactory.Create<CollectTargetFeature>());
           
            Add(systemFactory.Create<AbilityFeature>());
            Add(systemFactory.Create<ArmamentFeature>());
            
            Add(systemFactory.Create<EffectApplicationFeature>());
            
            Add(systemFactory.Create<EffectFeature>());
            Add(systemFactory.Create<StatusFeature>());
            Add(systemFactory.Create<StatFeature>());
            
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}