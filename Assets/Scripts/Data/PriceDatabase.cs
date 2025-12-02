using System.Collections.Generic;
using UnityEngine;
using MerchantsRoad.Core;

namespace MerchantsRoad.Data
{
    [CreateAssetMenu(menuName = "MerchantsRoad/PriceDatabase")]
    public class PriceDatabase : ScriptableObject
    {
        [System.Serializable]
        public class TownPriceEntry
        {
            public TownId town;
            public int grainPrice;
            public int ironPrice;
            public int spicesPrice;
            public int clothPrice;
        }

        [System.Serializable]
        public class TravelCostEntry
        {
            public TownId fromTown;
            public TownId toTown;
            public int cost;
        }

        public List<TownPriceEntry> townPrices = new List<TownPriceEntry>();
        public List<TravelCostEntry> travelCosts = new List<TravelCostEntry>();

        public int GetPrice(TownId _town, GoodType good)
        {
            foreach (TownPriceEntry entry in townPrices)
            {
                if (entry.town == _town)
                {
                    switch (good)
                    {
                        case GoodType.Grain: return entry.grainPrice;
                        case GoodType.Iron: return entry.ironPrice;
                        case GoodType.Spices: return entry.spicesPrice;
                        case GoodType.Cloth: return entry.clothPrice;
                    }
                }
            }
            return -1;
        }

        public int GetTravelCost(TownId from, TownId to)
        {
            foreach (TravelCostEntry entry in travelCosts)
            {
                if (entry.fromTown == from && entry.toTown == to)
                {
                    return entry.cost;
                }
            }
            return -1;
        }
    }
}