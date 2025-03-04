using Code.Common.Destruct;
using Code.Gameplay.Features.Abilities.Systems;
using Code.Gameplay.Features.Armaments;
using Code.Gameplay.Features.Boosters;
using Code.Gameplay.Features.CharacterStats.Systems;
using Code.Gameplay.Features.EffectApplication;
using Code.Gameplay.Features.Effects.Systems;
using Code.Gameplay.Features.Enchants.Systems;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Hero;
using Code.Gameplay.Features.LifeTime;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.TargetCollection;
using Code.Gameplay.Input;
using Code.Infrastructure.Systems;
using Code.Infrastructure.View;
using Unity.VisualScripting;

namespace Code.Gameplay
{
    public class BattleFeature : Feature
    {
        public BattleFeature(ISystemFactory system)
        {
            Add(system.Create<InputFeature>());
            Add(system.Create<BindViewFeature>());
            
            Add(system.Create<HeroFeature>());
            Add(system.Create<EnemyFeature>());
            Add(system.Create<DeathFeature>());
            
            Add(system.Create<LootingFeature>());
            
            Add(system.Create<MovementFeature>());
            Add(system.Create<AbilityFeature>());
            
            Add(system.Create<ArmamentFeature>());
            
            Add(system.Create<CollectTargetFeature>());
            Add(system.Create<EffectApplicationFeature>());
           // Add(systemFactory.Create<BoosterFeature>());
           
            Add(system.Create<EnchantFeature>());
            Add(system.Create<EffectFeature>());
            Add(system.Create<StatusFeature>());
            Add(system.Create<StatFeature>());
            
            Add(system.Create<ProcessDestructedFeature>());
        }
    }
}