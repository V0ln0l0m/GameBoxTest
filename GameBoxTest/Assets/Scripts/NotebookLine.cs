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

    [SerializeField] Button connectButton;

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
    public NotebookLine mainConnection;
    public int namberMainConn;

    public int connectedLines;

    bool select = false;

    
    string[] names = { "Михаил", "София", "Александр", "Мария", "Матвей", "Вера", "Тимофей", "Елена", "Илья", "Ксения", };
    string[] places = { "сквер", "дом", "парк", "улица", "магазин", "предприятие", "свалка", "мост", "поле", "площадь" };
    string[] events = {"похищение", "замечен НЛО", "контакт с ИС", "странные звуки", "странное поведение", "необычное событие", "потеря памяти", "вспышки в темноте", "следы ИО "};



    public void LineCreate()
    {
        int percentKnown = NotebookManager.instance.percentKnownData;

        if (Random.Range(0, 99) < percentKnown)
            nameObject = names[Random.Range(0, names.Length)];
        else
            nameObject = "???";

        if (Random.Range(0, 99) < percentKnown)
            place = places[Random.Range(0, places.Length)];
        else
            place = "???";

        if (Random.Range(0, 99) < percentKnown)
            eventHappen = events[Random.Range(0, events.Length)];
        else
            eventHappen = "???";

        DateCreate();
        main = true;
        select = false;
        connectedLines = 0;

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

        if (nameObject != "???" && place != "???" && eventHappen != "???")
            connectButton.interactable = true;
        else
            connectButton.interactable = false;

        if (!main)
            connectButton.gameObject.SetActive(false);
        else
            connectButton.gameObject.SetActive(true);


        UpdateBackgrColor();
    }

    void UpdateBackgrColor()
    {
        if (select)
            backgroundL.color = backgroundR.color = selectColor;
        else if (connectedLines > 0 || !main)
            backgroundL.color = backgroundR.color = overlapFoundColor;
        else
            backgroundL.color = backgroundR.color = mainColor;
    }

    public void IdentSelectedLine()
    {
        if (main)
            NotebookManager.selectedLine = this;
        else
            NotebookManager.selectedLine = mainConnection;

        NotebookManager.instance.SelectionLine();
        
    }

    public void ConnectLine()
    {
        NotebookLine selectLine = NotebookManager.selectedLine;
        int indexSelectLine = NoteBookStorage.notebookLines.IndexOf(selectLine);
        int indexLine = NoteBookStorage.notebookLines.IndexOf(this);


        if (selectLine != null && selectLine.nameObject == nameObject && selectLine.place != "???" &&
            selectLine.eventHappen != "???" && selectLine != this)
        {
            int newIndex = indexSelectLine + selectLine.connectedLines;
            bool above = indexLine < indexSelectLine; 
            if (!above) 
                newIndex++;
            transform.SetSiblingIndex(newIndex);
            
            selectLine.connectedLines++;
            main = false;
            select = true;
            mainConnection = selectLine;
            
            NoteBookStorage.notebookLines.RemoveAt(indexLine);
            NoteBookStorage.notebookLines.Insert(newIndex, this);

            UpdateUI();

            if (connectedLines > 0)
            {
                for (int i = 0; i < connectedLines; i++)
                {
                    NotebookLine connectLine;
                    if (!above)
                    {
                        newIndex++;
                        indexLine++;
                    }
                    
                    connectLine = NoteBookStorage.notebookLines[indexLine];

                    connectLine.transform.SetSiblingIndex(newIndex);

                    selectLine.connectedLines++;
                    connectLine.select = true;
                    connectLine.mainConnection = selectLine;

                    NoteBookStorage.notebookLines.RemoveAt(indexLine);
                    NoteBookStorage.notebookLines.Insert(newIndex, connectLine);

                    connectLine.UpdateUI();
                }
            }
        }
        UpdNamberMainConnect();
    }

    void UpdNamberMainConnect()
    {
        List<NotebookLine> lineList = NoteBookStorage.notebookLines;

        for (int i = 0;i < lineList.Count;i++)
        {
            if (!lineList[i].main)
            {
                lineList[i].namberMainConn = lineList.IndexOf(lineList[i].mainConnection);
            }
            
        }
    }

    public void SelectLine()
    {
        select = true;
        UpdateBackgrColor();
    }

    public void DeSelectLine()
    {
        select = false;
        UpdateBackgrColor();
    }
}
