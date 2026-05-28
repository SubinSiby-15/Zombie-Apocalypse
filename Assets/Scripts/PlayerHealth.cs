using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Damage Effect")]
    public bool playerDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // =========================
    // TAKE DAMAGE
    // =========================
    public void TakeDamage(int damage)
    {
        if (playerDead) return;

        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        // Check death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // =========================
    // PLAYER DEATH
    // =========================
    void Die()
    {
        playerDead = true;

        Debug.Log("Player Died");

        // Disable player movement
        // GetComponent<PlayerMovement>().enabled = false;

        // Play death animation here

        // Restart game after 3 sec
        Invoke(nameof(RestartGame), 3f);
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}