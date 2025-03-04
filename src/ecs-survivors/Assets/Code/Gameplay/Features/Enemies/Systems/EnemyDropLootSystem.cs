using System.Collections.Generic;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Factory;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyDropLootSystem : IExecuteSystem
    {
        private readonly ILootFactory _lootFactory;
        private readonly IGroup<GameEntity> _enemies;
        private readonly List<GameEntity> _buffer = new(128);

        public EnemyDropLootSystem(GameContext game, ILootFactory lootFactory)
        {
            _lootFactory = lootFactory;
            
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy, 
                    GameMatcher.Dead, 
                    GameMatcher.ProcessingDeath,
                    GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity enemy in _enemies.GetEntities(_buffer))
            {
                if (UnityEngine.Random.Range(0, 1f) <= 0.15f)
                    _lootFactory.CreateLootItem(LootTypeId.HealingItem, enemy.WorldPosition);
                else if (UnityEngine.Random.Range(0, 1f) <= 0.15f)
                    _lootFactory.CreateLootItem(LootTypeId.PoisonEnchantItem, enemy.WorldPosition);
                else if (UnityEngine.Random.Range(0, 1f) <= 0.15f)
                    _lootFactory.CreateLootItem(LootTypeId.ExplosionEnchantItem, enemy.WorldPosition);
                else
                    _lootFactory.CreateLootItem(LootTypeId.ExpGem, enemy.WorldPosition);

            }
        }
    }
}