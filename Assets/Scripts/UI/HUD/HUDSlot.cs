using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MerchantsRoad.Core;

namespace MerchantsRoad.UI.HUD
{
    public class HUDSlot : MonoBehaviour
    {
        public GoodType goodType;
        public Image icon;
        public TextMeshProUGUI quantityText;
        public Button button;

        private HUDController _hudController;

        public void Setup(HUDController controller)
        {
            _hudController = controller;
            
            if (button == null)
                button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnClicked);
            }
        }

        public void SetQuantity(int amount)
        {
            if (quantityText != null)
                quantityText.text = amount.ToString();
        }

        private void OnClicked()
        {
            if (_hudController != null)
            {
                _hudController.ShowSellDialog(goodType);
            }
        }
    }
}