using System;

namespace Code.Meta.UI.GoldHolder.Service
{
    public interface IStorageUIService
    {
        event Action GoldChangedEvent;
        float CurrentGold { get; }
        void UpdateCurrentGold(float gold);
        void Cleanup();
    }
}