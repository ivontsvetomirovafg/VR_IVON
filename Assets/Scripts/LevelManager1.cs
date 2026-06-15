using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField]
    private InputActionReference buttonA;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        buttonA.action.started += Pause;
        Debug.Log("Awake");
    }

    void Start()
    {
        AudioManager.instance.PlayMusic(musica);
    }

    private void Update()
    {
        Debug.Log(buttonA.action.ReadValue<bool>());
    }

    public void MainMenuButton()
    {
        //AudioManager.instance.StopMusic();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    
    public void Pause (InputAction.CallbackContext context)
    {
        Debug.Log("Entra en pausa");
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
