using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MerchantsRoad.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("UI References")]
        [Tooltip("The panel that contains the inventory UI.")]
        public GameObject inventoryPanel;

        [Tooltip("The button that opens the inventory.")]
        public Button openButton;

        [Tooltip("The button that closes the inventory.")]
        public Button closeButton;

        // Store the original interactable state of buttons to restore them later
        private Dictionary<Button, bool> _buttonStates = new Dictionary<Button, bool>();

        private void Start()
        {
            // Ensure the panel is closed at start
            if (inventoryPanel != null)
                inventoryPanel.SetActive(false);

            // Add listeners to buttons
            if (openButton != null)
                openButton.onClick.AddListener(OpenInventory);

            if (closeButton != null)
                closeButton.onClick.AddListener(CloseInventory);
        }

        public void OpenInventory()
        {
            if (inventoryPanel != null)
                inventoryPanel.SetActive(true);

            // Freeze the game
            Time.timeScale = 0f;

            // Disable other buttons
            DisableOtherButtons();
        }

        public void CloseInventory()
        {
            if (inventoryPanel != null)
                inventoryPanel.SetActive(false);

            // Unfreeze the game
            Time.timeScale = 1f;

            // Restore buttons
            RestoreButtonStates();
        }

        private void DisableOtherButtons()
        {
            _buttonStates.Clear();

            // Find all buttons in the scene (including inactive ones if needed, but usually active is enough)
            Button[] allButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);

            foreach (Button btn in allButtons)
            {
                // Skip the close button, we need that to be clickable!
                if (btn == closeButton)
                    continue;

                // Skip buttons that are children of the inventory panel (like HUD slots)
                if (inventoryPanel != null && btn.transform.IsChildOf(inventoryPanel.transform))
                    continue;

                // Save current state
                _buttonStates[btn] = btn.interactable;

                // Disable interaction
                btn.interactable = false;
            }
        }

        private void RestoreButtonStates()
        {
            foreach (KeyValuePair<Button, bool> entry in _buttonStates)
            {
                if (entry.Key != null)
                {
                    entry.Key.interactable = entry.Value;
                }
            }
            _buttonStates.Clear();
        }

        private void OnDestroy()
        {
            // Clean up listeners
            if (openButton != null)
                openButton.onClick.RemoveListener(OpenInventory);

            if (closeButton != null)
                closeButton.onClick.RemoveListener(CloseInventory);
        }
    }
}
