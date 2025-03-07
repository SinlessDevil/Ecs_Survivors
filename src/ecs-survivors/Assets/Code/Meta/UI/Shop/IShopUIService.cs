using System;
using System.Collections.Generic;
using Code.Meta.UI.Shop.Items;

namespace Code.Meta.UI.Shop.Service
{
    public interface IShopUIService
    {
        event Action ShopChangedEvent;
        List<ShopItemConfig> GetAvailableShopItems();
        void UpdatePurchasedItems(IEnumerable<ShopItemId> purchasedItems);
        void Cleanup();
    }
}