using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 playerposition;

   

    Camera playerCam;

    Ray ray;
    Ray interactRay;
    RaycastHit interactHit;
    GameObject pickupObj;
    RaycastHit hit;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon;

    Rigidbody rb;

    float verticalMove;
    float horizontalMove;

    public float speed = 5f;
    public float jumpHeight = 10f;
    public float groundDetectLength = 0.5f;
    public float interactDistance = 1f;

    public int health = 100;
    public int maxHealth = 100;

    public bool attacking = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        input = GetComponent<PlayerInput>();
        ray = new Ray(transform.position, transform.forward);
        interactRay = new Ray(transform.position, transform.forward);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        weaponSlot = playerCam.transform.GetChild(0);

        
    }

    // Update is called once per frame
    void Update()
    {
    

        playerposition = GameObject.FindGameObjectWithTag("player").transform.position;


        
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

        interactRay.origin = playerCam.transform.position;
        interactRay.direction = playerCam.transform.forward;

        if (Physics.Raycast(interactRay, out interactHit, interactDistance))
        {
            if (interactHit.collider.gameObject.tag == "weapon")
            {
                pickupObj = interactHit.collider.gameObject;
            }
        }
        else
            pickupObj = null;

        if(currentWeapon)
        {
            if (currentWeapon.holdToAttack && attacking && currentWeapon)
            {
                currentWeapon.fire();
            }
        }
        
        

        

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
        if (collision.gameObject.tag == "LevelTransition")
        {
            SceneManager.LoadScene(2);
        }
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
        if (collision.gameObject.tag == "Spike")
        {
            health -= 20;
        }
    }

    public void Jump()
    {
        if(Physics.Raycast(ray, groundDetectLength))
        {
            rb.AddForce (transform.up * jumpHeight, ForceMode.Impulse);
        }
        
    }
    public void fireModeSwitch()
    {
        if(currentWeapon.weaponID == 1)
        {
            currentWeapon.GetComponent<Rifle>().changeFireMode();
        }

        if (currentWeapon.weaponID == 2)
        {
            currentWeapon.GetComponent<ShotGun>().changeFireMode();
        }
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if(currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }
            else
                if (context.ReadValueAsButton())
                currentWeapon.fire();
        }
    }

    public void Reload()
    {
        if(currentWeapon)
            if(!currentWeapon.reloading)
                currentWeapon.reload();
    }

    public void Interact()
    {
        if (pickupObj)
        {
            if (pickupObj.tag == "weapon")
                pickupObj.GetComponent<Weapon>().equip(this);

            pickupObj = null;
        }
        else
            Reload();
    }

    public void DropWeapon()
    {
        if(currentWeapon)
        {
            currentWeapon.GetComponent<Weapon>().unequip();
        }
    }
   
}
