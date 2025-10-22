using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    Animator myAnim;
    NavMeshAgent agent;
    public int healthEmax;

    public int healthE;

    
    public int moveToPlayerDist;
    public int AttackDist;
    Vector3 destination;

    public bool gotHit;
    

    public GameObject player;
    public GameObject enemy;
    public GameObject HealthDrop;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 enemyposition = enemy.transform.position;

        float distance = Vector3.Distance(playerposition, enemyposition);

        destination = GameObject.Find("player").transform.position;
        agent.destination = destination;

        if (gotHit = false && distance < moveToPlayerDist)
        {
            
            agent.isStopped = false;
            
        }
        if (distance > moveToPlayerDist * 1.75)
        {
            agent.isStopped = true;
        }
     
        if (distance < AttackDist)
        {
            myAnim.SetBool("isAttacking", true);
        }
        if (distance > AttackDist)
        {
            myAnim.SetBool("isAttacking", false);
        }
        

        if (gotHit == true)
        {
            agent.destination = GameObject.Find("player").transform.position;
            agent.isStopped = false;
        }


        if (healthE <= 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "proj1P")
        {
            healthE -= 8;
            Destroy(collision.gameObject);
            gotHit = true;
        }
        if(collision.gameObject.tag == "proj2P")
        {
            healthE -= 4;
            Destroy(collision.gameObject);
            gotHit = true;
        }

        if (collision.gameObject.tag == "proj3P")
        {
            healthE -= 3;
            Destroy(collision.gameObject);
            gotHit = true;
        }

        if (healthE >= healthEmax)
        {
            healthE = healthEmax;
        }
    }
   
}
