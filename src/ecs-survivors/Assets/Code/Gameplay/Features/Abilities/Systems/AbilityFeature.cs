using Code.Gameplay.Features.Cooldowns.Systems;
using Code.Infrastructure.Systems;
using Unity.VisualScripting;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public sealed class AbilityFeature : Feature
    {
        public AbilityFeature(ISystemFactory systems)
        {
            Add(systems.Create<CooldownSystem>());
            Add(systems.Create<DestroyAbilityEntitiesOnUpgrade>());
            
            Add(systems.Create<VegetableBoltAbilitySystem>());
            Add(systems.Create<RadiatingCogBoltAbilitySystem>());
            Add(systems.Create<BouncingCoinBoltAbilitySystem>());
            Add(systems.Create<ScatteringRuneStoneBoltAbilitySystem>());
            Add(systems.Create<OrbitingMushroomAbilitySystem>());
            
            Add(systems.Create<GarlicAuraAbilitySystem>());
        }
    }
}