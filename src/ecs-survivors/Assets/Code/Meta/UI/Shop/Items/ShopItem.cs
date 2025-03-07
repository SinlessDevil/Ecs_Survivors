using Code.Meta.UI.GoldHolder.Service;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Code.Meta.UI.Shop.Items
{
    public class ShopItem : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI PriceText;
        public TextMeshProUGUI DurationText;
        public TextMeshProUGUI BoostText;
        public Button BuyButton;
        public CanvasGroup CanvasBuyGroup;
        [Space(10)]
        public Color EnoughGoldColor = Color.white;
        public Color NotEnoughGoldColor = Color.red;
        
        private bool _isAvailable;
        private int _price;
        private float _currentGold;
        
        private IStorageUIService _storageUIService;

        [Inject]
        private void Construct(IStorageUIService storageUIService)
        {
            _storageUIService = storageUIService;
        }
        
        public void Setup(ShopItemConfig config)
        {
            Icon.sprite = config.Icon;
            PriceText.text = config.Price.ToString();
            DurationText.text = config.Duration.ToString("m'm 's's'");
            BoostText.text = config.Boost.ToString("+0%");
            
            _price = config.Price;
        }

        private void Start()
        {
            _storageUIService.GoldChangedEvent += OnUpdatePriceThreshold;
            
            OnUpdatePriceThreshold();
        }

        private void OnDestroy()
        {
            _storageUIService.GoldChangedEvent -= OnUpdatePriceThreshold;
        }
        
        public void OnUpdateAvailability(bool itemsCanBeBought)
        {
            _isAvailable = itemsCanBeBought;
            CanvasBuyGroup.alpha = _isAvailable ? 1 : 0;
            
            RefreshBuyButton();
        }

        private void OnUpdatePriceThreshold()
        {
            _currentGold = _storageUIService.CurrentGold;
            
            PriceText.color = EnoughGold() ? EnoughGoldColor : NotEnoughGoldColor;
            
            RefreshBuyButton();
        }

        private bool EnoughGold() => _currentGold >= _price;

        private void RefreshBuyButton() => BuyButton.interactable = EnoughGold() && _isAvailable;
    }
}