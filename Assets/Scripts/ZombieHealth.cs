using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float attackRange = 1.5f;
    public Transform player;

    private float attackTimer;
    public float attackCooldown = 1.5f;
    [HideInInspector]
    public ZombieWaveSpawner waveSpawner;

    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform;



    }
    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                Attack();
                attackTimer = 0f;
            }
        }
    }

    void Attack()
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);

            Debug.Log("Zombie Attacked Player");
        }
    }
}