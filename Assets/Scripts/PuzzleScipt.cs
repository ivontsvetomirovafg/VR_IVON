using UnityEngine;

public class PuzzleScipt : MonoBehaviour
{
    [Header("Puzzle 1")]
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private GameObject[] luces;
    [SerializeField]
    private Animator garaje;
    [SerializeField]
    private Material redMaterial;
    [SerializeField]
    private bool puzzle1;

    [Header("Puzzle 2")]
    [SerializeField]
    private Animator garaje2;
    [SerializeField]
    private GameObject[] palancas;
    [SerializeField]
    private bool puzzle2;

    void OnTriggerEnter(Collider other)
    {
        if (puzzle1 == false)
        {
            return;
        }
        mesh.material = redMaterial;

        foreach (GameObject luz in luces)
        {
            luz.SetActive(true);
        }
        garaje.SetTrigger("Open");
    }

    public void PalancasPuzzle()
    {
        if (puzzle2 == true)
        {
            return;
        }

        foreach (GameObject palancaObj in palancas)
        {
            /*Palancas palanca = palancaObj.GetComponent<Palancas>();

            if (palanca.activated == false)
            {
                return;
            }*/
        }
        puzzle2 = true;
        garaje2.SetTrigger("Open");
    }
}
