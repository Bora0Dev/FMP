using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.UI
{
    public enum MarketMode
    {
        Buy,
        Sell
    }

    public class TradeDialog : MonoBehaviour
    {
        public System.Action OnClose;

        [Header("UI References")]
        public GameObject dialogPanel;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI infoText; // Shows Cost or Value
        public Slider quantitySlider;
        public TextMeshProUGUI quantityText;
        public Button confirmButton;
        public Button cancelButton;

        [Header("Tabs")]
        public Button buyTabButton;
        public Button sellTabButton;
        public Image buyTabImage;
        public Image sellTabImage;
        public Color activeTabColor = Color.white;
        public Color inactiveTabColor = Color.gray;

        private GoodType _goodType;
        private int _unitPrice;
        private MarketMode _currentMode;

        private void Start()
        {
            if (dialogPanel != null)
                dialogPanel.SetActive(false);

            if (quantitySlider != null)
                quantitySlider.onValueChanged.AddListener(OnSliderValueChanged);

            if (confirmButton != null)
                confirmButton.onClick.AddListener(OnConfirmClicked);

            if (cancelButton != null)
                cancelButton.onClick.AddListener(CloseDialog);

            if (buyTabButton != null)
                buyTabButton.onClick.AddListener(() => SetMode(MarketMode.Buy));

            if (sellTabButton != null)
                sellTabButton.onClick.AddListener(() => SetMode(MarketMode.Sell));
        }

        public void Open(GoodType good)
        {
            _goodType = good;
            
            // Get current price
            TownId currentTown = GameManager.instance.currentTown;
            _unitPrice = GameManager.instance.priceDatabase.GetPrice(currentTown, good);

            if (dialogPanel != null)
                dialogPanel.SetActive(true);

            if (titleText != null)
                titleText.text = good.ToString();

            // Default to Buy mode
            SetMode(MarketMode.Buy);
        }

        private void SetMode(MarketMode mode)
        {
            _currentMode = mode;

            // Update Tab Visuals
            if (buyTabImage != null) buyTabImage.color = (mode == MarketMode.Buy) ? activeTabColor : inactiveTabColor;
            if (sellTabImage != null) sellTabImage.color = (mode == MarketMode.Sell) ? activeTabColor : inactiveTabColor;

            // Update Slider Max
            UpdateSliderRange();

            // Reset Slider Value
            if (quantitySlider != null)
            {
                if (quantitySlider.maxValue > 0)
                    quantitySlider.value = 1;
                else
                    quantitySlider.value = 0;
            }

            UpdateUI();
        }

        private void UpdateSliderRange()
        {
            if (quantitySlider == null) return;

            int maxQuantity = 0;

            if (_currentMode == MarketMode.Buy)
            {
                // Buy Logic: Min(Affordable, Space)
                int maxAffordable = (_unitPrice > 0) ? GameManager.instance.gold / _unitPrice : 0;
                int currentCapacity = GameManager.instance.GetCapacityUsed();
                int maxCapacity = GameManager.instance.cartCapacityMax;
                int availableSpace = maxCapacity - currentCapacity;
                maxQuantity = Mathf.Min(maxAffordable, availableSpace);
            }
            else
            {
                // Sell Logic: Inventory Amount
                maxQuantity = GameManager.instance.GetInventoryAmount(_goodType);
            }

            quantitySlider.minValue = 0;
            quantitySlider.maxValue = Mathf.Max(0, maxQuantity);
        }

        private void OnSliderValueChanged(float value)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            int qty = (int)quantitySlider.value;
            int total = qty * _unitPrice;

            if (quantityText != null)
                quantityText.text = qty.ToString();

            if (infoText != null)
            {
                if (_currentMode == MarketMode.Buy)
                    infoText.text = "Cost: " + total + " G";
                else
                    infoText.text = "Value: " + total + " G";
            }
            
            if (confirmButton != null)
            {
                confirmButton.interactable = (qty > 0);
                
                TextMeshProUGUI btnText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();
                if (btnText != null)
                {
                    btnText.text = (_currentMode == MarketMode.Buy) ? "Buy" : "Sell";
                }
            }
        }

        private void OnConfirmClicked()
        {
            int qty = (int)quantitySlider.value;
            if (qty <= 0) return;

            bool success = true;

            for (int i = 0; i < qty; i++)
            {
                if (_currentMode == MarketMode.Buy)
                {
                    if (!GameManager.instance.TryBuy(_goodType))
                    {
                        success = false;
                        break;
                    }
                }
                else
                {
                    if (!GameManager.instance.TrySell(_goodType))
                    {
                        success = false;
                        break;
                    }
                }
            }

            if (!success)
            {
                Debug.LogError("Transaction failed partially or completely.");
            }

            CloseDialog();
        }

        public void CloseDialog()
        {
            if (dialogPanel != null)
                dialogPanel.SetActive(false);
            
            OnClose?.Invoke();
        }
    }
}
