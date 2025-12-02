using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SurveyController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject surveyPanel;
    public TMP_InputField identityInput;
    public TMP_InputField commentsInput;
    public TMP_InputField bugReportInput;
    public TMP_InputField suggestionsInput;
    public Slider ratingSlider;
    public Toggle consentToggle;
    public TextMeshProUGUI ratingValueText;
    public Button submitButton;

    private void Start()
    {
        // Optional: Hide survey at start, show it later or via a menu
        // surveyPanel.SetActive(false);
        
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitPressed);
        }

        if (ratingSlider != null)
        {
            ratingSlider.onValueChanged.AddListener(OnRatingChanged);
            UpdateRatingText(ratingSlider.value);
        }
    }

    private void OnRatingChanged(float value)
    {
        UpdateRatingText(value);
    }

    private void UpdateRatingText(float value)
    {
        if (ratingValueText != null)
        {
            ratingValueText.text = value.ToString("0");
        }
    }

    public void ShowSurvey()
    {
        if (surveyPanel != null)
        {
            surveyPanel.SetActive(true);
        }
    }

    public void OnSubmitPressed()
    {
        if (TelemetryManager.Instance == null)
        {
            Debug.LogError("TelemetryManager instance not found!");
            return;
        }

        int rating = ratingSlider != null ? Mathf.RoundToInt(ratingSlider.value) : 0;
        string identity = identityInput != null ? identityInput.text : "Anonymous";
        string comments = commentsInput != null ? commentsInput.text : "";
        string bugReport = bugReportInput != null ? bugReportInput.text : "";
        string suggestions = suggestionsInput != null ? suggestionsInput.text : "";
        bool consent = consentToggle != null ? consentToggle.isOn : false;

        TelemetryManager.Instance.SubmitFeedback(identity, rating, comments, bugReport, suggestions, consent);

        // Hide panel after submission
        if (surveyPanel != null)
        {
            surveyPanel.SetActive(false);
        }
        
        Debug.Log("Survey Submitted!");
    }
}
