using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] Transform spaceForLines;
    [SerializeField] GameObject linePrefab;
    [SerializeField] AudioManager audManager;

    [SerializeField] int numberOfLines;

    public int percentKnownData;

    public static NotebookLine selectedLine;

    public static NotebookManager instance;


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
            for (int i = 0; i < NoteBookStorage.queueFounds.Count; i++)
            {
                FoundPosInNotebook found = NoteBookStorage.queueFounds.Dequeue();
                if (found.typeUnknown == TypeUnknown.nameObj)
                    NoteBookStorage.notebookLines[found.posLineInStorage].nameObject = found.value;
                else if (found.typeUnknown == TypeUnknown.place)
                    NoteBookStorage.notebookLines[found.posLineInStorage].place = found.value;
                else if (found.typeUnknown == TypeUnknown.eventHeppen)
                    NoteBookStorage.notebookLines[found.posLineInStorage].eventHappen = found.value;
                StartCoroutine(AppearanceEntryLines(found));
            }
        }
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

            
        }
    }

    IEnumerator AppearanceEntryLines(FoundPosInNotebook found) 
    {
        yield return null; //задержка, чтобы FillingFromStorage() завершил выполнение
        NoteBookStorage.notebookLines[found.posLineInStorage].AppearancEntry(found.typeUnknown);
    }

    public void SelectionLine()
    {
        audManager.PageRustlingSound();
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
