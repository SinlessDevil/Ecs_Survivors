using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Boosters;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.Enchants.Configs;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.Windows;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        public void LoadAll();

        public HeroConfig HeroConfig { get; }
        public int MaxLevel { get; }
        public float ExperienceForLevel(int level);
        
        public AbilityConfig GetAbilityConfig(AbilityId abilityId);
        public AbilityLevel GetAbilityLevel(AbilityId abilityId, int level);

        public EnemyConfig GetEnemyConfig(EnemyTypeId enemyTypeId);
        public EnemyLevel GetEnemyLevel(EnemyTypeId enemyTypeId, int level);
        
        public BoosterConfig GetBoosterConfig(BoosterTypeId boosterTypeId);
        public EnchantConfig GetEnchantConfig(EnchantTypeId enchantTypeId);
        public LootConfig GetLootConfig(LootTypeId lootTypeId);
        
        public GameObject GetWindowPrefab(WindowId windowId);
    }
}