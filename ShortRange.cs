using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ShortRange : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;
    [SerializeField] private float detectionRange = 10f;  
    [SerializeField] private float attackRange = 5f;  
    [SerializeField] private float moveSpeed = 3f;  

    [SerializeField] private Transform[] waypoints;  

    [SerializeField] private GameObject armorPackPrefab;  
    [SerializeField] private GameObject medkitPrefab;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private PlayerHealth health;

    //[SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navMeshAgent;  
    private int currentWaypointIndex = 0;  

    private bool isChasing = false;  
    private bool isAttacking = false;
    private float attackTimer = 0f;
    [SerializeField] private float attackCooldown = 2f;  

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            SetDestination(waypoints[currentWaypointIndex].position);
        }
        currentHealth = maxHealth;
    }

    private void Update()
    {
        var test = FindObjectOfType<PlayerHealth>();
        //health = GetComponent<PlayerHealth>();
        if (!test)
            return;


        player = test.gameObject;

        target = player.transform;

        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            Patrol();
        }

        if (isChasing && distanceToPlayer <= attackRange && !isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown;
            }
        }
    }

    private void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
        navMeshAgent.isStopped = false;

        //animator.SetBool("IsRunning", true);
    }

    private void ChasePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > attackRange)
        {
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.isStopped = false;
            //animator.SetBool("IsRunning", true);
        }
        else
        {
            navMeshAgent.isStopped = true;
            //animator.SetBool("IsRunning", false);
        }
    }

    private void Attack()
    {
        //animator.SetTrigger("Attack");
        isAttacking = true;
        health.TakeDamage();
        navMeshAgent.isStopped = true;
        Debug.Log("enemy shot");
    }

    private void FinishAttack()
    {
        isAttacking = false;
        navMeshAgent.isStopped = false;
    }
    private void Patrol()
    {
        if (waypoints.Length == 0)
            return;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;  

            SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    public void TakeDamage(int damage)
    {
        damage = 1;
        currentHealth -= damage;
        Debug.Log("taken health off");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        int dropType = Random.Range(0, 3);  //0: niks, 1: Armor Pack, 2: Medkit

        switch (dropType)
        {
            case 1:
                Instantiate(armorPackPrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(medkitPrefab, transform.position, Quaternion.identity);
                break;
            default:
                //niks
                break;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}