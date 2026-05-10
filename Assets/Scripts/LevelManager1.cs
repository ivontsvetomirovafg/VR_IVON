using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField] 
    private GameObject panelPause;
    /*[SerializeField]
    private AudioClip musica;*/

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        //AudioManager.instance.PlayMusic(musica);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
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
