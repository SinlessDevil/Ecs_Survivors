using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using Code.Gameplay.Features.Enemies;
using Code.Gameplay.Features.Enemies.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<AbilityId, AbilityConfig> _abilityById;
        private Dictionary<EnemyTypeId, EnemyConfig> _enemyById;

        public void LoadAll()
        {
            LoadAbilities();
            LoadEnemies();
        }

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
    }
}