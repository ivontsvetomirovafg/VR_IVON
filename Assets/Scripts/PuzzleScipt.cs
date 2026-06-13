using UnityEngine;
using System.Collections;

public class PuzzleScipt : MonoBehaviour
{
    [Header("Puzzle 1")]
    [SerializeField]
    private MeshRenderer mesh;
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

    [SerializeField]
    private AudioClip buttonClick; 
    [SerializeField]
    private AudioClip palancaActivada; 

    [Header("Cinemática")]
    [SerializeField]
    private GameObject puzzleCam;     
    [SerializeField]
    private float duracionCinematica;  
    private bool puzzle2Completado;

    void OnTriggerEnter(Collider other)
    {
        if (puzzle1 == false)
        {
            return;
        }
        Debug.Log("Toco el boton");
        AudioManager.instance.PlaySFX(buttonClick, transform.position);
        button.SetTrigger("TouchButton");
        mesh.materials[3].SetColor("_EmissionColor", Color.green); //hacer que funcione
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
        PalancasPuzzle(); 
    }

    public void PalancasPuzzle()
    {
        if (puzzle2Completado == true)
        {
            return;
        }

        bool todasOkay = true;
        foreach (GameObject palanca in palancas)
        {
            if (palanca.transform.localEulerAngles.y > 45f)  
            {
                todasOkay = false;
            }
        }

        if (todasOkay == true)
        {
            puzzle2Completado = true;
            garaje2.SetTrigger("Open");
            StartCoroutine(Cinematica());
        }
    }
    private IEnumerator Cinematica()
    {
        puzzleCam.SetActive(true);                   
        yield return new WaitForSeconds(duracionCinematica);   
        puzzleCam.SetActive(false);          
    }
}
