using Code.Common.Destruct;
using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation;

namespace Code.Meta
{
    public class HomeScreenFeature : Feature
    {
        public HomeScreenFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<SimulationFeature>());
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}