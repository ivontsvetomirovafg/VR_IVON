using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<ZombieController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
