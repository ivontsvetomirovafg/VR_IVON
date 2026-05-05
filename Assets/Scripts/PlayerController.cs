using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float life;
    [SerializeField]
    private float maxLife;
    [SerializeField]
    private float minLife;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakePlayerDamage(float _daamage)
    {

        life -= _daamage;

        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Collider>().enabled = false;
        //panel game over
    }
}
