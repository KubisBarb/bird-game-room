using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTime : MonoBehaviour
{
    private void Start()
    {
        float maxFloatValue = float.MaxValue;
        int secondsInMinute = 60;
        int minutesInHour = 60;
        int hoursInDay = 24;

    }
        float maxTimeInDays = maxFloatValue / (secondsInMinute * minutesInHour * hoursInDay);

        Debug.Log(maxTimeInDays);
    }
}
