using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManagement : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] AudioManager audManager;

    public static PointFeatures[] pointsFeatures;

    public static List<string> nameList = new List<string>();
    public static List<int> levelList = new List<int>();

    public static bool firstFindingDone = false;

    public static Vector3[] pointsPos;
    public static int pointsVisible;

    Coroutine findPointsCor;

    private void Start()
    {
        SearchPointNames();


        if (!firstFindingDone)
        {
            firstFindingDone = true;
            pointsPos = new Vector3[points.Length];

            pointsFeatures = new PointFeatures[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                pointsFeatures[i] = points[i].GetComponent<PointFeatures>();
            }

            FindingPoints();
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (i < pointsVisible)
                {
                    points[i].transform.localPosition = pointsPos[i];

                    points[i].GetComponent<PointFeatures>().namePoint = pointsFeatures[i].namePoint;
                    points[i].GetComponent<PointFeatures>().levelPoint = pointsFeatures[i].levelPoint;
                    pointsFeatures[i] = points[i].GetComponent<PointFeatures>();
                    pointsFeatures[i].UpdateUIPanel();
                }
                else
                {
                    pointsFeatures[i] = points[i].GetComponent<PointFeatures>();
                    points[i].SetActive(false);
                }
                    
            }
            PointSelection(-1);
        }
    }

    public void FindingPoints()
    {
        if (findPointsCor != null)
            StopCoroutine(findPointsCor);
        findPointsCor = StartCoroutine(FindPoints());
    }

    IEnumerator FindPoints()
    {
        PointSelection(-1);

        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
        }

        pointsVisible = Random.Range(3, 6);

        for (int i = 0; i < pointsVisible; i++)
        {
            pointsPos[i] = points[i].transform.localPosition = new Vector3(Random.Range(-640, 460), Random.Range(-350, 350), 0);
            
            for (int j = 0; j < points.Length; j++)  // Проверка дистанции между точками и изменение положения, если ближе 100 к другим
            {
                if (i != j && points[j].activeInHierarchy)
                {
                    if (Vector3.Distance(points[i].transform.position, points[j].transform.position) < 100)
                    {
                        pointsPos[i] = points[i].transform.localPosition = new Vector3(Random.Range(-640, 460), Random.Range(-350, 350), 0);
                        j--;
                    }
                }
            }

            points[i].SetActive(true);
            pointsFeatures[i].ChooseLevel_Name();
            audManager.PointSound();

            yield return new WaitForSeconds(0.4f);
        }
        
    }

    public void PointSelection(int numberPoint)
    {
        audManager.PointSound();
        for (int i = 0;i < points.Length;i++)
        {
            if (i == numberPoint)
                pointsFeatures[i].SelectPoint();
            else
                pointsFeatures[i].DeSelectPoint();
        }
    }

    void SearchPointNames()
    {
        if (NoteBookStorage.notebookLines != null)
        {
            List<NotebookLine> storageList = NoteBookStorage.notebookLines;
            for (int i = 0; i < storageList.Count; i++)
            {
                if (storageList[i].nameObject != "???" && storageList[i].main)
                {
                    if (!nameList.Contains(storageList[i].nameObject))
                    {
                        nameList.Add(storageList[i].nameObject);
                        levelList.Add(storageList[i].connectedLines+1);
                    }
                    else
                    {
                        int index = nameList.IndexOf(storageList[i].nameObject);
                        if (levelList[index] < storageList[i].connectedLines+1)
                            levelList[index] = storageList[i].connectedLines+1;
                    }
                }
            }
        }
    }
}
