using Code.Common.Extensions;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Registrars
{
  public class HeroRegistrar : EntityComponentRegistrar
  {
    public float Speed = 2;
    
    public override void RegisterComponents()
    {
      Entity
          .AddWorldPosition(transform.position)
          .AddDirection(Vector2.zero)
          .AddSpeed(Speed)
          .With(x => x.isHero = true)
          .With(x => x.isTurnedAlongDirection = true)
        ;
    }

    public override void UnregisterComponents()
    {
      
    }
  }
}