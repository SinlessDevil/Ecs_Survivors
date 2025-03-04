using System.Collections.Generic;
using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using UnityEngine;

namespace Code.Gameplay.Features.Enchants.Configs
{
    [CreateAssetMenu(menuName = "ECS Survivors/Enchant", fileName = "EnchantConfig")]
    public class EnchantConfig : ScriptableObject
    {
        public EnchantTypeId EnchantTypeId;
        public List<EffectSetup> EffectSetups;
        public List<StatusSetup> StatusSetups;
    }
}