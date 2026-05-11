using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;

    [Header ("Zombie Kid")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float life;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    public float damage;
    [SerializeField]
    private float attackCooldown;

    private Transform targetPlayer;
    private float attackTimer;
    private bool Muerto;
    private bool crawling;
    private bool playerDetected;

    private PlayerController player;

    [Header ("Zombie Woman")]
    [SerializeField]
    private bool zombie2;
    [SerializeField] 
    private float slowSpeed;
    [SerializeField] 
    private float slowDuration;


    void Start()
    {
        life = 100f;
        gameObject.GetComponent<Collider>().enabled = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        player = FindObjectOfType<PlayerController>();
    }

    public void Update()
    {
        if (Muerto == true)
        {
            return;
        }

        if (playerDetected == true)
        {
            if (targetPlayer == null)
            {
                targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
                if (targetPlayer == null)
                {
                    return;
                }
            }

            agent.SetDestination(targetPlayer.position);
            agent.speed = speed;

            float distance = Vector3.Distance(transform.position, targetPlayer.position);

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                Attack();
            }
            else
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Run", true);
                agent.isStopped = false;
            }

            //cooldown

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            transform.LookAt(collision.gameObject.transform);
            animator.SetTrigger("Detect");
            Invoke("StartMoving", 2f);
        }
    }

    public void StartMoving()
    {
        playerDetected = true;
        animator.SetBool("Run", true);
    }

    private void Attack()
    {
        if (attackTimer > 0)
        {
            return;
        }
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);
        player.TakePlayerDamage(damage);
        
        if (zombie2 == true)
        {
            player.Slow(slowSpeed, slowDuration);
        }
        attackTimer = attackCooldown;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakePlayerDamage(damage);
        }
    }*/

    public void TakeDamage(float _damage)
    {
        Debug.Log("Recibe daño");

        life -= _damage;

        if (life <= 30 && crawling == false)
        {
            crawling = true;

            animator.SetBool("Attack", false);
            animator.SetTrigger("Crawl");
        }    

        if (life <= 0)
        {
            Die();
        }       
    }

    private void Die()
    {  
        Muerto = true;
        agent.isStopped = true;
        animator.SetTrigger("Die");

        GetComponent<Collider>().enabled = false;
    }
}