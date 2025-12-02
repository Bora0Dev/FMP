using UnityEngine;
using MerchantsRoad.Core;
using MerchantsRoad.Data;
using MerchantsRoad.UI.HUD;
using MerchantsRoad.World;

namespace MerchantsRoad.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        [Header("References")]
        public PriceDatabase priceDatabase;
        public HUDController hudController;
        public TownBackgroundController townBackgroundController;

        [Header("Player State")]
        public TownId currentTown = TownId.Oakfield;
        public int gold = 100;

        [Header("Cart Settings")]
        public int cartCapacityMax = 20;

        // New: inventory system handles inventory and capacity logic
        private InventorySystem _inventorySystem;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create the inventory system
            _inventorySystem = new InventorySystem();
        }

        private void Start()
        {
            if (hudController == null)
            {
                hudController = FindAnyObjectByType<HUDController>();
                if (hudController == null)
                    Debug.LogError("GameManager: Could not find HUDController in the scene!");
                else
                    Debug.Log("GameManager: Found HUDController automatically.");
            }

            if (hudController != null)
                hudController.RefreshHUD();

            if (townBackgroundController != null)
                townBackgroundController.UpdateTownBackground(currentTown);
        }

        // -------------------- CAPACITY --------------------

        public int GetCapacityUsed()
        {
            return _inventorySystem.GetCapacityUsed();
        }

        public bool HasCartSpace(GoodType good, int quantity = 1)
        {
            int used = _inventorySystem.GetCapacityUsed();
            int unitSize = _inventorySystem.GetUnitSize(good);
            int needed = unitSize * quantity;

            return (used + needed) <= cartCapacityMax;
        }

        // -------------------- GOLD --------------------

        public bool HasEnoughGold(int amount)
        {
            return gold >= amount;
        }

        public void AddGold(int amount)
        {
            gold += amount;

            if (hudController != null)
                hudController.RefreshHUD();
        }

        public void SpendGold(int amount)
        {
            gold -= amount;

            if (hudController != null)
                hudController.RefreshHUD();
        }

        // -------------------- BUY / SELL --------------------

        public bool TryBuy(GoodType good)
        {
            int price = priceDatabase.GetPrice(currentTown, good);

            if (!HasEnoughGold(price))
                return false;

            if (!HasCartSpace(good))
                return false;

            SpendGold(price);
            _inventorySystem.Add(good, 1);

            if (hudController != null)
                hudController.RefreshHUD();

            return true;
        }

        public bool TrySell(GoodType good)
        {
            bool removed = _inventorySystem.TryRemove(good, 1);
            if (!removed)
                return false;

            int price = priceDatabase.GetPrice(currentTown, good);
            AddGold(price);

            if (hudController != null)
                hudController.RefreshHUD();

            return true;
        }

        // -------------------- TRAVEL --------------------

        public bool TryTravelTo(TownId destination)
        {
            if (destination == currentTown)
                return false;

            int cost = priceDatabase.GetTravelCost(currentTown, destination);

            if (!HasEnoughGold(cost))
                return false;

            SpendGold(cost);
            currentTown = destination;

            if (townBackgroundController != null)
                townBackgroundController.UpdateTownBackground(currentTown);

            if (hudController != null)
                hudController.RefreshHUD();

            return true;
        }

        // -------------------- INVENTORY ACCESS FOR HUD --------------------

        public int GetInventoryAmount(GoodType good)
        {
            return _inventorySystem.GetQuantity(good);
        }
    }
}
