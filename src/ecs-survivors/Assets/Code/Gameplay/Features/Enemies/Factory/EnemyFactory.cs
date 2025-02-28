using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Effects;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private float _hp = 3;
        private float _damage = 1;
        private float _speed = 1;
        
        private readonly IIdentifierService _identifierService;

        public EnemyFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }
        
        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case EnemyTypeId.Goblin:
                    return CreateGoblin(typeId, at);
            }
            
            throw new Exception($"Enemy with type id {typeId} does not exist");
        }

        private GameEntity CreateGoblin(EnemyTypeId typeId, Vector3 at)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddEnemyTypeID(typeId)
                .AddWorldPosition(at)
                .AddDirection(Vector2.zero)
                .AddSpeed(_speed)
                .AddCurrentHp(_hp)
                .AddMaxHp(_hp)
                .AddEffectSetups(new List<EffectSetup>(){new EffectSetup(){EffectTypeId = EffectTypeId.Damage, Value = 1}})
                .AddTargetsBuffer(new List<int>(1))
                .AddRadius(0.3f)
                .AddCollectTargetsInterval(0.5f)
                .AddCollectTargetsTimer(0)
                .AddLayerMask(CollisionLayer.Hero.AsMask())
                .AddViewPath("Gameplay/Enemies/Goblins/Torch/goblin_torch_blue")
                .With(x => x.isEnemy = true)
                .With(x => x.isTurnedAlongDirection = true)
                .With(x => x.isMovementAvailable = true);
        }
    }
}