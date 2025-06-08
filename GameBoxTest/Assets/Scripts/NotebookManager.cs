using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] Transform spaceForLines;
    [SerializeField] GameObject linePrefab;

    [SerializeField] int numberOfLines;

    public int percentKnownData;

    public static NotebookLine selectedLine;

    public static NotebookManager instance;

    Queue<FoundPosInNotebook> queueFoundsManager = new Queue<FoundPosInNotebook>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (NoteBookStorage.filled)
        {
            CheckingQueue();

            FillingFromStorage();
        }
        else
        {
            InitialFilling();
        }
        SelectionLine();
    }

    public void InitialFilling()
    {
        for (int i = 0; i < numberOfLines; i++)
        {
            GameObject line = Instantiate(linePrefab, spaceForLines);
            NotebookLine lineScript = line.GetComponent<NotebookLine>();
            lineScript.LineCreate();
            lineScript.UpdateUI();
        }
        NoteBookStorage.filled = true;
    }

    public void CheckingQueue()
    {
        if (NoteBookStorage.queueFounds.Count > 0)
        {
            queueFoundsManager = NoteBookStorage.queueFounds; // ��� ������� �������

            for (int i = 0; i < NoteBookStorage.queueFounds.Count; i++)
            {
                FoundPosInNotebook found = NoteBookStorage.queueFounds.Dequeue();

                if (found.typeUnknown == TypeUnknown.nameObj)
                    NoteBookStorage.notebookLines[found.posLineInStorage].nameObject = found.value;
                else if (found.typeUnknown == TypeUnknown.place)
                    NoteBookStorage.notebookLines[found.posLineInStorage].place = found.value;
                else if (found.typeUnknown == TypeUnknown.eventHeppen)
                    NoteBookStorage.notebookLines[found.posLineInStorage].eventHappen = found.value;
            }
        }
        queueFoundsManager = null;
    }

    public void FillingFromStorage()
    {
        List<NotebookLine> linesListStorage = NoteBookStorage.notebookLines;
        int count = linesListStorage.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject line = Instantiate(linePrefab, spaceForLines);
            NotebookLine lineScript = line.GetComponent<NotebookLine>();

            lineScript.nameObject = linesListStorage[i].nameObject;
            lineScript.place = linesListStorage[i].place;
            lineScript.eventHappen = linesListStorage[i].eventHappen;
            lineScript.date = linesListStorage[i].date;
            lineScript.main = linesListStorage[i].main;
            lineScript.connectedLines = linesListStorage[i].connectedLines;
            lineScript.namberMainConn = linesListStorage[i].namberMainConn;
            if (!lineScript.main)
                lineScript.mainConnection = linesListStorage[lineScript.namberMainConn];
            lineScript.UpdateUI();

            linesListStorage[i] = lineScript;

            AppearanceEntryLines(queueFoundsManager);
        }
    }

    public void AppearanceEntryLines(Queue<FoundPosInNotebook> founds)
    {
        if (queueFoundsManager != null)
        {
            for (int i = 0; i < founds.Count; i++)
            {
                FoundPosInNotebook found = founds.Dequeue();
                NoteBookStorage.notebookLines[found.posLineInStorage].AppearancEntry(found.typeUnknown);
            }
        }
    }

    public void SelectionLine()
    {
        int count = NoteBookStorage.notebookLines.Count;
        for (int i = 0; i < count; i++)
        {
            NotebookLine line = NoteBookStorage.notebookLines[i];
            if (selectedLine != null && (line == selectedLine || line.mainConnection == selectedLine))
                line.SelectLine();
            else
                line.DeSelectLine();
        }
    }
}
