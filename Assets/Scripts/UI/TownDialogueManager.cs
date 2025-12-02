using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using MerchantsRoad.Core;

namespace MerchantsRoad.UI
{
    public class TownDialogueManager : MonoBehaviour
    {
        [Header("UI")]
        public GameObject dialoguePanel;
        public TMP_Text npcText;
        public TMP_InputField playerInput;
        public GameObject closeButton;

        [Header("Ollama")]
        public string modelName = "llama3";
        public string ollamaUrl = "http://localhost:11434/api/generate";

        private bool isTalking = false;
        private string _currentPrompt;

        // Town Prompts
        private const string PROMPT_OAKFIELD = "Oakfield is a prosperous town nestled in a fertile valley. Its market square is always bustling with the trade of grain, its most valuable commodity. In the background, a large watermill works tirelessly, processing the golden harvest from the surrounding fields. You are a friendly merchant in Oakfield.";
        private const string PROMPT_IRONFORGE = "Ironforge is a town built on industry, known far and wide for its cheap and high-quality iron. The air in the market is thick with the smoke of forges, and the clanging of hammers on anvils is the town's constant rhythm. Blacksmiths display their wares, from simple tools to fine weapons, directly from their workshops. You are a gruff but honest blacksmith in Ironforge.";
        private const string PROMPT_RIVERMERE = "Rivermere is a vibrant trading hub located on a major river. Its market is a feast for the senses, filled with the colors and aromas of exotic spices brought in by ships from distant lands. The market square is next to the river, where a trading vessel is docked, unloading its precious cargo. You are a worldly spice trader in Rivermere.";

        void Start()
        {
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);

            if (playerInput != null)
            {
                playerInput.gameObject.SetActive(false);
                playerInput.onSubmit.AddListener(OnInputSubmit);
            }

            if (closeButton != null)
                closeButton.SetActive(false);
        }

        public void OpenDialogue(TownId town)
        {
            if (dialoguePanel == null) return;

            // Set Prompt based on Town
            switch (town)
            {
                case TownId.Oakfield:
                    _currentPrompt = PROMPT_OAKFIELD;
                    break;
                case TownId.Ironforge:
                    _currentPrompt = PROMPT_IRONFORGE;
                    break;
                case TownId.Rivermere:
                    _currentPrompt = PROMPT_RIVERMERE;
                    break;
                default:
                    _currentPrompt = "You are a generic NPC.";
                    break;
            }

            isTalking = true;
            dialoguePanel.SetActive(true);
            
            if (npcText != null)
                npcText.text = "Greetings! How can I help you today?";
            
            if (playerInput != null)
            {
                playerInput.gameObject.SetActive(true);
                playerInput.text = "";
                playerInput.ActivateInputField();
            }

            if (closeButton != null)
                closeButton.SetActive(true);
        }

        public void CloseDialogue()
        {
            isTalking = false;
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
                
            if (playerInput != null)
                playerInput.gameObject.SetActive(false);

            if (closeButton != null)
                closeButton.SetActive(false);
        }

        private void OnInputSubmit(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                OnSendClicked();
            }
        }

        public void OnSendClicked()
        {
            if (playerInput == null) return;
            if (string.IsNullOrWhiteSpace(playerInput.text)) return;
            
            string userMessage = playerInput.text;
            playerInput.text = "";
            
            if (npcText != null)
                npcText.text = "Thinking...";

            StartCoroutine(SendToOllamaCoroutine(userMessage));
        }

        private IEnumerator SendToOllamaCoroutine(string userMessage)
        {
            string fullPrompt = _currentPrompt + "\nReply in 1–2 short sentences.\nPlayer: " + userMessage;

            var requestData = new OllamaRequest
            {
                model = modelName,
                prompt = fullPrompt,
                stream = false
            };

            string jsonBody = JsonUtility.ToJson(requestData);

            using (UnityWebRequest req = new UnityWebRequest(ollamaUrl, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
                req.uploadHandler = new UploadHandlerRaw(bodyRaw);
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");

                yield return req.SendWebRequest();

                if (req.result == UnityWebRequest.Result.Success)
                {
                    string responseJson = req.downloadHandler.text;
                    OllamaResponse ollamaRes = JsonUtility.FromJson<OllamaResponse>(responseJson);
                    
                    if (npcText != null)
                        npcText.text = ollamaRes.response;
                }
                else
                {
                    if (npcText != null)
                        npcText.text = "The NPC is silent... (Error: " + req.error + ")";
                    Debug.LogError(req.error);
                }
            }
        }
    }

    [Serializable]
    public class OllamaRequest
    {
        public string model;
        public string prompt;
        public bool stream = false;
    }

    [Serializable]
    public class OllamaResponse
    {
        public string model;
        public string response;
        public bool done;
    }
}
