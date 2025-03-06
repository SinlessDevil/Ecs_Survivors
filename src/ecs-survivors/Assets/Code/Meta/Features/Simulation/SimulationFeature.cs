using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation.Systems;

namespace Code.Meta.Features.Simulation
{
    public sealed class SimulationFeature : Feature
    {
        public SimulationFeature(ISystemFactory systems)
        {
            Add(systems.Create<EmitTickSystem>(MetaConstants.SimulationTickSeconds));
            Add(systems.Create<AfkGoldGainSystem>());
            
            Add(systems.Create<CleanupTickSystem>());
        }
    }
}