using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PointFeatures : MonoBehaviour
{

    [SerializeField] GameObject pointInfoPanel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI nameText;


    private string[] namesTest = { "Владимир", "Петр", "Андрей", "Владислав", "Дмитрий", };

    private int levelPoint;
    private string namePoint;

    Image image;
    Color mainColor = new Color(1, 0.43f, 0.1f);
    Color selectColor = new Color(0.1f, 0.85f, 0.1f);


    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }


    public void ChooseLevel_Name()
    {
        levelPoint = Random.Range(1, 5);
        levelText.text = "Уровень " + levelPoint;

        namePoint = namesTest[Random.Range(0, 5)];
        nameText.text = namePoint;
    }

    void ChoosePosPanel()
    {
        if (transform.localPosition.x > -210)
        {
            if (transform.position.y > 0)
                pointInfoPanel.transform.localPosition = new Vector3(-140, -100, 0);
            else
                pointInfoPanel.transform.localPosition = new Vector3(-140, 100, 0);
        }
        else
        {
            if (transform.localPosition.y > 0)
                pointInfoPanel.transform.localPosition = new Vector3(140, -100, 0);
            else
                pointInfoPanel.transform.localPosition = new Vector3(140, 100, 0);
        }
    }

    public void SelectPoint()
    {
        image.color = selectColor;
        pointInfoPanel.SetActive(true);
        ChoosePosPanel();
    }

    public void DeSelectPoint()
    {
        image.color = mainColor;
        pointInfoPanel.SetActive(false);
    }
}
