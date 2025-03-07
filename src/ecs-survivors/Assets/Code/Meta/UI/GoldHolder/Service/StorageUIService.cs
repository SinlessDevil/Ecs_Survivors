using System;

namespace Code.Meta.UI.GoldHolder.Service
{
    public class StorageUIService : IStorageUIService
    {
        private float _currentGold;

        public event Action GoldChangedEvent;
        
        public float CurrentGold => _currentGold;

        public void UpdateCurrentGold(float gold)
        {
            if (Math.Abs(gold - _currentGold) > float.Epsilon)
            {
                _currentGold = gold;
                GoldChangedEvent?.Invoke();
            }
        }

        public void Cleanup()
        {
            _currentGold = 0f;
            
            GoldChangedEvent = null;
        }
    }
}