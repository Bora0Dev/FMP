using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.UI
{
    public class BuyDialog : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject dialogPanel;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI totalCostText;
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

        public void Open(GoodType good, int price)
        {
            _goodType = good;
            _unitPrice = price;

            if (dialogPanel != null)
                dialogPanel.SetActive(true);

            if (titleText != null)
                titleText.text = "Buy " + good;

            // Calculate max affordable/carryable
            int maxAffordable = GameManager.instance.gold / price;
            
            int currentCapacity = GameManager.instance.GetCapacityUsed();
            int maxCapacity = GameManager.instance.cartCapacityMax;
            int availableSpace = maxCapacity - currentCapacity;
            // Assuming 1 unit size for now, as per InventorySystem
            int maxCarryable = availableSpace; 

            int maxQuantity = Mathf.Min(maxAffordable, maxCarryable);
            
            // Ensure at least 0
            maxQuantity = Mathf.Max(0, maxQuantity);

            if (quantitySlider != null)
            {
                quantitySlider.minValue = 0;
                quantitySlider.maxValue = maxQuantity;
                quantitySlider.value = 0; // Reset to 0 or 1
                
                // If we can buy at least 1, default to 1? Or 0? Let's do 1 if possible.
                if (maxQuantity > 0)
                    quantitySlider.value = 1;
                else
                    quantitySlider.value = 0;
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

            if (totalCostText != null)
                totalCostText.text = "Total: " + total + " G";
            
            if (confirmButton != null)
                confirmButton.interactable = (qty > 0);
        }

        private void OnConfirmClicked()
        {
            int qty = (int)quantitySlider.value;
            if (qty <= 0) return;

            // Perform buy loop
            for (int i = 0; i < qty; i++)
            {
                bool success = GameManager.instance.TryBuy(_goodType);
                if (!success)
                {
                    Debug.LogError("Failed to buy good even though calculations said we could!");
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
