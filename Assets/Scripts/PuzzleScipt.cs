using UnityEngine;

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

    void OnTriggerEnter(Collider other)
    {
        if (puzzle1 == false)
        {
            return;
        }
        Debug.Log("Toco el boton");
        button.SetTrigger("TouchButton");
        mesh.materials[3].SetColor("_EmissionColor", Color.green); //hacer que funcione
        foreach (GameObject puerta in puertasGaraje)
        {
            Debug.Log("Se abre el garaje");
            puerta.SetActive(false);
            puzzle1 = false;    
        }
        PalancasPuzzle();
    }

    public void PalancasPuzzle()
    {
        bool todasOkay = true;
        foreach (GameObject palanca in palancas)
        {
            if (palanca.transform.localEulerAngles.y <= 45f)
            {
               todasOkay = false; 
            }
        }
        if (todasOkay == true)
        {
            garaje2.SetTrigger("Open");
        }
    }
}
