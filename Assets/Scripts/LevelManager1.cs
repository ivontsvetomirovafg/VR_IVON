using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR; 

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject panelPause;
    [SerializeField]
    private AudioClip musica;
    [SerializeField]
    private AudioClip buttonPausa;
    private bool botonPulsado;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        AudioManager.instance.PlayMusic(musica);
    }

    public void MainMenuButton()
    {
        //AudioManager.instance.StopMusic();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
    public void Pause()
    {
        if (panelPause.activeInHierarchy == false)
        {
            AudioManager.instance.PlaySFX(buttonPausa, transform.position);
            panelPause.SetActive(true);
        }
        else
        {
            panelPause.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
