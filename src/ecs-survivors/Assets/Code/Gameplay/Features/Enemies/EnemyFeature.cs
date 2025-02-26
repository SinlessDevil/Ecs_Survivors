using Code.Gameplay.Features.Enemies.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Enemies
{
    public class EnemyFeature : Feature
    {
        public EnemyFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ChaseHeroSystem>());
        }
    }
}