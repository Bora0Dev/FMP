using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MerchantsRoad.Core;

namespace MerchantsRoad.UI
{
    public class MarketItemSlot : MonoBehaviour
    {
        [Header("UI References")]
        public Image iconImage;
        public Button actionButton; 

        private GoodType _goodType;
        private int _currentPrice;
        private TownMarketUI _marketUI;

        public void Setup(GoodType good, int price, TownMarketUI marketUI)
        {
            Debug.Log($"[MarketItemSlot] Setup called for {good}. Price: {price}");
            _goodType = good;
            _currentPrice = price;
            _marketUI = marketUI;

            if (actionButton != null)
            {
                // Reset text to "Trade" or just keep it as is (icon usually)
                TextMeshProUGUI btnText = actionButton.GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null)
                {
                    btnText.text = "Trade";
                }

                actionButton.onClick.RemoveAllListeners();
                actionButton.onClick.AddListener(OnSlotClicked);
            }
            else
            {
                Debug.LogError($"[MarketItemSlot] Action Button is missing on slot for {good}!");
            }
        }

        private void OnSlotClicked()
        {
            Debug.Log($"[MarketItemSlot] Clicked on {_goodType}");
            if (_marketUI != null)
            {
                _marketUI.ShowTradeDialog(_goodType);
            }
            else
            {
                Debug.LogError("[MarketItemSlot] MarketUI reference is null!");
            }
        }
    }
}
