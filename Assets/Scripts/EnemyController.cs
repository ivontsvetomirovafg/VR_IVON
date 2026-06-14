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
    private bool playerDetected;
    private bool reloading;
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

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isDead == true) 
        {
            return;
        }

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime; //para bajar el coldown
        }     

        if (following == true)
        {
            agent.speed = speed;
            agent.stoppingDistance = 10;
            agent.SetDestination(player.position);
            float distance = (player.position - transform.position).magnitude;

            animator.SetBool("Run", true);
            animator.SetBool("Attack", false);

            if (distance <= 10)
            {
                if (attackTimer <= 0f)
                {
                    animator.SetBool("Attack", true);
                    animator.SetBool("Run", false);
                    transform.LookAt(player);
                    attackTimer = attackCooldown;
                }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 1.65f, 0), (player.position - transform.position).normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    following = true;
                }
            }
        }
    }

    public void TakeDamage(float _damage)
    {
        if (isDead == true) 
        {
            return;
        }

        life -= _damage;
        following = true;

        if (life <= 0)
        {
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
        
        Vector3 point = player.position + new Vector3(0, 1f, 0); //para que no dispare a los pies xd
        Vector3 direction = (point - bulletSpawnPoint.position).normalized;

        bulletClone.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
        BulletScript bala = bulletClone.GetComponent<BulletScript>(); 
        bala.damage = bulletDamage;
        bala.enemyBullet = true;
    }

    public void Reload()
    {
        reloading = true;
    }

    public void FinishReload()
    {
        reloading = false;
    }
}