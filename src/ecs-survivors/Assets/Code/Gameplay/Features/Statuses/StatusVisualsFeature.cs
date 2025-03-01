using Code.Gameplay.Features.Statuses.Systems.StatusVisuals;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Statuses
{
    public class StatusVisualsFeature : Feature
    {
        public StatusVisualsFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ApplyPoisonVisualsSystem>());
            Add(systemFactory.Create<UnapplyPoisonVisualsSystem>());
        }
    }
}