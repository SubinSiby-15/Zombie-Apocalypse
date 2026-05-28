using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Zombie Settings")]
    public float detectRange = 15f;
    public float attackRange = 2f;
    public float chaseRange = 15f;
    
    [Header("Components")]
    public NavMeshAgent agent;
    public Animator animator;

    private bool isAttacking;


    void Start()
    {
        // Auto get components
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position , player.position);

        // Rotate towards player
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime * 5f);
        }

        // ATTACK FIRST
        if (distance <= attackRange)
        {
            AttackPlayer();
        }
        // THEN CHASE
        else if (distance <= chaseRange)
        {
            ChasePlayer();
        }
        // IDLE
        else
        {
            DetectPlayer();
        }
    }

    void ChasePlayer()
    {
        isAttacking = false;
        agent.speed = 3.5f;
        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }

        animator.SetBool("Walk", false);
        animator.SetBool("Running", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Attacking", false);
    }

    void AttackPlayer()
    {
        if (isAttacking) return;

        isAttacking = true;

        agent.isStopped = true;

        animator.SetBool("Attack", true);
        animator.SetBool("Attacking", true);
        animator.SetBool("Running", false);
        animator.SetBool("Walk", false);
    }

    void DetectPlayer()
    {
        isAttacking = false;
        agent.speed = 0.5f;
        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        animator.SetBool("Running", false);
        animator.SetBool("Walk", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Attacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        // Chase Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Detect Range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}