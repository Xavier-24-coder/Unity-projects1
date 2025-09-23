using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public int healthEmax;

    public int healthE;

    public int moveToPlayerDist;

    public bool gotHit = false;

    public GameObject player;
    public GameObject enemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 enemyposition = enemy.transform.position;

        float distance = Vector3.Distance(playerposition, enemyposition);

        if(distance < moveToPlayerDist)
        {
            agent.destination = GameObject.Find("Player").transform.position;
        }
        if(distance > moveToPlayerDist * 1.5)
        {
            if(gotHit == false)
            {
                agent.destination = enemyposition;
                agent.destination = GameObject.Find("Player").transform.position;
            }
            
        }
        
        
            

        if (healthE <= 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "proj1P")
        {
            healthE -= 1;
            Destroy(other.gameObject);
            gotHit = true;
        }

        if (healthE >= healthEmax)
        {
            healthE = 5;
        }
    }
   
}
