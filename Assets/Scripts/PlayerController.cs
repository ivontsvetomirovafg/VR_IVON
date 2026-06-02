using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using System.Collections;

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
    [SerializeField] 
    private GameObject gameOver;
    [SerializeField] 
    private Image gameOverPanel;

    private bool isSlowed = false;
    private bool isDead = false; 

    [SerializeField] 
    private DynamicMoveProvider moveSpeed;
    private float originalSpeed;

    void Start()
    {
        life = maxLife; 
        originalSpeed = moveSpeed.moveSpeed;

        if (gameOverPanel != null)
        {
            Color color = gameOverPanel.color;
            color.a = 0f;
            gameOverPanel.color = color;
        }
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
        StartCoroutine(FadeIn());
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = life / maxLife;
    }

    private IEnumerator FadeIn()
    {
        float duration = 2f;
        float tiempoTranscurrido = 0f;
        Color color = gameOverPanel.color;

        while (tiempoTranscurrido < duration)
        {
            tiempoTranscurrido += Time.deltaTime;
            color.a = Mathf.Clamp01(tiempoTranscurrido / duration);
            gameOverPanel.color = color;
            yield return null;
        }

        color.a = 1f;
        gameOverPanel.color = color;
    }

    public void Slow(float newSpeed, float duration)
    {
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
