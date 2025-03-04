using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Boosters.Factroy
{
    public class BoosterFactory : IBoosterFactory
    {
        private const int TargetBufferSize = 1;
        
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public BoosterFactory(
            IIdentifierService identifierService, 
            IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }
        
        public GameEntity CreateBooster(BoosterTypeId typeId, Vector2 at)
        {
            switch (typeId)
            {
                case BoosterTypeId.MaxHPUpBooster:
                    return CreateMaxHPUpBooster(typeId, at);
                case BoosterTypeId.InvulnerabilityBooster:
                    return CreateInvulnerabilityBooster(typeId, at);
            }
            
            throw new Exception($"Booster with type id {typeId} does not exist");
        }

        private GameEntity CreateMaxHPUpBooster(BoosterTypeId typeId, Vector3 at)
        {
            BoosterConfig boosterConfig = _staticDataService.GetBoosterConfig(BoosterTypeId.MaxHPUpBooster);

            return CreateEntity.Empty()
                    .AddId(_identifierService.Next())
                    .AddWorldPosition(at)
                    .AddBoosterTypeId(typeId)
                    .AddEffectSetups(boosterConfig.EffectSetups)
                    .AddStatusSetups(boosterConfig.StatusSetups)
                    .AddRadius(boosterConfig.RadiusToCollectTargets)
                    .AddTargetsBuffer(new List<int>(TargetBufferSize))
                    .AddLayerMask(CollisionLayer.Hero.AsMask())
                    .With(x => x.isReadyToCollectTargets = true)
                    .With(x => x.isCollectingTargetsContinuously = true)
                    .AddSelfDestructTimer(boosterConfig.LifeTime)
                    .AddViewPath("Gameplay/Boosters/MaxHPUpBooster")
                    .With(x => x.isBooster = true);
        }
        
        private GameEntity CreateInvulnerabilityBooster(BoosterTypeId typeId, Vector3 at)
        {
            BoosterConfig boosterConfig = _staticDataService.GetBoosterConfig(BoosterTypeId.InvulnerabilityBooster);

            return CreateEntity.Empty()
                .AddId(_identifierService.Next())
                .AddWorldPosition(at)
                .AddBoosterTypeId(typeId)
                .AddEffectSetups(boosterConfig.EffectSetups)
                .AddStatusSetups(boosterConfig.StatusSetups)
                .AddRadius(boosterConfig.RadiusToCollectTargets)
                .AddTargetsBuffer(new List<int>(TargetBufferSize))
                .AddLayerMask(CollisionLayer.Hero.AsMask())
                .With(x => x.isReadyToCollectTargets = true)
                .With(x => x.isCollectingTargetsContinuously = true)
                .AddSelfDestructTimer(boosterConfig.LifeTime)
                .AddViewPath("Gameplay/Boosters/InvulnerabilityBooster")
                .With(x => x.isBooster = true);
        }
    }
}