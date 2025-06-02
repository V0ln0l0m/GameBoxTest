using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsFinding : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] PointFeatures[] pointsFeatures;


    private void Start()
    {
        FindingPoints();
    }

    public void FindingPoints()
    {
        int numberOfPoints = Random.Range(3, 5);

        for (int i = 0; i < points.Length; i++)
        {
            points[i].SetActive(false);
            if (i < numberOfPoints)
            {
                points[i].transform.localPosition = new Vector3(Random.Range(-750,350), Random.Range(-350, 350),0);
                points[i].SetActive(true);
            }
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
