using Code.Common.Destruct;
using Code.Infrastructure.Systems;

namespace Code.Meta
{
    public class HomeScreenFeature : Feature
    {
        public HomeScreenFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}