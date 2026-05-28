using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageCooldown = 1f;

    private float timer;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer += Time.deltaTime;

            if (timer >= damageCooldown)
            {
                PlayerHealth health =
                    other.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(damage);
                }

                timer = 0f;
            }
        }
    }


}