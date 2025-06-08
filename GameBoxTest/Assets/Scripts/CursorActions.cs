using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorActions : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject hintAboutButton;
    [SerializeField] MessageAttenuat messageAtten;


    [SerializeField] LayerMask raycastMask;
    [SerializeField] LayerMask defaultMask;
    [SerializeField] float raycastDistance;

    Animator cursorAnim;
    FindUnknownInNotebook findUnknScript;

    private void Awake()
    {
        cursorAnim = GetComponent<Animator>();
        findUnknScript = gameManager.GetComponent<FindUnknownInNotebook>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, raycastDistance, raycastMask))
        {
            hintAboutButton.SetActive(true);
            cursorAnim.SetBool("FindTarget", true);
            if(Input.GetMouseButtonDown(0))
            {
                Use(hit);
            }
        }
        else
        {
            hintAboutButton.SetActive(false);
            cursorAnim.SetBool("FindTarget", false);
        }
    }

    void Use(RaycastHit hit)
    {
        if (hit.collider.tag == "Door")
            gameManager.LoadNotebook();
        else if (hit.collider.tag == "TargetObj")
        {
            hit.collider.gameObject.layer = defaultMask;
            messageAtten.SetVisible();
            findUnknScript.AddingToQueueFounds();
            
        }
            

    }
}
