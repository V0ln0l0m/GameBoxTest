using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageAttenuat : MonoBehaviour
{
    [SerializeField] float timeVisible;

    Image imgHint;
    TextMeshProUGUI textHint;

    Color colorImg;
    Color colorText;

    float visible = 0f;

    void Start()
    {
        imgHint = GetComponent<Image>();
        textHint = GetComponentInChildren<TextMeshProUGUI>();
        colorImg = imgHint.color;
        colorText = textHint.color;
    }

    void Update()
    {
        if (visible > 0)
        {
            visible -= Time.deltaTime/2;

            imgHint.color = new Color(colorImg.r, colorImg.g, colorImg.b, Mathf.Clamp (visible, 0, 0.64f));
            textHint.color = new Color(colorText.r, colorText.g, colorText.b, Mathf.Clamp(visible, 0, 0.75f));
        }
    }

    public void SetVisible()
    {
        visible = 0.75f + timeVisible;
    }
}
