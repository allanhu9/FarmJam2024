using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;
public class TimeManager : MonoBehaviour, DataPersistable
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnDayChanged;
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static int Day { get; private set; }

    private float gameMinuteToRealSecond = 0.1f;
    private float timer;
    private Light2D sun;

    public GameObject PauseMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        Day = 1;
        Minute = 0;
        Hour = 8;
        this.timer = gameMinuteToRealSecond;
        sun = GameObject.FindWithTag("Sun").GetComponent<Light2D>();
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
        float timeElapsed = Time.timeSinceLevelLoad;
        float period = 24 * 60 * gameMinuteToRealSecond;
        float intensity = Mathf.Sin(timeElapsed * Mathf.PI * 2 / period) * 0.5f + 0.5f;
        sun.intensity = intensity;
    }

    public void Sleep()
    {
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
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadData(GameData data)
    {
        Day = data.Day;
        Minute = data.Minute;
        Hour = data.Hour;
        sun = GameObject.FindWithTag("Sun").GetComponent<Light2D>();
    }

    public void SaveData(ref GameData data)
    {
        data.Day = Day;
        data.Minute = Minute;
        data.Hour = Hour;
    }
}
