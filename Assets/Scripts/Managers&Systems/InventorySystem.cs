using System.Collections.Generic;
using MerchantsRoad.Core;

namespace MerchantsRoad.Managers
{
    
    // Handles the player's inventory and cart capacity logic.
    // GameManager uses this to store quantities and calculate used capacity.
    
    public class InventorySystem
    {
        // How many of each good the player has
        private Dictionary<GoodType, int> _inventory = new Dictionary<GoodType, int>();

        // How much cart space one unit of each good uses (int for now)
        private Dictionary<GoodType, int> _unitSizes = new Dictionary<GoodType, int>();

        public InventorySystem()
        {
            InitializeInventory();
            InitializeUnitSizes();
        }

        private void InitializeInventory()
        {
            foreach (GoodType good in System.Enum.GetValues(typeof(GoodType)))
            {
                _inventory[good] = 0;
            }
        }

        private void InitializeUnitSizes()
        {
            // All 1 for now, but you can change these later
            _unitSizes[GoodType.Grain] = 1;
            _unitSizes[GoodType.Iron] = 1;
            _unitSizes[GoodType.Spices] = 1;
            _unitSizes[GoodType.Cloth] = 1;
        }

        /// <summary>
        /// Returns how many units of this good are currently in inventory.
        /// </summary>
        public int GetQuantity(GoodType good)
        {
            return _inventory[good];
        }

        /// <summary>
        /// Adds quantity of a good to the inventory.
        /// </summary>
        public void Add(GoodType good, int quantity)
        {
            _inventory[good] += quantity;
        }

        /// <summary>
        /// Tries to remove quantity of a good. Returns false if not enough.
        /// </summary>
        public bool TryRemove(GoodType good, int quantity)
        {
            if (_inventory[good] < quantity)
            {
                return false;
            }

            _inventory[good] -= quantity;
            return true;
        }

        /// <summary>
        /// How much capacity does a single unit of this good take.
        /// </summary>
        public int GetUnitSize(GoodType good)
        {
            return _unitSizes[good];
        }

        /// <summary>
        /// Returns all capacity currently used by inventory.
        /// </summary>
        public int GetCapacityUsed()
        {
            int used = 0;

            foreach (KeyValuePair<GoodType, int> kvp in _inventory)
            {
                GoodType good = kvp.Key;
                int qty = kvp.Value;
                int size = _unitSizes[good];

                used += qty * size;
            }

            return used;
        }
    }
}
