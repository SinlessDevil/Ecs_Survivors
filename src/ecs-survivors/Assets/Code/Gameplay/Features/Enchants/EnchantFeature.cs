using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public sealed class EnchantFeature : Feature
    {
        public EnchantFeature(ISystemFactory systems)
        {
            Add(systems.Create<PoisonEnchantSystem>());

            Add(systems.Create<ApplyPoisonEnchantVisualsSystem>());
        }
    }
}