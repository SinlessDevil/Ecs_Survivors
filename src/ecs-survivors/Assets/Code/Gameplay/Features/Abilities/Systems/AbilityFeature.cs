using Code.Gameplay.Features.Cooldowns.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public sealed class AbilityFeature : Feature
    {
        public AbilityFeature(ISystemFactory systems)
        {
            Add(systems.Create<CooldownSystem>());
            
            Add(systems.Create<VegetableBoltAbilitySystem>());
            Add(systems.Create<RadiatingCogBoltAbilitySystem>());
            Add(systems.Create<BouncingCoinBoltAbilitySystem>());
            Add(systems.Create<ScatteringRuneStoneBoltAbilitySystem>());
        }
    }
}