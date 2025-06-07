using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform checkGroundTransform;
    CharacterController charController;

    [SerializeField] float speed = 4;
    [SerializeField] float speedRun = 7;
    [SerializeField] float gravity = 20f; 
    [SerializeField] float jumpHeight = 1;


    [SerializeField] float sensitivity = 1.2f;

    bool isGrounded;
    float rotationX;

    Vector3 moveDir;
    Vector3 velosity;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Velosity();
        Move();
        Rotation();
    }

    

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -70, 70);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.Rotate(0, mouseX, 0);
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDir = transform.forward * moveZ + transform.right * moveX;

        if (Input.GetKey(KeyCode.LeftShift) && (moveX != 0 || moveZ != 0))
        {
            charController.Move(moveDir * speedRun * Time.deltaTime);
        }
        else
        {
            charController.Move(moveDir * speed * Time.deltaTime);
        }

    }

    void Velosity()
    {
        isGrounded = Physics.CheckSphere(checkGroundTransform.position, 0.1f);
        //if (isGrounded && velosity.y < 0)
        //{
        //    velosity.y = -2;
        //}

        velosity.y -= Time.deltaTime * gravity;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velosity.y = Mathf.Sqrt(jumpHeight * 2f *gravity);
        }
        charController.Move(velosity * Time.deltaTime);
    }
}
