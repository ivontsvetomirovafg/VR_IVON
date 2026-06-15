using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float life;
    [SerializeField] 
    private float maxLife;
    private Animator animator;
    [SerializeField] 
    private Image lifeBar;

    [SerializeField] 
    private GameObject gameOverPanel;
    [SerializeField]
    private Animator gameOverAnim; 

    private bool isSlowed = false;
    private bool isDead = false; 

    [SerializeField] 
    private DynamicMoveProvider moveSpeed;
    private float originalSpeed;
                                     
    [SerializeField]
    private float iniciarPasos = 0.3f;
    [SerializeField]
    private InputActionReference stepsAction;   
    private bool steps;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxLife; 
        originalSpeed = moveSpeed.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //si está en performed que suene, si está en canceled que no suene 

        /*if (steps == false)
        {
            if (input.x >= iniciarPasos || input.x <= -iniciarPasos || input.y >= iniciarPasos || input.y <= -iniciarPasos)
            {
                steps = true;
                AudioManager.instance.PlaySteps(0.3f);
            }
        }
        else
        {
            if (input.x >= -iniciarPasos && input.x <= iniciarPasos && input.y >= -iniciarPasos && input.y <= iniciarPasos)
            {
                AudioManager.instance.StopSteps();
                steps = false;
            }
        }*/
    }

    public void TakePlayerDamage(float _damage)
    {
        if (isDead == true) 
        {
            return;
        } 
        life -= _damage;
        UpdateLife();

        if (life <= 0)
        {
            Die();
        }      
    }

    private void Die()
    {
        isDead = true;
        GetComponent<Collider>().enabled = false;
        
        gameOverPanel.SetActive(true);
        gameOverAnim.SetTrigger("Buttons");
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = life / maxLife;
    }

    public void Slow(float newSpeed, float duration)
    {
        if (isSlowed == true) 
        {
            return;  
        }
        StartCoroutine(SlowEffect(newSpeed, duration));
    }

    private IEnumerator SlowEffect(float newSpeed, float duration)
    {
        isSlowed = true;
        moveSpeed.moveSpeed = newSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed.moveSpeed = originalSpeed;
        isSlowed = false;
    }
}
