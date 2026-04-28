using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPlay, buttonExit;
    public void PlayButton()
    {
        StartCoroutine(CargarSceneAsync());
        buttonPlay.SetActive(false);
        buttonExit.SetActive(false);
    }

    public void ExitButton() 
    { 
        Application.Quit();
    }

    IEnumerator CargarSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Mapa1");
        //asyncLoad.allowSceneActivation = false;
        while (asyncLoad.isDone == false)
        {
            //asyncLoad.progress //Para ver el progreso y poder hacer una barra de carga
            yield return null;
        }
        
        //asyncLoad.allowSceneActivation = true;
    }
}
