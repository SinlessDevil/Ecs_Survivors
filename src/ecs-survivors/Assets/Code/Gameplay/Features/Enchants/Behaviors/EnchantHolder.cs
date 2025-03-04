using System.Collections.Generic;
using Code.Gameplay.Features.Enchants.UIFactories;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Enchants.Behaviors
{
    public class EnchantHolder : MonoBehaviour
    {
        public Transform EnchantLayout;

        private List<Enchant> _enchants = new List<Enchant>();
        private IEnchantUIFactory _enchantUIFactory;
        
        [Inject]
        private void Construct(IEnchantUIFactory enchantUIFactory)
        {
            _enchantUIFactory = enchantUIFactory;
        }
        
        public void AddEnchant(EnchantTypeId enchantTypeId)
        {
            //Debug.Log($"{enchantTypeId} added to holder");
            
            if (EnchantAlreadyHeld(enchantTypeId))
                return;
            
            Enchant enchant = _enchantUIFactory.CreateEnchant(EnchantLayout, enchantTypeId);
            _enchants.Add(enchant);
        }

        public void RemoveEnchant(EnchantTypeId enchantTypeId)
        {
            //Debug.Log($"{enchantTypeId} added to holder");
            
            Enchant enchant = _enchants.Find(x => x.EnchantTypeId == enchantTypeId);

            if (enchant != null)
            {
                _enchants.Remove(enchant);
                Destroy(enchant.gameObject);
            }
        }
        
        private bool EnchantAlreadyHeld(EnchantTypeId enchantTypeId) =>
        _enchants.Find(x => x.EnchantTypeId == enchantTypeId) != null;
    }
}