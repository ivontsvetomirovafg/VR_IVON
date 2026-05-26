using UnityEngine;

public class PuzzleScipt : MonoBehaviour
{
    [Header("Puzzle 1")]
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private GameObject[] puertasGaraje;
    [SerializeField]
    private bool puzzle1;

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
        mesh.materials[3].SetColor("_EmissionColor", Color.green);
        foreach (GameObject puerta in puertasGaraje)
        {
            puerta.SetActive(false);
        }
        PalancasPuzzle();
    }

    public void PalancasPuzzle()
    {
        foreach (GameObject palanca in palancas)
        {
            if (palanca.transform.localEulerAngles.y <= 45f)
            {
                return;
            }
        }
        garaje2.SetTrigger("Open");
    }
}
