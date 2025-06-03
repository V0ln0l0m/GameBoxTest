using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateChange : MonoBehaviour
{
    TextMeshProUGUI date;

    int day = 3;
    int month = 5;
    int year = 3417;

    private void Awake()
    {
        date = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void ChangingDate() //Высокосные года пака не учитываются
    {
        day++;
        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            if (day > 31)
                ChangeMonth_Year();
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            if (day > 30)
                ChangeMonth_Year();
        }
        else if (month == 2)
        {
            if (day > 28)
                ChangeMonth_Year();
        }
        UpdateUI();
    }

    void ChangeMonth_Year()
    {
        day = 1;
        month++;
        if (month > 12)
        {
            month = 1;
            year++;
        }
    }

    void UpdateUI()
    {
        string dayText;
        if (day < 10)
            dayText = "0" + day;
        else
            dayText = day.ToString();

        string monthText;
        if (month < 10)
            monthText = "0" + month;
        else
            monthText = month.ToString();

        date.text = dayText + "." + monthText + "." + year;
    }
}
