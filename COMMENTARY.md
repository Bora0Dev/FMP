# Technical Commentary: Telemetry and User Testing System

## Introduction
This document outlines the technical design and implementation of the user testing and telemetry system developed for the Final Major Project (FMP) prototype. The system is designed to bridge the gap between subjective player feedback and objective performance metrics, providing a holistic view of the user experience. By integrating these data streams directly into the Unity engine, we ensure that QA testing is streamlined and that data collection is consistent across different playtest sessions.

## 1. Collecting Player Feedback and Consent
The core of the feedback mechanism is the `SurveyController` class, which manages a runtime User Interface (UI). Unlike external survey tools (e.g., Google Forms) that require players to leave the game environment, this system presents a non-intrusive overlay at the end of a session or upon user request.

**Data Collection Points:**
*   **Identity**: A mandatory field captures the user's Name and Student ID, allowing us to correlate feedback with specific testers.
*   **Consent**: A boolean toggle explicitly asks for permission to collect and share data, ensuring ethical compliance with data protection standards.
*   **Qualitative Data**: Dedicated input fields for "Comments," "Bug Reports," and "Suggestions" allow users to provide free-form text feedback.
*   **Quantitative Data**: A 1-5 slider provides a quick "Satisfaction Rating" that can be aggregated to track sentiment trends over time.

This in-engine approach maximizes response rates by reducing friction; players can report bugs immediately while the context is fresh in their minds.

## 2. Telemetry and Performance Logging
While the survey captures what players *say*, the `TelemetryManager` captures what the system *does*. This script runs as a persistent Singleton, surviving scene loads to track the entire session lifecycle.

**Logged Metrics:**
*   **System Specifications**: Upon initialization, the system logs the Operating System, Graphics Device (GPU), Processor Type (CPU), and System Memory (RAM). This is crucial for identifying hardware-specific issues.
*   **Performance Data**: The system calculates and logs the Average Frames Per Second (FPS) by sampling the frame rate every second. This granular performance data helps pinpoint performance bottlenecks.
*   **Session Metadata**: A unique Session ID and timestamp are generated for every run, ensuring that logs are distinct and chronologically ordered.

## 3. Tools and Frameworks Used
The system was built using **Unity 2022+** and **C#**, leveraging the engine's native capabilities to minimize external dependencies.

*   **Unity UI (UGUI) / TextMeshPro**: Used for building the responsive survey interface.
*   **JsonUtility**: Unity's built-in JSON serializer is used to format the data. JSON was chosen for its human-readability and universal compatibility with data visualization tools.
*   **System.IO**: Standard .NET file I/O is used to write logs locally.
*   **Sanitization**: Custom Regex logic ensures that user inputs (like names) do not create invalid filenames, preventing file system errors.

## 4. Informing QA and Game Improvements
The combination of subjective and objective data creates a powerful feedback loop for Quality Assurance (QA):

*   **Performance Optimization**: If a player reports "lag" in the Bug Report, we can cross-reference their session log. If the Average FPS is low and they are running on minimum spec hardware, we know optimization is needed. If FPS is high, the "lag" might be a network or input latency issue.
*   **Bug Reproduction**: The "Bug Report" field, combined with the Session ID, allows developers to isolate specific logs. If a crash or error occurs, knowing the exact hardware configuration helps in reproducing the issue on similar devices.
*   **Feature Prioritization**: The "Suggestions" field helps prioritize the backlog. Frequently requested features from high-rated sessions might be prioritized over niche requests.

By saving logs to a dedicated `Logs/Telemetry Logs` directory, the team can easily archive and analyze playtest data, driving data-informed decisions for the final polish of the FMP.
