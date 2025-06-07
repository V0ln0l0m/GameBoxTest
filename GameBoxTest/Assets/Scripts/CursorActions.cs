using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorActions : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject hint;
    
    [SerializeField] LayerMask mask;
    [SerializeField] float raycastDistance;

    Animator cursorAnim;

    private void Awake()
    {
        cursorAnim = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, raycastDistance, mask))
        {
            hint.SetActive(true);
            cursorAnim.SetBool("FindTarget", true);
            if(Input.GetMouseButtonDown(0))
            {
                Use(hit);
            }
        }
        else
        {
            hint.SetActive(false);
            cursorAnim.SetBool("FindTarget", false);
        }
    }

    void Use(RaycastHit hit)
    {
        if (hit.collider.tag == "Door")
            gameManager.LoadNotebook();
    }
}
