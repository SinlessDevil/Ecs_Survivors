using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Hero.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private float _hp = 100;
        private float _speed = 2;
        
        private readonly IIdentifierService _identifierService;

        public HeroFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }
        
        public GameEntity CreateHero(Vector3 at)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddWorldPosition(at)
                .AddDirection(Vector2.zero)
                .AddSpeed(_speed)
                .AddCurrentHp(_hp)
                .AddMaxHp(_hp)
                .AddViewPath("Gameplay/Hero/hero")
                .With(x => x.isHero = true)
                .With(x => x.isTurnedAlongDirection = true)
                .With(x => x.isMovementAvailable = true);
        }
    }
}