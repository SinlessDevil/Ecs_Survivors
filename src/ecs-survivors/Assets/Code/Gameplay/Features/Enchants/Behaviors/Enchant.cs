using Code.Gameplay.Features.Enchants.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Enchants.Behaviors
{
    public class Enchant : MonoBehaviour
    {
        public Image Icon;
        public EnchantTypeId EnchantTypeId;

        public void Set(EnchantConfig config)
        {
            Icon.sprite = config.Icon;
            EnchantTypeId = config.EnchantTypeId;
        }
    }
}