using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector2 cameraRotatation;
    Vector3 cameraOffset;
    Transform playerCam;

    InputAction lookVector;

    Rigidbody rb;

    float verticalMove;
    float horizontalMove;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float Xsensitivity = 1.0f;
    public float Ysensitivity = 1.0f;
    public float camRotationLimit = 90.0f;

    public int health = 100;
    public int maxHealth = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        cameraOffset = new Vector3(0, .5f, .5f);
        rb = GetComponent<Rigidbody>();
        playerCam = transform.GetChild(0);
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        cameraRotatation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //Camera Rotation System


        cameraRotatation.x += lookVector.ReadValue<Vector2>().x * Xsensitivity;
        cameraRotatation.y += lookVector.ReadValue<Vector2>().y * Ysensitivity;

        cameraRotatation.y = Mathf.Clamp(cameraRotatation.y, -camRotationLimit, camRotationLimit);

        playerCam.rotation = Quaternion.Euler(-cameraRotatation.y, cameraRotatation.x, 0);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killzone")
            health = 0;

        if (health < maxHealth)
        {

            if (other.tag == "healLV1")
            { health += 10;
                Destroy(other.gameObject);
            }
        }

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "healzone")
        {
            health += 1;
        }
    }
}
