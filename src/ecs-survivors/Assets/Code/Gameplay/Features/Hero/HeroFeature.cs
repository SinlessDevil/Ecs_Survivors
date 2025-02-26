using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Cameras.Systems;
using Code.Gameplay.Features.Hero.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Hero
{
  public class HeroFeature : Feature
  {
    public HeroFeature(ISystemFactory systemFactory)
    {
      Add(systemFactory.Create<SetHeroDirectionByInputSystem>());
      Add(systemFactory.Create<CameraFollowHeroSystem>());
      Add(systemFactory.Create<AnimateHeroMovementSystem>());
    }
  }
}