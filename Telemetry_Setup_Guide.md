# Telemetry System Setup Guide

Since I cannot interact with the Unity Editor directly, please follow these steps to set up the telemetry system in your project.

## 1. Scene Setup
1.  Open your main scene (or the first scene of your game).
2.  Create a new Empty GameObject and name it `TelemetrySystem`.
3.  Attach the `TelemetryManager` script to this GameObject.
    - This object will persist across scenes automatically.

## 2. Survey UI Setup
1.  Create a UI Canvas (if you don't have one).
2.  Create a Panel for the survey (e.g., "SurveyPanel").
3.  Inside the Panel, add:
    - **Input Field (TMP)**: For "Name & Student ID".
    - **Slider**: For the rating (set Min Value to 1, Max Value to 5, Whole Numbers to True).
    - **Text (TMP)**: To display the current rating value.
    - **Input Field (TMP)**: For comments.
    - **Input Field (TMP)**: For "Bug Report".
    - **Input Field (TMP)**: For "Suggestions".
    - **Toggle**: For "Consent to share data".
    - **Button**: For "Submit".
4.  Attach the `SurveyController` script to the `SurveyPanel` (or a manager object).
5.  Drag and drop the UI references to the `SurveyController` slots in the Inspector:
    - **Survey Panel**: The panel itself.
    - **Identity Input**: The TMP Input Field for Name/ID.
    - **Comments Input**: The TMP Input Field for comments.
    - **Bug Report Input**: The TMP Input Field for bugs.
    - **Suggestions Input**: The TMP Input Field for suggestions.
    - **Rating Slider**: The Slider.
    - **Consent Toggle**: The Toggle.
    - **Rating Value Text**: The TMP Text for the slider value.
    - **Submit Button**: The Submit button.

## 3. Testing
1.  Play the game in the Editor.
2.  Interact with the survey and click Submit.
3.  Stop the game.
4.  Check the `Logs/Telemetry Logs` folder in your project root. You should see a `Telemetry_{Identity}.json` (or `Telemetry_{SessionID}.json` if no identity provided) file.
5.  Open the file to verify it contains system info, FPS data, and your survey feedback.

## 4. Building
When you build the game, the logs will be saved to the device's persistent data path:
- **Windows**: `%userprofile%\AppData\LocalLow\<CompanyName>\<ProductName>\`
