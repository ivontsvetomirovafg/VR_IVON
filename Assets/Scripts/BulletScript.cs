using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
