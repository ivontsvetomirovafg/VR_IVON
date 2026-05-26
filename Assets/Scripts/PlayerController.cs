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

    [SerializeField]
    private DynamicMoveProvider moveSpeed;
    private float originalSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSpeed = moveSpeed.moveSpeed;
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
        
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = life / maxLife;
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float alpha = 0f;
        Color colorImagen = gameOverPanel.color;
        while (alpha < 1)
        {
            alpha += 0.05f;
            colorImagen.a = alpha;
            gameOverPanel.color = colorImagen;
            yield return null;
        }
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
        moveSpeed.moveSpeed = newSpeed;
        yield return new WaitForSeconds(duration);
        
        moveSpeed.moveSpeed = originalSpeed;
        isSlowed = false;
    }
}
