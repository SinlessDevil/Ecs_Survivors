using Code.Common.Entity;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.LevelUp.UIFactory;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class LevelUpWindow : BaseWindow
    {
        public Transform AbilityLayout;

        private IAbilityUIFactory _abilityFactory;
        private IAbilityUpgradeService _abilityUpgradeService;
        private IStaticDataService _staticDataService;
        private IWindowService _windowService;
        
        [Inject]
        private void Construct(
            IAbilityUIFactory abilityUIFactory, 
            IAbilityUpgradeService abilityUpgradeService,
            IStaticDataService staticDataService,
            IWindowService windowService)
        {
            Id = WindowId.LevelUpWindow;

            _abilityFactory = abilityUIFactory;
            _abilityUpgradeService = abilityUpgradeService;
            _staticDataService = staticDataService;
            _windowService = windowService;
        }

        protected override void Initialize()
        {
            foreach (AbilityUpgradeOption variableOption in _abilityUpgradeService.GetUpgradeOptions())
            {
                AbilityLevel abilityLevel = _staticDataService.GetAbilityLevel(variableOption.Id, variableOption.Level);
                
                _abilityFactory.CreateAbilityCard(AbilityLayout)
                    .Setup(variableOption.Id, abilityLevel, OnSelected);
            }
        }

        private void OnSelected(AbilityId id)
        {
            CreateEntity.Empty()
                .AddAbilityId(id)
                .isUpgradeRequest = true;
            
            _windowService.Close(Id);
        }
    }
}