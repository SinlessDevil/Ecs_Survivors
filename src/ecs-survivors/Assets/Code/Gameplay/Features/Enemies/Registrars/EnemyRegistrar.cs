using Code.Common.Extensions;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Registrars
{
    public class EnemyRegistrar : EntityComponentRegistrar
    {
        public float Speed = 1;

        public override void RegisterComponents()
        {
            Entity
                .AddEnemyTypeID(EnemyTypeId.Goblin)
                .AddWorldPosition(transform.position)
                .AddDirection(Vector2.zero)
                .AddSpeed(Speed)
                .With(x => x.isEnemy = true)
                .With(x => x.isTurnedAlongDirection = true);
        }

        public override void UnregisterComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}