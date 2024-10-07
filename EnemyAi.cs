using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform targetPlayer;
    [SerializeField] private GameObject player;

    [SerializeField] private float health;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private Rigidbody rb;

    //patrolling
    //[SerializeField] private Vector3 walkPoint;
    //[SerializeField] private bool walkPointSet;
    //[SerializeField] private float walkPointRange;

    //attacking
    [SerializeField] float timeBetweenAttacks;
    private bool alreadyAttacked;
    [SerializeField] private GameObject projectile;

    //states
    [SerializeField] float sightRange;
    [SerializeField] float attackRange;
    [SerializeField] bool playerInSightRange;
    [SerializeField] bool playerInAttackRange;

    //waypoints
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
        //if (!playerInSightRange && !playerInAttackRange)
        //{
        //    Patroling();
        //}
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
        if (!playerInSightRange)
        {

            transform.position = Vector3.MoveTowards(transform.position, _aiWaypoints[currentWaypoint].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _aiWaypoints[currentWaypoint].position) < 0.1f)
            {
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    timer = 0f;
                    currentWaypoint = (currentWaypoint + 1) % _aiWaypoints.Count;
                }
            }
        }
    }
        private void ChasePlayer()
        {
            agent.SetDestination(targetPlayer.position);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            transform.LookAt(targetPlayer);

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
            health -= damage;

            if (health <= 0)
            {
                Invoke(nameof(DestroyEnemy), damage);
            }
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    //private void Patroling()
    //{
    //    if (!walkPointSet)
    //    {
    //        SearchWalkPoint();
    //    }

    //    if (walkPointSet)
    //    {
    //        agent.SetDestination(walkPoint);
    //    }

    //    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //    if (distanceToWalkPoint.magnitude < 1f)
    //    {
    //        walkPointSet = false;
    //    }
    //}

    //private void SearchWalkPoint()
    //{
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //    {
    //        walkPointSet = true;
    //    }

    //}
}
