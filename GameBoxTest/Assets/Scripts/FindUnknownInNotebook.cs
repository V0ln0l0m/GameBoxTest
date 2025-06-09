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
    List<FoundPosInNotebook> listFounds = new List<FoundPosInNotebook>();

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
                    listFounds.Add(unknPos);
                }

                if (linesStorage[i].place == "???")
                {
                    FoundPosInNotebook unknPos = new FoundPosInNotebook();
                    unknPos.posLineInStorage = i;
                    unknPos.typeUnknown = TypeUnknown.place;
                    unknPos.value = linesStorage[i].ChoosingPlace();
                    listFounds.Add(unknPos);
                }

                if (linesStorage[i].eventHappen == "???")
                {
                    FoundPosInNotebook unknPos = new FoundPosInNotebook();
                    unknPos.posLineInStorage = i;
                    unknPos.typeUnknown = TypeUnknown.eventHeppen;
                    unknPos.value = linesStorage[i].ChoosingEvent();
                    listFounds.Add(unknPos);
                }
            }
        }
    }

    public void AddingToQueueFounds()
    {
        if (listFounds.Count > 0)
        {
            int index = Random.Range(0, listFounds.Count);
            FoundPosInNotebook found = listFounds[index];
            NoteBookStorage.queueFounds.Enqueue(found);
            listFounds.RemoveAt(index);
        }
        else
        {
            Debug.Log("Пустые позиции в записной книжке не найдены");
        }
    }
}

public class FoundPosInNotebook
{
    public int posLineInStorage;
    public TypeUnknown typeUnknown;
    public string value;
}
