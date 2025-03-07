using System;
using System.Collections.Generic;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using Code.Meta.UI.Shop.Items;
using Code.Meta.UI.Shop.UIFactory;

namespace Code.Meta.UI.Shop.Service
{
    public class ShopUIService : IShopUIService
    {
        private Dictionary<ShopItemId, ShopItemConfig> _availableItems = new();
        private List<ShopItemId> _purchasedItems = new();
        
        private readonly IStaticDataService _staticDataService;
        private readonly IWindowService _windowService;
        private readonly IShopUIFactory _shopUIFactory;

        public ShopUIService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public event Action ShopChangedEvent; 
        
        public List<ShopItemConfig> GetAvailableShopItems() => new(_availableItems.Values);
        
        public void UpdatePurchasedItems(IEnumerable<ShopItemId> purchasedItems)
        {
            _purchasedItems.AddRange(purchasedItems);
            
            RefreshAvailableItems();
        }

        public void Cleanup()
        {
            _purchasedItems.Clear();
            _availableItems.Clear();
            
            ShopChangedEvent = null;
        }
        
        private void RefreshAvailableItems()
        {
            foreach (ShopItemConfig shopItemConfig in _staticDataService.GetShopItemConfigs())
            {
                if (!_purchasedItems.Contains(shopItemConfig.ShopItemId))
                {
                    _availableItems.Add(shopItemConfig.ShopItemId, shopItemConfig);
                }
            }
            
            ShopChangedEvent?.Invoke();
        }
    }
}