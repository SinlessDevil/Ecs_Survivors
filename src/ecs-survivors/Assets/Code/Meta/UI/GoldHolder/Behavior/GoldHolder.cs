using Code.Meta.UI.GoldHolder.Service;
using UnityEngine;
using Zenject;
using TMPro;

namespace Code.Meta.UI.GoldHolder.Behavior
{
    public class GoldHolder : MonoBehaviour
    {
        public TextMeshProUGUI goldText;
        
        private IStorageUIService _storageUIService;

        [Inject]
        private void Construct(IStorageUIService storageUIService)
        {
            _storageUIService = storageUIService;
        }
        
        private void Start()
        {
            _storageUIService.GoldChangedEvent += OnGoldChanged;
            
            OnGoldChanged();
        }

        private void OnDestroy() =>
            _storageUIService.GoldChangedEvent -= OnGoldChanged;
        
        private void OnGoldChanged() =>
            goldText.text = _storageUIService.CurrentGold.ToString("0");
    }
}