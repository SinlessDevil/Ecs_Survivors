using Code.Gameplay.Features.Abilities.Factory;
using Code.Gameplay.Features.Hero.Factory;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.Features.Statuses.Applier;
using Code.Gameplay.Levels;
using Entitas;

namespace Code.Gameplay.Features.Hero.Systems
{
    public class InitializeHeroSystem : IInitializeSystem
    {
        private readonly IHeroFactory _heroFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IAbilityFactory _abilityFactory;
        private readonly IStatusApplier _statusApplier;

        public InitializeHeroSystem(
            IHeroFactory heroFactory, 
            ILevelDataProvider levelDataProvider,
            IAbilityFactory abilityFactory,
            IStatusApplier statusApplier)
        {
            _heroFactory = heroFactory;
            _levelDataProvider = levelDataProvider;
            _abilityFactory = abilityFactory;
            _statusApplier = statusApplier;
        }
        
        public void Initialize()
        {
            GameEntity hero = _heroFactory.CreateHero(_levelDataProvider.StartPoint);
            
            _abilityFactory.CreateVegetableBoltAbility(level: 1);
            // _abilityFactory.CreateOrbitingMushroomBolt(level: 1);
            //_abilityFactory.CreateRadiatingCogBoltAbility(level: 1);
            //_abilityFactory.CreateBouncingCoinBoltAbility(level: 1);
            //_abilityFactory.CreateScatteringRuneStoneBolt(level: 1);
            
            // _abilityFactory.CreateGarlicAuraAbility();
            
            // _statusApplier.ApplyStatusOnTarget(new StatusSetup()
            // {
            //     StatusTypeId = StatusTypeId.PoisonEnchant,
            //     Duration = 10,
            //     StatusApplicationTypeId = StatusApplicationTypeId.Target,
            //     StatusTriggerConditionTypeId =StatusTriggerConditionTypeId.OnTouch,
            // }, hero.Id, hero.Id);
            
            _statusApplier.ApplyStatusOnTarget(new StatusSetup()
            {
                StatusTypeId = StatusTypeId.ExplosiveEnchant,
                Duration = 10,
                StatusApplicationTypeId = StatusApplicationTypeId.Target,
                StatusTriggerConditionTypeId =StatusTriggerConditionTypeId.OnTouch,
            }, hero.Id, hero.Id);
        }
    }
}