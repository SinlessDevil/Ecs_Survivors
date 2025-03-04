using System;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Enchants;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Statuses.Factory
{
    public class StatusFactory : IStatusFactory
    {
        private readonly IIdentifierService _identifierService;

        public StatusFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId)
        {
            GameEntity status = null;

            switch (setup.StatusTypeId)
            {
                case StatusTypeId.Poison:
                    status = CreatePoisonStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.Freeze:
                    status = CreateFreezeStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.SpeedUp:
                    status = CreateSpeedUpStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.MaxHpUp:
                    status = CreateMaxHpUpStatus(setup, producerId, targetId);
                    break;
                case StatusTypeId.PoisonEnchant:
                    status = CreatePoisonEnchantStatus(setup, producerId, targetId);
                    break;
                default:
                    throw new Exception($"Status with type id {setup.StatusTypeId} does not exist");
            }

            status
                .With(x => x.AddDuration(setup.Duration), when: setup.Duration > 0)
                .With(x => x.AddTimeLeft(setup.Duration), when: setup.Duration > 0)
                .With(x => x.AddPeriod(setup.Period), when: setup.Period > 0)
                .With(x => x.AddTimeSinceLastTick(0), when: setup.Period > 0);

            return status;
        }

        private GameEntity CreatePoisonStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(StatusTypeId.Poison)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isPoison = true);
        }

        private GameEntity CreateFreezeStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(StatusTypeId.Freeze)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isFreeze = true);
        }

        private GameEntity CreateSpeedUpStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(StatusTypeId.SpeedUp)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isSpeedUp = true);
        }
        
        private GameEntity CreateMaxHpUpStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(StatusTypeId.MaxHpUp)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isMaxHpUp = true);
        }
        
        private GameEntity CreatePoisonEnchantStatus(StatusSetup setup, int producerId, int targetId)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddStatusTypeId(StatusTypeId.PoisonEnchant)
                .AddEnchantTypeId(EnchantTypeId.PoisonArmaments)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(x => x.isStatus = true)
                .With(x => x.isPoison = true)
                .With(x => x.isPoisonEnchant = true);
        }
    }
}