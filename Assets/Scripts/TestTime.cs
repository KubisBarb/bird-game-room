using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("SavedTime", "2023-05-28 17:54:00");
        Debug.Log(CalculateElapsedSeconds());

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

}
