using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class ScatteringRuneStoneBoltAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;
        private readonly IAbilityUpgradeService _abilityUpgradeService;

        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;
        
        private readonly List<GameEntity> _buffer = new(1);

        public ScatteringRuneStoneBoltAbilitySystem(GameContext game, 
            IStaticDataService staticDataService, 
            IArmamentFactory armamentFactory,
            IAbilityUpgradeService abilityUpgradeService)
        {
            _staticDataService = staticDataService;
            _armamentFactory = armamentFactory;
            _abilityUpgradeService = abilityUpgradeService;

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.ScatteringRuneStoneAbility, 
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
                if(_enemies.count <= 0)
                    continue;

                int level = _abilityUpgradeService.GetAbilityLevel(AbilityId.ScatteringRuneStoneBolt);
                
                _armamentFactory
                    .CreateScatteringRuneStoneBolt(level, hero.WorldPosition)
                    .AddProducerId(hero.Id)
                    .ReplaceDirection((FirstAvailableTarget().WorldPosition - hero.WorldPosition).normalized)
                    .With(x => x.isMoving = true)
                    .With(x => x.isSeparable = true);
                
                ability
                    .PutOnCooldown(_staticDataService.GetAbilityLevel(AbilityId.ScatteringRuneStoneBolt, level).Cooldown);
            }
        }

        private GameEntity FirstAvailableTarget()
        {
            return _enemies.AsEnumerable().First();
        }
    }
}