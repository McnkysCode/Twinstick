using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossAi : MonoBehaviour
{
    [Header("stats")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private GameObject player;
   
    [SerializeField] private int health;
    [SerializeField] private int currentHealth;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int scenenumber;

    [Header("Layers")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("attacking")]
    [SerializeField] float timeBetweenAttacks;
    private bool alreadyAttacked;
    [SerializeField] private GameObject projectile;
  
        
    [Header("boss states")]
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    [SerializeField] bool playerInSightRange;
    [SerializeField] bool playerInAttackRange;

    [Header("waypoints")]
    [SerializeField] private float MaxVelocity = 5;
    private int currentWaypointIndex = 0;
    public List<Transform> AssignedWaypointsAI = new List<Transform>();
    private List<Transform> _aiWaypoints = new List<Transform>();
    [SerializeField] private float waitTime = 2f;

    [SerializeField] private int currentWaypoint = 0;
    [SerializeField] private float timer = 0f;

    [SerializeField] private float speed = 7f;

    private void Awake()
    {
        currentHealth = health;
        agent = GetComponent<NavMeshAgent>();

        foreach(Transform trans in AssignedWaypointsAI)
        {
            Transform point = GameObject.Find(trans.name).transform;
            _aiWaypoints.Add(point);
        }

    }

    private void Update()
    {
        var test = FindObjectOfType<clonePickUp>();

        if (!test)
            return;


        player = test.gameObject;

        targetPlayer = player.transform;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVelocity);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
       
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
            transform.position = Vector3.MoveTowards(transform.position, _aiWaypoints[currentWaypoint].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _aiWaypoints[currentWaypoint].position) < 1.7f)
            {
                    currentWaypoint = (currentWaypoint + 1) % _aiWaypoints.Count;
                
            }
        }
        if (!playerInSightRange)
        {

            transform.position = Vector3.MoveTowards(transform.position, _aiWaypoints[currentWaypoint].position, speed * Time.deltaTime);
            //Debug.Log("distance to wp " + Vector3.Distance(transform.position, _aiWaypoints[currentWaypoint].position));
            if (Vector3.Distance(transform.position, _aiWaypoints[currentWaypoint].position) < 1.7f)
            {
               currentWaypoint = (currentWaypoint + 1) % _aiWaypoints.Count;

            }
        }
    }
        

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

        //transform.LookAt(targetPlayer);


        if (!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

                rb.AddForce(transform.forward * 32, ForceMode.Impulse);
                rb.AddForce(transform.up * 1, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
          
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("damage");
            currentHealth -= damage;
           
            if (currentHealth <= 0)
            {
                Invoke(nameof(DestroyEnemy), damage);
            }
        }

        private void DestroyEnemy()
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scenenumber);
          Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
}
