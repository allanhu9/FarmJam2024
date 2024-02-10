
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
        TimeManager.OnDayChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
        TimeManager.OnDayChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = "Day:" + $"{TimeManager.Day}" + " " + $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
