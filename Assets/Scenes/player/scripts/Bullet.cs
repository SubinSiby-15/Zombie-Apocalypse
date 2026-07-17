using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("zombie"))
        {
            print("hit" + collision.gameObject.name + "!");
            Destroy(gameObject);
        }
    }
}
