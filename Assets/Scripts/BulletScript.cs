using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public bool enemyBullet;

    void Start()
    {
        Destroy(gameObject, 5f);   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enemyBullet == false)  
        {
            if (collision.gameObject.tag == "Enemy")
            {
                ZombieController zombie = collision.gameObject.GetComponent<ZombieController>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }

                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>(); 
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (enemyBullet == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().TakePlayerDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
