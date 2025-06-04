using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NotebookLine : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI placeText;
    [SerializeField] TextMeshProUGUI eventText;
    [SerializeField] TextMeshProUGUI dateText;

    [SerializeField] Image backgroundL;
    [SerializeField] Image backgroundR;

    [SerializeField] int yearIntervalMin;
    [SerializeField] int yearIntervalMax;

    [SerializeField] Color mainColor;
    [SerializeField] Color overlapFoundColor;
    [SerializeField] Color selectColor;


    public string nameObject;
    public string place;
    public string eventHappen;
    public string date;

    public bool main;
    public int overlapFound;

    bool select = false;

    
    string[] names = { "Михаил", "София", "Александр", "Мария", "Матвей", "Вера", "Тимофей", "Елена", "Илья", "Ксения", };
    string[] places = { "сквер", "дом", "парк", "улица", "магазин", "предприятие", "свалка", "мост", "поле", "площадь" };
    string[] events = {"похищение", "замечен НЛО", "контакт с ИС", "странные звуки", "странное поведение", "необычное событие", "потеря памяти", "вспышки в темноте", "следы ИО "};



    public void LineCreate()
    {
        nameObject = names[Random.Range(0,names.Length)];
        place = places[Random.Range(0, places.Length)];
        eventHappen = events[Random.Range(0, events.Length)];
        DateCreate();
        main = true;
        select = false;
        overlapFound = 0;

        NoteBookStorage.notebookLines.Add(this);
    }

    void DateCreate()
    {
        int month = Random.Range(1, 13);

        int day;
        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            day = Random.Range(1, 32);
        else if (month == 4 || month == 6 || month == 9 || month == 11)
            day = Random.Range(1, 31);
        else
            day = Random.Range(1, 29);

        int year = Random.Range(yearIntervalMin, yearIntervalMax + 1);

        string dayZero;
        if (day < 10)
            dayZero = "0";
        else
            dayZero = "";

        string monthZero;
        if (month < 10)
            monthZero = "0";
        else
            monthZero = "";

        date = dayZero + day + "." + monthZero + month + "." + year;
    }

    public void UpdateUI()
    {
        if (main)
            nameText.text = nameObject;
        else
            nameText.text = "";

        placeText.text = place;
        eventText.text = eventHappen;
        dateText.text = date;
        UpdateBackgrColor();
    }

    void UpdateBackgrColor()
    {
        if (select)
            backgroundL.color = backgroundR.color = selectColor;
        else if (overlapFound > 0)
            backgroundL.color = backgroundR.color = overlapFoundColor;
        else
            backgroundL.color = backgroundR.color = mainColor;
    }
}
