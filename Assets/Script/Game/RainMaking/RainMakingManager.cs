using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RainMakingManager : MonoBehaviour
{
    public static List<RainMakingPos> allRainMakingPos = new List<RainMakingPos>();

    public GameObject UI;
    public TextMeshProUGUI timerText;
    public float distance = 5f;

    public static float leftRainMakingTime = 2f;
    float elapsedTime = 0f;
    float targetTime = 9999f;
    bool isRunning = false;
    bool UIactivated = false;

    void Start()
    {
        targetTime = UnityEngine.Random.Range(3600.0f, 7200.0f);
        ResetTimer();
    }

    void Update()
    {

        if (UIactivated)
        {
            leftRainMakingTime -= Time.deltaTime;

            if (leftRainMakingTime <= 0 || !CanRainMaking())
            {
                ResetTimer();
            }
        }
        else
        {
            if (CanRainMaking())
            {
                TimerOn();
                leftRainMakingTime = 5f;
            }
        }

        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }

        if (Test.Instance.isTest)
        {
            if (Input.GetKey(KeyCode.Keypad3))
            {
                elapsedTime += 60;
                leftRainMakingTime = 5f;
            }
            else if (Input.GetKey(KeyCode.Keypad4))
            {
                elapsedTime -= 60;
                leftRainMakingTime = 5f;
            }
        }

        if (targetTime <= elapsedTime)
        {
            ScreenTransition.JustLoadScene("End", "LoadingScene");
        }
    }

    private void UpdateTimerDisplay()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);

        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}",
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds / 10);

        timerText.text = timeString;
    }

    public void TimerOn()
    {
        isRunning = true;

        UI.SetActive(true);
        UIactivated = true;
    }

    public void ResetTimer()
    {
        isRunning = false;
        elapsedTime = 0f;
        UpdateTimerDisplay();
        UI.SetActive(false);
        UIactivated = false;
    }

    bool CanRainMaking()
    {
        ItemType selectedItem = InventoryManager.instance.GetSelectedItem();
        if (selectedItem != ItemType.Pull) return false;

        Vector3 playerPos = Vector3.zero;
        if (PlayerCamera.Instance != null) playerPos = PlayerCamera.Instance.transform.position;
        foreach (var pos in allRainMakingPos)
        {
            if (pos.CanRainMaking(playerPos, distance)) return true;
        }

        return false;
    }
}
