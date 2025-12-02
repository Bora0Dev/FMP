using UnityEngine;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.UI.Map
{
    public class MapController : MonoBehaviour
    {
        public GameObject MapPanel;

        public TownButton OakfieldButton;
        public TownButton IronforgeButton;
        public TownButton RivermereButton;

        [Header("Tooltip")]
        [Tooltip("Tooltip prefab (with Tooltip script attached).")]
        public Tooltip TooltipPrefab;

        private Tooltip _activeTooltip;
     

        public void OpenMap()
        {
            MapPanel.SetActive(true);
        }

        public void CloseMap()
        {
            MapPanel.SetActive(false);
            DestroyActiveTooltip();
        }

        private void DestroyActiveTooltip()
        {
            if (_activeTooltip != null)
            {
                Destroy(_activeTooltip.gameObject);
                _activeTooltip = null;
            }
        }

        public void ShowTownHover(TownId targetTown, RectTransform buttonRect)
        {
            if (TooltipPrefab == null || GameManager.instance == null || GameManager.instance.priceDatabase == null)
                return;

            TownId current = GameManager.instance.currentTown;
            int cost = GameManager.instance.priceDatabase.GetTravelCost(current, targetTown);
            int gold = GameManager.instance.gold;

            string message = "";

            // Always start with town name
            message += targetTown + "\n";

            if (targetTown == current)
            {
                // Same town
                message += "You are here\n";
                message += "Your Gold: " + gold + "g";
            }
            else if (cost < 0)
            {
                // No travel route
                message += "No route found\n";
                message += "Your Gold: " + gold + "g";
            }
            else
            {
                // Other town with valid cost
                message += "Travel Cost: " + cost + "g\n";
                message += "Your Gold: " + gold + "g";

                if (gold < cost)
                {
                    message += "\nNot enough gold!";
                }
            }

            // Spawn tooltip if needed
            if (_activeTooltip == null)
            {
                Transform parent = buttonRect.parent;
                _activeTooltip = Instantiate(TooltipPrefab, parent);
            }

            _activeTooltip.SetText(message);

            // Position the tooltip just under the hovered button
            RectTransform tooltipRect = _activeTooltip.transform as RectTransform;
            tooltipRect.localScale = Vector3.one;

            Vector2 pos = buttonRect.anchoredPosition;
            float gap = 10f;
            float offsetY = buttonRect.rect.height * 0.5f + tooltipRect.rect.height * 0.5f + gap;

            pos.y -= offsetY;
            tooltipRect.anchoredPosition = pos;
        }

        // Called from TownButton when mouse exits
        public void HideHover()
        {
            DestroyActiveTooltip();
        }

        public void RequestTravel(TownId town)
        {
            bool success = GameManager.instance.TryTravelTo(town);
            if (success)
            {
                CloseMap();
            }
        }

        private void Start()
        {
            // This is safe: everything exists when Start runs.
            if (OakfieldButton != null) OakfieldButton.Map = this;
            if (IronforgeButton != null) IronforgeButton.Map = this;
            if (RivermereButton != null) RivermereButton.Map = this;
        }
    }
}