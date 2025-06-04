using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] Transform spaceForLines;
    [SerializeField] GameObject linePrefab;

    [SerializeField] int numberOfLines;

    void Start()
    {
        if (NoteBookStorage.filled)
        {
            for (int i = 0; i < NoteBookStorage.notebookLines.Count; i++)
            {
                GameObject line = Instantiate(linePrefab, spaceForLines);
                NotebookLine lineScript = line.GetComponent<NotebookLine>();

                lineScript.nameObject = NoteBookStorage.notebookLines[i].nameObject;
                lineScript.place = NoteBookStorage.notebookLines[i].place;
                lineScript.eventHappen = NoteBookStorage.notebookLines[i].eventHappen;
                lineScript.date = NoteBookStorage.notebookLines[i].date;
                lineScript.main = NoteBookStorage.notebookLines[i].main;
                lineScript.overlapFound = NoteBookStorage.notebookLines[i].overlapFound;

                lineScript.UpdateUI();
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
    }


}
