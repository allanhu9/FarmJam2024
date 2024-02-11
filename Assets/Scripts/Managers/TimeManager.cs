using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static int Day { get; private set; }

    private float gameMinuteToRealSecond = 0.1f;
    private float timer;
    private float startTime;
    public Light2D sun;

    public GameObject PauseMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Day = 1;
        Minute = 0;
        Hour = 8;
        timer = gameMinuteToRealSecond;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPauseMenu();
        UpdateTimeCounter();
        UpdateLight();
    }
    public void CheckPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Sleep();
        }
    }
    public void TogglePauseMenu()
    {
        // if turned off
        if (!PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        //Setup();
    }

    private void UpdateTimeCounter()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Minute++;
            OnMinuteChanged?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                Minute -= 60;
                OnHourChanged?.Invoke();
                if (Hour >= 24)
                {
                    Day++;
                    Hour -= 24;
                    OnDayChanged?.Invoke();
                }
            }
            timer = gameMinuteToRealSecond;
        }

        if(Hour == 1)
        {
           Sleep();
        }
    }

    private void UpdateLight()
    {
        float timeElapsed = Time.time - startTime;
        float period = 24 * 60 * gameMinuteToRealSecond;
        float intensity = Mathf.Sin(timeElapsed * Mathf.PI * 2 / period) * 0.5f + 0.5f;
        sun.intensity = intensity;
    }

    public void Sleep()
    {
        startTime = Time.time;
        if(Hour >= 8)
        {
                Day++;
        }
        Hour = 8;
        Minute = 0;
        timer = gameMinuteToRealSecond;
        OnMinuteChanged?.Invoke();
        OnHourChanged?.Invoke();
        OnDayChanged?.Invoke();
        Time.timeScale = 0f;
        SceneManager.LoadSceneAsync(1);
    }
}
