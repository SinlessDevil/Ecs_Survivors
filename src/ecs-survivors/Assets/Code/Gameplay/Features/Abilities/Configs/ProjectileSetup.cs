using System;
using UnityEngine;

namespace Code.Gameplay.Features.Abilities.Configs
{
    [Serializable]
    public class ProjectileSetup
    {
        [Space(10)] [Header("<----- Projectile Default ----->")]
        public float Speed;
        public int Pierce;
        public float ContactRadius;
        public float LifeTime;
        
        [Space(10)] [Header("Radiating")]
        public int ProjectileCount;
        [Range(0, 360)] public float SpreadAngle;
        
        [Space(10)] [Header("Bouncing")]
        public int MaxBounces;
        public float TargetSearchRadius;
        
        [Space(10)] [Header("Scattering")]
        public int SpawnedProjectilesCount;
        public float ScaleFactor;
    }
}