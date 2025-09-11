using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 cameraRotatation;
    Vector3 cameraOffset;
    InputAction lookVector;
    Camera playerCam;
    Rigidbody rb;

    float verticalMove;
    float horizontalMove;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float Xsensitivity = 1.0f;
    public float Ysensitivity = 1.0f;
    public float camRotationLimit = 90.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        cameraOffset = new Vector3(0, .5f, -.5f);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        cameraRotatation = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera Rotation System
        playerCam.transform.position = transform.position + cameraOffset;
        cameraRotatation.x += lookVector.ReadValue<Vector2>().x * Xsensitivity;
        cameraRotatation.y += lookVector.ReadValue<Vector2>().y * Ysensitivity;

        cameraRotatation.y = Mathf.Clamp(cameraRotatation.y, -camRotationLimit, camRotationLimit);

        playerCam.transform.rotation = Quaternion.Euler(-cameraRotatation.y, cameraRotatation.x, 0);
        transform.localRotation = Quaternion.AngleAxis(cameraRotatation.x, Vector3.up);

        //Movement system
        Vector3 temp = rb.linearVelocity;

        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;

        rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;
    }
}
