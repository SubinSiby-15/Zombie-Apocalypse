using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 30f;
   
    
    public void Shoot()
    {
        TakeDamage();
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity =transform.forward * bulletSpeed;

        Destroy(bullet, 3f);
    }

   public void AimAtEnemy(Transform enemy)
    {
        if (enemy == null) return;
        Vector3 direction =
    (enemy.position - firePoint.position).normalized;

        Rigidbody rb = bulletPrefab.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * bulletSpeed;

    }

    void TakeDamage()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position,
                            Camera.main.transform.forward,
                            out hit, 100f))
        {
            ZombieHealth zombie = hit.collider.GetComponent<ZombieHealth>();

            if (zombie != null)
            {
                zombie.TakeDamage(25);
            }
        }
    }


}