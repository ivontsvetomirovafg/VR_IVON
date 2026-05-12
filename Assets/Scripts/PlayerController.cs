using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float life;
    [SerializeField]
    private float maxLife;
    [SerializeField]
    private float minLife;
    private Animator animator;
    [SerializeField]
    private Image lifeBar;

    private bool isSlowed = false;

    [SerializeField]
    private DynamicMoveProvider moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //moveSpeed =
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
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

    public void UpdateLife()
    {
        lifeBar.fillAmount = life / maxLife;
    }

    public void Slow(float newSpeed, float duration) 
    {
        if (isSlowed == false)
        {
            StartCoroutine(SlowEffect(newSpeed, duration));
        }
    }

    private System.Collections.IEnumerator SlowEffect(float newSpeed, float duration)
    {
        isSlowed = true;
        //moveSpeed
        yield return new WaitForSeconds(duration);
        //moveSpeed
        isSlowed = false;
    }
}
