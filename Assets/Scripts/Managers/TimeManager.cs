using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI remainingTimeText;
    public Button startButton;
    public Button speedUpButton;

    //private float startTime;
    private bool timerStarted = false;
    public float timerDuration; // in minutes for easier testing
    private float subTimerDuration; // in seconds for easier code editing
    private float timeMultiplier = 1f;

    private string[] speedOptions = { "1x", "2x", "4x", "8x" };
    private int currentSpeedIndex = 0;

    private void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        speedUpButton.onClick.AddListener(SwitchSpeed);

        //Read the data saved in Player Prefs
        if (PlayerPrefs.HasKey("SubTimerDuration"))
        {
            subTimerDuration = PlayerPrefs.GetFloat("SubTimerDuration");
            timerStarted = true;
        }
        else
        {
            subTimerDuration = 0f;
        }

        if (PlayerPrefs.HasKey("TimeMultiplier"))
            timeMultiplier = PlayerPrefs.GetFloat("TimeMultiplier");

        //update multiplier UI so it matches Player Prefs
        currentSpeedIndex = TimeMultiplierToIndex() % speedOptions.Length;
        speedUpButton.GetComponentInChildren<TextMeshProUGUI>().text = speedOptions[currentSpeedIndex];

        // Calculate the elapsed time
        float elapsedTime = CalculateElapsedSeconds() * timeMultiplier;

        // Calculate the remaining time in seconds
        float remainingTime = subTimerDuration - elapsedTime;

        // Check if the timer has finished while not in play mode
        if (remainingTime <= 0)
        {
            if (PlayerPrefs.HasKey("SubTimerDuration"))
            {
                Debug.Log("Timer has finished while you were not in play mode.");
            }

            // Convert remaining time to minutes and seconds
            int minutes = (int)(remainingTime / 60f);
            int seconds = (int)(remainingTime % 60f);
            remainingTime = 0f;

            // Display the remaining time in MM:SS format
            remainingTimeText.text = "00:00";
            timerStarted = false;
            startButton.interactable = true;
            PlayerPrefs.DeleteKey("SavedTime");
            PlayerPrefs.DeleteKey("SubTimerDuration");
            PlayerPrefs.Save();
        }
        else
        {
            timerStarted = true;
            startButton.interactable = false; // Disable the button if the timer is still active from the last session
        }
    }

    private void Update()
    {
        timeText.text = SetTimeUI();

        if (timerStarted)
        {
            // Calculate the elapsed time
            float elapsedTime = CalculateElapsedSeconds() * timeMultiplier;

            // Calculate the remaining time in seconds
            float remainingTime = subTimerDuration - elapsedTime;

            // Check if the timer has finished
            if (remainingTime <= 0)
            {
                timerStarted = false;
                remainingTime = 0;
                startButton.interactable = true;
                PlayerPrefs.DeleteKey("SavedTime");
                PlayerPrefs.DeleteKey("SubTimerDuration");
                PlayerPrefs.Save();
                Debug.Log("Timer has finished!");
            }

            // Convert remaining time to minutes and seconds
            int minutes = (int)(remainingTime / 60f);
            int seconds = (int)(remainingTime % 60f);

            // Display the remaining time in MM:SS format
            remainingTimeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    private string SetTimeUI()
    {
        // Get the current system time
        System.DateTime now = System.DateTime.Now;

        // Format the time as HH:MM:SS
        string systemTime = now.ToString("HH:mm:ss");

        return systemTime;
    }

    public void StartTimer()
    {
        if (!timerStarted)
        {
            // Store the current time as the start time
            SaveStartTime();

            timerStarted = true;
            startButton.interactable = false; // Disable the button when the timer starts

            subTimerDuration = timerDuration * 60f;
            PlayerPrefs.SetFloat("SubTimerDuration", subTimerDuration);
            PlayerPrefs.Save();
        }
    }

    public void SwitchSpeed()
    {
        if (timerStarted)
        {
            // Calculate the elapsed time
            float elapsedTime = CalculateElapsedSeconds() * timeMultiplier;
            subTimerDuration = subTimerDuration - elapsedTime;
            PlayerPrefs.SetFloat("SubTimerDuration", subTimerDuration);
            PlayerPrefs.Save();

            // Set a new start time because we are changing the multiplier
            SaveStartTime();
        }

        currentSpeedIndex = (currentSpeedIndex + 1) % speedOptions.Length;
        speedUpButton.GetComponentInChildren<TextMeshProUGUI>().text = speedOptions[currentSpeedIndex];
        SetTimeMultiplier();
    }

    private void SetTimeMultiplier()
    {
        switch (currentSpeedIndex)
        {
            case 0:
                timeMultiplier = 1f;
                break;
            case 1:
                timeMultiplier = 2f;
                break;
            case 2:
                timeMultiplier = 4f;
                break;
            case 3:
                timeMultiplier = 8f;
                break;
        }

        PlayerPrefs.SetFloat("TimeMultiplier", timeMultiplier);
        PlayerPrefs.Save();
    }

    private int TimeMultiplierToIndex()
    {
        if (timeMultiplier == 1)
            return 0;
        if (timeMultiplier == 2)
            return 1;
        if (timeMultiplier == 4)
            return 2;
        else
            return 3;
    }

    float CalculateElapsedSeconds()
    {
        if (PlayerPrefs.HasKey("SavedTime"))
        {
            string savedTimeString = PlayerPrefs.GetString("SavedTime");
            System.DateTime savedTime = System.DateTime.ParseExact(savedTimeString, "yyyy-MM-dd HH:mm:ss", null);
            System.TimeSpan timeSpan = System.DateTime.Now - savedTime;
            float elapsedSeconds = (float)timeSpan.TotalSeconds;
            return elapsedSeconds;
        }

        // If "SavedTime" key doesn't exist in PlayerPrefs, return 0 seconds
        return 0f;
    }

    void SaveStartTime()
    {
        System.DateTime currentTime = System.DateTime.Now;
        string formattedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
        PlayerPrefs.SetString("SavedTime", formattedTime);
        PlayerPrefs.Save();
    }
}