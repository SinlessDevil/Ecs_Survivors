using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using UnityEngine;

namespace Code.Gameplay.Features.Boosters
{
    [CreateAssetMenu(menuName = "ECS Survivors/Boosters", fileName = "BoosterConfig")]
    public class BoosterConfig : ScriptableObject
    {
        public BoosterTypeId BoosterTypeId;
        public List<EffectSetup> EffectSetups;
        public List<StatusSetup> StatusSetups;
        public float RadiusToCollectTargets = 0.5f;
        public float LifeTime = 10f;
    }
}