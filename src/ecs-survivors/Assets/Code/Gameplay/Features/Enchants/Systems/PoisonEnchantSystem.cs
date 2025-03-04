using System.Collections.Generic;
using Code.Gameplay.Features.Statuses;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class PoisonEnchantSystem : IExecuteSystem
    {
        private readonly IStaticDataService _staticDataService;
        
        private readonly IGroup<GameEntity> _enchants;
        private readonly IGroup<GameEntity> _armamets;
        
        private readonly List<GameEntity> _buffer = new(32);

        public PoisonEnchantSystem(GameContext game, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            
            _enchants = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.EnchantTypeId,
                    GameMatcher.ProducerId,
                    GameMatcher.PoisonEnchant));
            
            _armamets = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Armament,
                    GameMatcher.ProducerId)
                .NoneOf(GameMatcher.PoisonEnchant));

        }

        public void Execute()
        {
            foreach (GameEntity enchant in _enchants)
            foreach (GameEntity armamet in _armamets.GetEntities(_buffer))
            {
                if (enchant.ProducerId == armamet.ProducerId)
                {
                    GetOrAddStatusSetups(armamet)
                        .AddRange(_staticDataService.GetEnchantConfig(EnchantTypeId.PoisonArmaments).StatusSetups);
                    
                    armamet.isPoisonEnchant = true;
                }
            }
        }

        private static List<StatusSetup> GetOrAddStatusSetups(GameEntity armamet)
        {
            if(!armamet.hasStatusSetups)
                armamet.AddStatusSetups(new List<StatusSetup>());
            
            return armamet.StatusSetups;
        }
    }
}