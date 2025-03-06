using Code.Infrastructure.Systems;

namespace Code.Meta.Features.Simulation
{
    public sealed class SimulationFeature : Feature
    {
        public SimulationFeature(ISystemFactory systems)
        {
            Add(systems.Create<AfkGoldGainSystem>());
            Add(systems.Create<UpdateSimulationTimeSystem>());
        }
    }
}