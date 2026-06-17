using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] 
    private float speed;
    private Transform player;
    private NavMeshAgent agent;
    private bool following;
    [SerializeField] 
    private Transform[] patrolPoints;
    private int patrolIndex;
    [SerializeField] 
    private float life;
    [SerializeField] 
    private float attackCooldown = 1.5f;
    private float attackTimer;
    private bool isDead;

    [SerializeField] 
    private GameObject bulletPrefab;
    [SerializeField] 
    private Transform bulletSpawnPoint;
    [SerializeField] 
    private float bulletSpeed;
    [SerializeField] 
    private float bulletDamage;

    [SerializeField]
    private AudioClip shoot; 
    [SerializeField]
    private AudioClip zombieSFX;
    [SerializeField]
    private AudioClip deathSFX;
    [SerializeField]
    private AudioClip detect;
    [SerializeField]
    private AudioSource loopSource; 

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        loopSource.clip = zombieSFX;
        loopSource.loop = true;
        loopSource.Play();
    }

    void Update()
    {
        if (isDead == true) 
        {
            return;
        }
        if (player.GetComponent<PlayerController>().isDead == true)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            agent.isStopped = true;
            return;
        }

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime; //para bajar el coldown
        }

        if (following == true)
        {
            float distance = (player.position - transform.position).magnitude;

            if (distance <= 5)  
            {
                agent.isStopped = true;         
                transform.LookAt(player);        
                animator.SetBool("Run", false);

                if (attackTimer <= 0f)         
                {
                    animator.SetBool("Attack", true);
                    attackTimer = attackCooldown; 
                }
                else
                {
                    animator.SetBool("Attack", false);
                }
            }
            else  
            {
                agent.isStopped = false;
                agent.speed = speed;
                agent.SetDestination(player.position);
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            if (patrolPoints.Length > 0)
            {
                agent.speed = speed * 0.5f;
                agent.SetDestination(patrolPoints[patrolIndex].position);
                float distance = (patrolPoints[patrolIndex].position - transform.position).magnitude;
                if (distance < 1)
                {
                    patrolIndex += 1;
                    if (patrolIndex >= patrolPoints.Length)
                    {
                        patrolIndex = 0;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            DetectarPlayer(); 
        }
    }

    private void DetectarPlayer()
    {
        if (following == false) 
        {
            following = true;
            AudioManager.instance.PlaySFX(detect, transform.position);
        }
    }

    public void TakeDamage(float _damage)
    {
        if (isDead == true) 
        {
            return;
        }

        life -= _damage;
        DetectarPlayer();

        if (life <= 0)
        {
            animator.SetTrigger("Die");
            AudioManager.instance.PlaySFX(deathSFX, transform.position);
            isDead = true;
            agent.isStopped = true;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 3f);
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }
    public void Shoot()
    {
        if (isDead == true) 
        {
            return;
        }
        AudioManager.instance.PlaySFX(shoot, transform.position);
        GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Vector3 aimPoint = player.position + new Vector3(0, 1f, 0);   
        Vector3 direction = (aimPoint - bulletSpawnPoint.position).normalized;
        bulletClone.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;

        bulletClone.GetComponent<BulletScript>().damage = bulletDamage;
        bulletClone.GetComponent<BulletScript>().enemyBullet = true;  
    }
}