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

        private readonly List<(LootTypeId, float)> _lootTable = new()
        {
            (LootTypeId.HealingItem, 15f),
            (LootTypeId.PoisonEnchantItem, 15f),
            (LootTypeId.ExplosionEnchantItem, 15f),
            (LootTypeId.HexEnchantItem, 15f),
            (LootTypeId.ExpGem, 40f),
        };

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
                LootTypeId selectedLoot = GetRandomLoot();
                _lootFactory.CreateLootItem(selectedLoot, enemy.WorldPosition);
            }
        }

        private LootTypeId GetRandomLoot()
        {
            float totalWeight = 0f;
            foreach (var loot in _lootTable)
                totalWeight += loot.Item2;

            float randomValue = UnityEngine.Random.Range(0, totalWeight);
            float cumulativeWeight = 0f;

            foreach (var loot in _lootTable)
            {
                cumulativeWeight += loot.Item2;
                if (randomValue < cumulativeWeight)
                    return loot.Item1;
            }

            return LootTypeId.ExpGem;
        }
    }
}
