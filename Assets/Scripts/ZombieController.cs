using UnityEngine;
using System.Collections; 

public class ZombieController : MonoBehaviour
{
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;

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
    private Rigidbody rb;

    private PlayerController player;

    [SerializeField] 
    private bool zombie2;
    [SerializeField] 
    private float slowSpeed;
    [SerializeField] 
    private float slowDuration;

    [SerializeField]
    private AudioClip scream;
    [SerializeField]
    private AudioClip zombieSFX;
    [SerializeField]
    private AudioClip dead;
    [SerializeField]
    private AudioSource loopSource; 
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = 100f;
        gameObject.GetComponent<Collider>().enabled = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();

        loopSource.clip = zombieSFX;
        loopSource.loop = true;
        loopSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Muerto == true) 
        {
            return;
        }

        if (player.isDead == true)
        {
            agent.isStopped = true;
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            loopSource.Stop(); 
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

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }           
        }
    }

    private void OnTriggerEnter(Collider collision)
    {      
        if (Muerto == true) 
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            transform.LookAt(collision.gameObject.transform);
            animator.SetTrigger("Detect");
            Invoke("StartMoving", 2f);
            StartCoroutine(Scream());

            if (zombie2 == true)   
            {
                player.Slow(slowSpeed, slowDuration);
            }
        }
    }

    private IEnumerator Scream()
    {
        loopSource.Stop();                 
        loopSource.PlayOneShot(scream);    
        yield return new WaitForSeconds(5f);  
        loopSource.Play();               
    }

    public void StartMoving()
    {
        playerDetected = true;
        animator.SetBool("Run", true);
    }
    
    public void DealDamage()
    {
        if (Muerto == true) 
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if (distance <= attackRange)
        {
            player.TakePlayerDamage(damage);
        }
    }

    private void Attack()
    {
        if (attackTimer > 0)
        {
            return;
        }
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);
        attackTimer = attackCooldown;
    }

    public void TakeDamage(float _damage)
    {
        if (Muerto == true) 
        {
            return;
        }

        life -= _damage;
        Debug.Log("Recibe da�o");

        if (zombie2 == true)
        {
            if (life <= 30 && crawling == false)
            {
                crawling = true;
                animator.SetBool("Attack", false);
                animator.SetTrigger("Crawl");
                speed *= 0.4f;
            }
        }

        if (life <= 0)
        {
            Die();
        }     
    }

    private void Die()
    {
        //AudioManager.instance.PlaySFX(dead, transform.position);
        Muerto = true;
        agent.isStopped = true;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        animator.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 3f);
    }
}