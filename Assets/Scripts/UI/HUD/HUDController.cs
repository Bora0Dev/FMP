using UnityEngine;
using TMPro;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        public TextMeshProUGUI GoldText;
        public TextMeshProUGUI CapacityText;

        public HUDSlot GrainSlot;
        public HUDSlot IronSlot;
        public HUDSlot SpicesSlot;
        public HUDSlot ClothSlot;

        [Header("Dialogs")]
        public SellDialog sellDialog;

        private void Start()
        {
            // Setup slots
            if (GrainSlot != null) GrainSlot.Setup(this);
            if (IronSlot != null) IronSlot.Setup(this);
            if (SpicesSlot != null) SpicesSlot.Setup(this);
            if (ClothSlot != null) ClothSlot.Setup(this);
        }

        public void ShowSellDialog(GoodType good)
        {
            if (sellDialog != null)
            {
                sellDialog.Open(good);
            }
            else
            {
                Debug.LogWarning("HUDController: SellDialog is not assigned!");
            }
        }

        public void RefreshHUD()
        {
            if (GameManager.instance == null)
            {
                Debug.LogError("HUDController: GameManager instance is null!");
                return;
            }

            Debug.Log("HUDController: Refreshing HUD...");

            if (GoldText == null) Debug.LogError("HUDController: GoldText reference is missing!");
            else UpdateGold(GameManager.instance.gold);

            UpdateSlot(GrainSlot, GoodType.Grain);
            UpdateSlot(IronSlot, GoodType.Iron);
            UpdateSlot(SpicesSlot, GoodType.Spices);
            UpdateSlot(ClothSlot, GoodType.Cloth);

            if (CapacityText == null) Debug.LogError("HUDController: CapacityText reference is missing!");
            else
            {
                int used = GameManager.instance.GetCapacityUsed();
                int max = GameManager.instance.cartCapacityMax;
                CapacityText.text = "Capacity: " + used + " / " + max;
            }
        }

        private void UpdateGold(int gold)
        {
            GoldText.text = "Gold: " + gold;
        }

        private void UpdateSlot(HUDSlot slot, GoodType good)
        {
            if (slot == null)
                return;

            int amount = GameManager.instance.GetInventoryAmount(good);
            slot.SetQuantity(amount);
        }
    }
}