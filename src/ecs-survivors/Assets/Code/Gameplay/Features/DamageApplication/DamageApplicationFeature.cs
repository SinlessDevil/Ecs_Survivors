using Code.Gameplay.Features.DamageApplication.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.DamageApplication
{
    public class DamageApplicationFeature : Feature
    {
        public DamageApplicationFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ApplyDamageOnTargetsSystem>());
            Add(systemFactory.Create<DestructOnZeroHpSystem>());
        }
    }
}