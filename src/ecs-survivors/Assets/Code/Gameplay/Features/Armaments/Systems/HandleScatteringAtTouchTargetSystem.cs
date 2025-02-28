using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Armaments.Extensions;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Armaments.Systems
{
    public class HandleScatteringAtTouchTargetSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _armaments;
        private readonly List<GameEntity> _bufferArmaments = new(16);
        
        private readonly GameContext _game;
        private readonly IArmamentFactory _armamentFactory;
        private readonly IStaticDataService _staticDataService;

        public HandleScatteringAtTouchTargetSystem (GameContext game, 
            IArmamentFactory armamentFactory,
            IStaticDataService staticDataService)
        {
            _game = game;
            _armamentFactory = armamentFactory;
            _staticDataService = staticDataService;

            _armaments = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Armament,
                    GameMatcher.TargetsBuffer,
                    GameMatcher.Separable));
        }

        public void Execute()
        {
            foreach (GameEntity armament in _armaments.GetEntities(_bufferArmaments))
            foreach (int targetId in armament.TargetsBuffer)
            {
                if(armament.isSeparable == false)
                    continue;
                
                GameEntity currentTarget = _game.GetEntityWithId(targetId);
                
                var abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.ScatteringRuneStoneBolt, 1);
                
                for (int i = 0; i < abilityLevel.ProjectileSetup.ProjectileCount; i++)
                {
                    _armamentFactory
                        .CreateScatteringRuneStoneBolt(1, currentTarget.WorldPosition)
                        .ReplaceDirection(i.GetDirectionByRadian(abilityLevel.ProjectileSetup.SpreadAngle,
                            abilityLevel.ProjectileSetup.ProjectileCount))
                        .With(x => x.isMoving = true)
                        .With(x => x.isSeparable = false);
                }
            }
        }
    }
}