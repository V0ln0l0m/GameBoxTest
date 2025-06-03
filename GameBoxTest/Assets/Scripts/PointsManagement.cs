using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManagement : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] PointFeatures[] pointsFeatures;

    bool firstFinding = false;
    Coroutine findPointsCor;

    private void Start()
    {
        if (!firstFinding)
        {
            firstFinding = true;
            FindingPoints();
        }
            
    }

    public void FindingPoints()
    {
        if (findPointsCor != null)
            StopCoroutine(findPointsCor);
        findPointsCor = StartCoroutine(FindPoints());
        //int numberOfPoints = Random.Range(3, 5);

        //for (int i = 0; i < points.Length; i++)
        //{
        //    points[i].SetActive(false);
        //    if (i < numberOfPoints)
        //    {
        //        points[i].transform.localPosition = new Vector3(Random.Range(-750,350), Random.Range(-350, 350),0);
        //        points[i].SetActive(true);
        //        pointsFeatures[i].ChooseLevel_Name();
        //    }
        //}
        //PointSelection(-1);
    }

    IEnumerator FindPoints()
    {
        PointSelection(-1);

        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
        }

        int numberOfPoints = Random.Range(3, 6);

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.localPosition = new Vector3(Random.Range(-750, 350), Random.Range(-350, 350), 0);

            for (int j = 0; j < points.Length; j++)  // Проверка дистанции между точками и изменение положения, если ближе 100 к другим
            {
                if (i != j && points[j].activeInHierarchy)
                {
                    if (Vector3.Distance(points[i].transform.position, points[j].transform.position) < 100)
                    {
                        points[i].transform.localPosition = new Vector3(Random.Range(-750, 350), Random.Range(-350, 350), 0);
                        j--;
                    }
                }
            }

            points[i].SetActive(true);
            pointsFeatures[i].ChooseLevel_Name();

            yield return new WaitForSeconds(0.4f);
        }
        
    }

    public void PointSelection(int numberPoint)
    {
        for (int i = 0;i < points.Length;i++)
        {
            if (i == numberPoint)
                pointsFeatures[i].SelectPoint();
            else
                pointsFeatures[i].DeSelectPoint();


        }
    }
}
