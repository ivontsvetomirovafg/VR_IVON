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

    [SerializeField] 
    private DynamicMoveProvider moveSpeed;

    [SerializeField]
    private AudioSource breathSource;   
    [SerializeField]
    private AudioClip breathSFX;    

    private bool isSlowed = false;
    public bool isDead = false; 
    private float originalSpeed;
                                     
    [SerializeField]
    private InputActionReference stepsAction;   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxLife; 
        originalSpeed = moveSpeed.moveSpeed;

        breathSource.clip = breathSFX;  
        breathSource.loop = true;    
        breathSource.Play(); 
    }

    /*private void OnEnable()
    {
        if (stepsAction == null || stepsAction.action == null) 
        {
            return;
        }
        stepsAction.action.started  += EmpezarPasos;  
        stepsAction.action.canceled += PararPasos;
        stepsAction.action.Enable();
    }

    private void OnDisable()
    {
        if (stepsAction == null || stepsAction.action == null) 
        {
            return;
        }
        stepsAction.action.started  -= EmpezarPasos;
        stepsAction.action.canceled -= PararPasos;
    }
    private void EmpezarPasos(InputAction.CallbackContext context)
    {
        Debug.Log("paso start");

        if (isDead == true) 
        {
            return;
        }
        AudioManager.instance.PlaySteps(0.3f);
    }

    private void PararPasos(InputAction.CallbackContext context)
    {
        Debug.Log("paso stopp");
        AudioManager.instance.StopSteps();
    }*/

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
        AudioManager.instance.StopMusic();
        breathSource.Stop();

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
