using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Services;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameResultStates
{
    public class GameOverState : SimpleState
    {
        private readonly IAbilityUpgradeService _abilityUpgradeService;
        private readonly IWindowService _windowService;

        public GameOverState(IAbilityUpgradeService abilityUpgradeService, IWindowService windowService)
        {
            _abilityUpgradeService = abilityUpgradeService;
            _windowService = windowService;
        }
    
        public override void Enter()
        {
            _abilityUpgradeService.Cleanup();
      
            _windowService.Open(WindowId.GameOverWindow);
        }
    }
}