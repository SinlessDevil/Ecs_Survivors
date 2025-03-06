using Code.Common.Destruct;
using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation;
using Code.Meta.Features.Simulation.Systems;

namespace Code.Meta
{
    public class HomeScreenFeature : Feature
    {
        public HomeScreenFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<EmitTickSystem>(MetaConstants.SimulationTickSeconds));
            
            Add(systemFactory.Create<SimulationFeature>());

            Add(systemFactory.Create<CleanupTickSystem>());
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}