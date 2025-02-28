using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Abilities.Systems
{
    public class RadiatingCogBoltAbilitySystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IArmamentFactory _armamentFactory;
        
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly IGroup<GameEntity> _enemies;
        
        private readonly List<GameEntity> _buffer = new(1);

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
                
            }
        }

        private GameEntity FirstAvailableTarget()
        {
            return _enemies.AsEnumerable().First();
        }

    }
}