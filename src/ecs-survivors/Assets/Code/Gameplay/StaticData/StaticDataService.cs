using System;
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
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<AbilityId, AbilityConfig> _abilityById;
        private Dictionary<EnemyTypeId, EnemyConfig> _enemyById;
        private Dictionary<BoosterTypeId, BoosterConfig> _boosterById;
        private Dictionary<EnchantTypeId, EnchantConfig> _enchantById;
        
        private HeroConfig _heroConfig;

        public void LoadAll()
        {
            LoadAbilities();
            LoadEnemies();
            LoadHeroConfig();
            LoadBoosters();
            LoadEnchants();
        }

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

        public BoosterConfig GetBoosterConfig(BoosterTypeId boosterTypeId)
        {
            if(_boosterById.TryGetValue(boosterTypeId, out BoosterConfig config)) 
                return config;

            throw new Exception($"Booster config for {boosterTypeId} not found");
        }
        
        public EnchantConfig GetEnchantConfig(EnchantTypeId enchantTypeId)
        {
            if(_enchantById.TryGetValue(enchantTypeId, out EnchantConfig config)) 
                return config;

            throw new Exception($"Enchant config for {enchantTypeId} not found");
        }
        
        public EnemyLevel GetEnemyLevel(EnemyTypeId enemyTypeId, int level)
        {
            EnemyConfig config = GetEnemyConfig(enemyTypeId);

            if (level > config.Levels.Count)
                level = config.Levels.Count;
            
            return config.Levels[level - 1];
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

        private void LoadBoosters()
        {
            _boosterById = Resources
                .LoadAll<BoosterConfig>("Configs/Boosters")
                .ToDictionary(x => x.BoosterTypeId, x => x);
        }

        private void LoadEnchants()
        {
            _enchantById = Resources
                .LoadAll<EnchantConfig>("Configs/Enchants")
                .ToDictionary(x => x.EnchantTypeId, x => x);
        }
        
        private void LoadHeroConfig()
        {
            _heroConfig = Resources.Load<HeroConfig>("Configs/Heroes/HeroConfig");
        }
    }
}