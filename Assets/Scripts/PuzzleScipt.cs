using UnityEngine;
using System.Collections;

public class PuzzleScipt : MonoBehaviour
{
    [Header("Puzzle 1")]
    [SerializeField]
    private GameObject[] puertasGaraje;
    [SerializeField]
    private bool puzzle1 = true;
    [SerializeField]
    private Animator button;

    [Header("Puzzle 2")]
    [SerializeField]
    private Animator garaje2;
    [SerializeField]
    private GameObject[] palancas;
    private int palancasActivadas = 0;

    [SerializeField]
    private AudioClip buttonClick; 
    [SerializeField]
    private AudioClip palancaActivada; 
    [SerializeField]
    private AudioClip garajeSFX; 

    [Header("Cinemática")]
    [SerializeField]
    private GameObject puzzleCam;     
    [SerializeField]
    private float duracionCinematica;  
    //private bool puzzle2Completado = false;

    void OnTriggerEnter(Collider other)
    {
        if (puzzle1 == false)
        {
            return;
        }
        Debug.Log("Toco el boton");
        AudioManager.instance.PlaySFX(buttonClick, transform.position);
        button.SetTrigger("TouchButton");
        foreach (GameObject puerta in puertasGaraje)
        {
            Debug.Log("Se abre el garaje");
            puerta.SetActive(false);
            puzzle1 = false;    
        }
    }

    public void ActivarPalanca()
    {
        AudioManager.instance.PlaySFX(palancaActivada, transform.position);
        palancasActivadas++;

        if (palancasActivadas >= palancas.Length)
        {
            Debug.Log("Puzzle completado");
            garaje2.SetTrigger("Open");
            AudioManager.instance.PlaySFX(garajeSFX, transform.position);
            StartCoroutine(Cinematica());
        }
    }

    /*public void PalancasPuzzle()
    {
        if (puzzle2Completado == true)
        {
            return;
        }

        bool todasOkay = true;
        foreach (GameObject palanca in palancas)
        {
            if (palanca.transform.localEulerAngles.y < 45f)
            {
                todasOkay = false;
            }
        }

        if (todasOkay == true)
        {
            Debug.Log("Puzzle completado");
            garaje2.SetTrigger("Open");
            AudioManager.instance.PlaySFX(garajeSFX, transform.position);
            StartCoroutine(Cinematica());
        }
    }*/ 
    private IEnumerator Cinematica()
    {
        puzzleCam.SetActive(true);                   
        yield return new WaitForSeconds(duracionCinematica);   
        puzzleCam.SetActive(false);              
    }
}
