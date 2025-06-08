using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookStorage : MonoBehaviour
{
    public static bool filled = false;
    public static List <NotebookLine> notebookLines = new List<NotebookLine>();
    public static Queue <FoundPosInNotebook> queueFounds = new Queue <FoundPosInNotebook>();

}
