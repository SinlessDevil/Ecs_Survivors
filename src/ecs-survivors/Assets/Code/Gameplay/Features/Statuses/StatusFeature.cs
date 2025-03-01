using Code.Gameplay.Features.Statuses.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Statuses
{
    public class StatusFeature : Feature
    {
        public StatusFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<StatusDurationSystem>());
            Add(systemFactory.Create<PeriodicDamageStatusSystem>());
            
            Add(systemFactory.Create<StatusVisualsFeature>());
            
            Add(systemFactory.Create<CleanupUnappliedStatuses>());
        }
    }
}