using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class RadiatingCogBoltAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;

        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;

        private readonly List<GameEntity> _buffer = new(64);

        public RadiatingCogBoltAbilitySystem(GameContext game,
            IStaticDataService staticDataService,
            IArmamentFactory armamentFactory)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.VegetableBoltAbility,
                    GameMatcher.CooldownUp));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Hero,
                    GameMatcher.WorldPosition));

            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            foreach (GameEntity hero in _heroes)
            {
                if (_enemies.count <= 0)
                    continue;

                var abilityLevel = _staticDataService.GetAbilityLevel(AbilityId.RadiatingCogBolt, 1);

                for (int i = 0; i < abilityLevel.ProjectileSetup.ProjectileCount; i++)
                {
                    _armamentFactory
                        .CreateRadiatingCogBolt(1, hero.WorldPosition)
                        .ReplaceDirection(GetDirectionByRadian(i, abilityLevel.ProjectileSetup.SpreadAngle, abilityLevel.ProjectileSetup.ProjectileCount))
                        .With(x => x.isMoving = true);

                    ability
                        .PutOnCooldown(abilityLevel.Cooldown);
                }
            }
        }

        private Vector2 GetDirectionByRadian(int i, float spreadAngle, int projectileCount)
        {
            float step = spreadAngle / projectileCount;
            float angle = -spreadAngle / 2 + i * step;
            float radian = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

    }
}