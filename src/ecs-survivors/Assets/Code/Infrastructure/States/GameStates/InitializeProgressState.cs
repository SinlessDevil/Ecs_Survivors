using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
{
    public class InitializeProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;
        private readonly IStaticDataService _staticDataService;

        public InitializeProgressState(
            IGameStateMachine stateMachine,
            IProgressProvider progressProvider,
            IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            InitializeProgress();

            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        private void InitializeProgress()
        {
            CreateNewProgress();
        }

        private void CreateNewProgress()
        {
            _progressProvider.SetProgressData(new ProgressData());

            CreateMetaEntity.Empty()
                .With(x => x.isStorage = true)
                .AddGold(0)
                .AddGoldPerSecond(_staticDataService.AfkGainConfig.GoldPerSecond);
        }

        public void Exit()
        {
        }
    }
}