using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 playerposition;

    public GameObject playerBullet1;


    Camera playerCam;

    Ray ray;

    RaycastHit hit;

    Rigidbody rb;

    float verticalMove;
    float horizontalMove;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float groundDetectLength = 0.5f;

    public int health = 100;
    public int maxHealth = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        ray = new Ray(transform.position, transform.forward);

        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        

    }

    // Update is called once per frame
    void Update()
    {

        playerposition = GameObject.FindGameObjectWithTag("player").transform.position;

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //Camera Rotation System

        Quaternion playerRotation = playerCam.transform.rotation;
        playerRotation.x = 0;
        playerRotation.z = 0;
        transform.rotation = playerRotation;

        //Movement system
        Vector3 temp = rb.linearVelocity;

        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;

        ray.origin = transform.position;
        ray.direction = -transform.up;
        

        

        rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);

    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;
    }

    public void Shoot()
    {
        Instantiate(playerBullet1, playerposition, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killzone")
        {
            health = 0;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "healzone")
        {
            health += 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "healzone")
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((health < maxHealth) && (collision.gameObject.tag == "healLV1"))
        {
            health += 10;
            Destroy(collision.gameObject);
        }
        if ((health < maxHealth) && (collision.gameObject.tag == "healLV2"))
        {
            health += 20;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BasicEnemy")
        {
            health -= 10;
        }
    }

    public void Jump()
    {
        if(Physics.Raycast(ray, groundDetectLength))
        {
            

            rb.AddForce (transform.up * jumpHeight, ForceMode.Impulse);
        }
        
    }
}
