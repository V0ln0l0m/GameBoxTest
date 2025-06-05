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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (NoteBookStorage.filled)
        {
            int count = NoteBookStorage.notebookLines.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject line = Instantiate(linePrefab, spaceForLines);
                NotebookLine lineScript = line.GetComponent<NotebookLine>();

                lineScript.nameObject = NoteBookStorage.notebookLines[i].nameObject;
                lineScript.place = NoteBookStorage.notebookLines[i].place;
                lineScript.eventHappen = NoteBookStorage.notebookLines[i].eventHappen;
                lineScript.date = NoteBookStorage.notebookLines[i].date;
                lineScript.main = NoteBookStorage.notebookLines[i].main;
                lineScript.connectedLines = NoteBookStorage.notebookLines[i].connectedLines;
                lineScript.mainConnection = NoteBookStorage.notebookLines[i].mainConnection;
                lineScript.UpdateUI();

                NoteBookStorage.notebookLines[i] = lineScript;
            }
        }
        else
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
        SelectionLine();
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
