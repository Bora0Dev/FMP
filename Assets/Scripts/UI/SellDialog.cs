using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.UI
{
    public class SellDialog : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject dialogPanel;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI totalValueText;
        public Slider quantitySlider;
        public TextMeshProUGUI quantityText;
        public Button confirmButton;
        public Button cancelButton;

        private GoodType _goodType;
        private int _unitPrice;

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
        }

        public void Open(GoodType good)
        {
            _goodType = good;
            
            // Get current price from GameManager/PriceDatabase
            TownId currentTown = GameManager.instance.currentTown;
            _unitPrice = GameManager.instance.priceDatabase.GetPrice(currentTown, good);

            if (dialogPanel != null)
                dialogPanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Sell " + good;

            // Max quantity is what we have in inventory
            int maxQuantity = GameManager.instance.GetInventoryAmount(good);

            if (quantitySlider != null)
            {
                quantitySlider.minValue = 0;
                quantitySlider.maxValue = maxQuantity;
                quantitySlider.value = 0;
                
                if (maxQuantity > 0)
                    quantitySlider.value = 1;
            }

            UpdateUI();
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

            if (totalValueText != null)
                totalValueText.text = "Value: " + total + " G";
            
            if (confirmButton != null)
                confirmButton.interactable = (qty > 0);
        }

        private void OnConfirmClicked()
        {
            int qty = (int)quantitySlider.value;
            if (qty <= 0) return;

            // Perform sell loop
            for (int i = 0; i < qty; i++)
            {
                bool success = GameManager.instance.TrySell(_goodType);
                if (!success)
                {
                    Debug.LogError("Failed to sell good!");
                    break;
                }
            }

            CloseDialog();
        }

        public void CloseDialog()
        {
            if (dialogPanel != null)
                dialogPanel.SetActive(false);
        }
    }
}
