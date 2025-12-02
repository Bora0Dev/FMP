using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using MerchantsRoad.Core;
using MerchantsRoad.Managers;

namespace MerchantsRoad.UI
{
    public class TownMarketUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject marketPanel;
        public Button closeButton;
        
        [Header("Town Buttons")]
        public Button openMarketButton; // Generic button to open current town's market
        public Button oakfieldButton;
        public Button ironforgeButton;
        public Button rivermereButton;

        [Header("Slots")]
        public MarketItemSlot[] slots; 

        [Header("Dialogs")]
        public TradeDialog tradeDialog;
        public TownDialogueManager dialogueManager;

        [Header("NPC Interaction")]
        public Button talkButton;

        // Store the original interactable state of buttons to restore them later
        private Dictionary<Button, bool> _buttonStates = new Dictionary<Button, bool>();

        private void Start()
        {
            if (marketPanel != null)
                marketPanel.SetActive(false);

            if (closeButton != null)
                closeButton.onClick.AddListener(CloseMarket);

            if (openMarketButton != null)
                openMarketButton.onClick.AddListener(() => OpenMarket(GameManager.instance.currentTown));

            if (oakfieldButton != null)
                oakfieldButton.onClick.AddListener(() => OpenMarket(TownId.Oakfield));

            if (ironforgeButton != null)
                ironforgeButton.onClick.AddListener(() => OpenMarket(TownId.Ironforge));

            if (rivermereButton != null)
                rivermereButton.onClick.AddListener(() => OpenMarket(TownId.Rivermere));

            if (talkButton != null)
                talkButton.onClick.AddListener(OpenDialogue);
        }

        private void OpenDialogue()
        {
            if (dialogueManager != null)
            {
                dialogueManager.OpenDialogue(GameManager.instance.currentTown);
            }
            else
            {
                Debug.LogError("[TownMarketUI] Dialogue Manager reference is missing!");
            }
        }

        public void OpenMarket(TownId town)
        {
            if (marketPanel != null)
                marketPanel.SetActive(true);

            UpdatePrices(town);

            // Disable other buttons
            DisableOtherButtons();
        }

        public void CloseMarket()
        {
            Debug.Log("[TownMarketUI] CloseMarket called.");
            if (marketPanel != null)
                marketPanel.SetActive(false);
            
            if (tradeDialog != null)
            {
                // Prevent the OnClose event from reopening the market panel
                tradeDialog.OnClose = null;
                tradeDialog.CloseDialog();
            }

            // Restore buttons
            RestoreButtonStates();
        }

        private void UpdatePrices(TownId town)
        {
            if (GameManager.instance == null || GameManager.instance.priceDatabase == null)
                return;

            GoodType[] goods = (GoodType[])System.Enum.GetValues(typeof(GoodType));

            for (int i = 0; i < slots.Length; i++)
            {
                if (i >= goods.Length) break;

                GoodType good = goods[i];
                int price = GameManager.instance.priceDatabase.GetPrice(town, good);

                if (slots[i] != null)
                {
                    slots[i].Setup(good, price, this);
                }
            }
        }

        public void ShowTradeDialog(GoodType good)
        {
            Debug.Log($"[TownMarketUI] Requesting to open Trade Dialog for {good}");
            if (tradeDialog != null)
            {
                // Hide the market panel
                if (marketPanel != null)
                    marketPanel.SetActive(false);

                // Subscribe to close event to reopen market
                tradeDialog.OnClose = () => 
                {
                    if (marketPanel != null)
                        marketPanel.SetActive(true);
                };

                tradeDialog.Open(good);
            }
            else
            {
                Debug.LogError("[TownMarketUI] TradeDialog reference is missing!");
            }
        }

        private void DisableOtherButtons()
        {
            Debug.Log("[TownMarketUI] DisableOtherButtons called.");
            _buttonStates.Clear();

            // Find all buttons in the scene
            Button[] allButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);

            foreach (Button btn in allButtons)
            {
                // Skip the close button
                if (btn == closeButton)
                {
                    Debug.Log("[TownMarketUI] Skipping Close Button (explicit check).");
                    continue;
                }

                // Skip buttons that are children of the market panel
                if (marketPanel != null && btn.transform.IsChildOf(marketPanel.transform))
                    continue;

                // Skip buttons that are children of the trade dialog (if it exists and is separate from market panel)
                if (tradeDialog != null && btn.transform.IsChildOf(tradeDialog.transform))
                    continue;

                // Skip buttons that are children of the dialogue panel
                if (dialogueManager != null && dialogueManager.dialoguePanel != null && btn.transform.IsChildOf(dialogueManager.dialoguePanel.transform))
                    continue;

                // Save current state
                _buttonStates[btn] = btn.interactable;

                // Disable interaction
                btn.interactable = false;
            }
            Debug.Log($"[TownMarketUI] Disabled {_buttonStates.Count} buttons.");
        }

        private void RestoreButtonStates()
        {
            Debug.Log($"[TownMarketUI] Restoring {_buttonStates.Count} buttons.");
            foreach (KeyValuePair<Button, bool> entry in _buttonStates)
            {
                if (entry.Key != null)
                {
                    entry.Key.interactable = entry.Value;
                }
            }
            _buttonStates.Clear();
        }
    }
}
