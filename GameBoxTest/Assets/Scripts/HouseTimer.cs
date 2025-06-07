using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HouseTimer : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] int givenMinutes = 5;

    [SerializeField] Color mainColor;

    int minutes;
    int seconds;
    float timer;

    void Start()
    {
        minutes = givenMinutes;
        seconds = 0;
        timer = 0;
        UpdateUITimer();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            secondPassed();
        }
    }

    void secondPassed()
    {
        seconds--;
        if (seconds < 0)
        {
            seconds = 59;
            minutes--;
        }

        UpdateUITimer();

        if (minutes == 0 && seconds == 0)
        {
            gameManager.LoadNotebook();
        }
    }

    void UpdateUITimer()
    {
        if (minutes == 0)
            timerText.color = Color.red;
        else
            timerText.color = mainColor;

        string secZero;
        if (seconds < 10)
            secZero = "0";
        else
            secZero = "";

        timerText.text = minutes + ":" + secZero + seconds;
    }
}
