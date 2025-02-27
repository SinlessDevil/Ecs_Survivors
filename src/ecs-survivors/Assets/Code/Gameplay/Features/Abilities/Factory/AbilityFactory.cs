using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;

namespace Code.Gameplay.Features.Abilities.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public AbilityFactory(
            IIdentifierService identifierService, 
            IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateVegetableBoltAbility(int level)
        {
            AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.VegetableBolt, level);
            
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddAbilityId(AbilityId.VegetableBolt)
                .AddCooldown(abilityLevel.Cooldown)
                .With(x => x.isVegetableBoltAbility = true)
                .PutOnCooldown();
        }
    }
}