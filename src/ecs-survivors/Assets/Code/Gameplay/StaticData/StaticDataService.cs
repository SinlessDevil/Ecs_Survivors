﻿using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Boosters;
using Code.Gameplay.Features.Enchants;
using Code.Gameplay.Features.Enchants.Configs;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Enemies.Configs;
using Code.Gameplay.Features.Hero.Configs;
using Code.Gameplay.Features.LevelUp.Configs;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Configs;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<AbilityId, AbilityConfig> _abilityById;
        private Dictionary<EnemyTypeId, EnemyConfig> _enemyById;
        private Dictionary<EnchantTypeId, EnchantConfig> _enchantById;
        private Dictionary<LootTypeId, LootConfig> _lootById;
        private Dictionary<WindowId, GameObject> _windowPrefabsById;

        private LevelupConfig _levelupRules;
        private HeroConfig _heroConfig;

        public void LoadAll()
        {
            LoadAbilities();
            LoadEnemies();
            LoadHeroConfig();
            LoadEnchants();
            LoadLoots();
            LoadWindows();
            LoadLevelUpRules();
        }
        
        public int MaxLevel => _levelupRules.MaxLevel;

        public float ExperienceForLevel(int level) => _levelupRules.ExperienceForLevel[level];
        
        public HeroConfig HeroConfig => _heroConfig;
        
        public AbilityConfig GetAbilityConfig(AbilityId abilityId)
        {
            if(_abilityById.TryGetValue(abilityId, out AbilityConfig config)) 
                return config;

            throw new Exception($"Ability config for {abilityId} not found");
        }

        public AbilityLevel GetAbilityLevel(AbilityId abilityId, int level)
        {
            AbilityConfig config = GetAbilityConfig(abilityId);

            if (level > config.Levels.Count)
                level = config.Levels.Count;
            
            return config.Levels[level - 1];
        }
        
        public EnemyConfig GetEnemyConfig(EnemyTypeId enemyTypeId)
        {
            if(_enemyById.TryGetValue(enemyTypeId, out EnemyConfig config)) 
                return config;

            throw new Exception($"Enemy config for {enemyTypeId} not found");
        }
        
        public EnchantConfig GetEnchantConfig(EnchantTypeId enchantTypeId)
        {
            if(_enchantById.TryGetValue(enchantTypeId, out EnchantConfig config)) 
                return config;

            throw new Exception($"Enchant config for {enchantTypeId} not found");
        }

        public LootConfig GetLootConfig(LootTypeId lootTypeId)
        {
            if(_lootById.TryGetValue(lootTypeId, out LootConfig config)) 
                return config;

            throw new Exception($"Loot config config for {lootTypeId} not found");
        }
        
        public EnemyLevel GetEnemyLevel(EnemyTypeId enemyTypeId, int level)
        {
            EnemyConfig config = GetEnemyConfig(enemyTypeId);

            if (level > config.Levels.Count)
                level = config.Levels.Count;
            
            return config.Levels[level - 1];
        }
        
        public GameObject GetWindowPrefab(WindowId windowId)
        {
           return _windowPrefabsById.TryGetValue(windowId, out GameObject windowPrefab)
                ? windowPrefab
                : throw new Exception($"Prefab config for window {windowId} was not found");
        }
        
        private void LoadAbilities()
        {
            _abilityById = Resources
                .LoadAll<AbilityConfig>("Configs/Abilities")
                .ToDictionary(x => x.AbilityId, x => x);
        }
        
        private void LoadEnemies()
        {
            _enemyById = Resources
                .LoadAll<EnemyConfig>("Configs/Enemies")
                .ToDictionary(x => x.EnemyTypeId, x => x);
        }


        private void LoadEnchants()
        {
            _enchantById = Resources
                .LoadAll<EnchantConfig>("Configs/Enchants")
                .ToDictionary(x => x.EnchantTypeId, x => x);
        }

        private void LoadLoots()
        {
            _lootById = Resources
                .LoadAll<LootConfig>("Configs/Loots")
                .ToDictionary(x => x.LootTypeId, x => x);
        }
        
        private void LoadHeroConfig()
        {
            _heroConfig = Resources.Load<HeroConfig>("Configs/Heroes/HeroConfig");
        }
        
        private void LoadWindows()
        {
            _windowPrefabsById = Resources
                .Load<WindowsConfig>("Configs/Windows/WindowConfig")
                .WindowConfigs
                .ToDictionary(x => x.Id, x => x.Prefab);
        }
        private void LoadLevelUpRules()
        {
            _levelupRules = Resources.Load<LevelupConfig>("Configs/Levelup/LevelupConfig");
        }
    }
}
