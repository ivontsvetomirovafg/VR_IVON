using UnityEngine;

public class PalancaManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleScipt puzzle;   
    private bool activada;     

    void Update()
    {
        if (activada == true) 
        {
            return;
        }

        if (transform.localEulerAngles.y <= 45f)
        {
            activada = true;
            puzzle.ActivarPalanca();
        }
    }
}
