using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TypeUnknown
{
    nameObj,
    place,
    eventHeppen
}


public class FindUnknownInNotebook : MonoBehaviour
{
    List<FoundPosInNotebook> ListFounds = new List<FoundPosInNotebook>();

    private void Start()
    {
        FindUnknPosAndValue();
    }

    void FindUnknPosAndValue()
    {
        if (NoteBookStorage.notebookLines != null)
        {
            List <NotebookLine> linesStorage = NoteBookStorage.notebookLines;
            for (int i = 0; i < linesStorage.Count; i++)
            {
                if (linesStorage[i].nameObject == "???")
                {
                    FoundPosInNotebook unknPos = new FoundPosInNotebook();
                    unknPos.posLineInStorage = i;
                    unknPos.typeUnknown = TypeUnknown.nameObj;
                    unknPos.value = linesStorage[i].ChoosingName();
                    ListFounds.Add(unknPos);
                }

                if (linesStorage[i].place == "???")
                {
                    FoundPosInNotebook unknPos = new FoundPosInNotebook();
                    unknPos.posLineInStorage = i;
                    unknPos.typeUnknown = TypeUnknown.place;
                    unknPos.value = linesStorage[i].ChoosingPlace();
                    ListFounds.Add(unknPos);
                }

                if (linesStorage[i].eventHappen == "???")
                {
                    FoundPosInNotebook unknPos = new FoundPosInNotebook();
                    unknPos.posLineInStorage = i;
                    unknPos.typeUnknown = TypeUnknown.eventHeppen;
                    unknPos.value = linesStorage[i].ChoosingEvent();
                    ListFounds.Add(unknPos);
                }
            }
        }
    }

    public void AddingToQueueFounds()
    {
        if (ListFounds.Count > 0)
        {
            for (int i = 0; i < 10; i++) // Убрать после проверки
            {
                FoundPosInNotebook found = ListFounds[Random.Range(0, ListFounds.Count)];
                NoteBookStorage.queueFounds.Enqueue(found);
            }
            
        }
        else
        {
            Debug.Log("Пустые позиции в запесной книжке не найдены");
        }
        
    }
}

public class FoundPosInNotebook
{
    public int posLineInStorage;
    public TypeUnknown typeUnknown;
    public string value;
}
