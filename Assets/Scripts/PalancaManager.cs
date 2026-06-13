using UnityEngine;

public class PalancaManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleScipt puzzle;   
    private bool activada;     

    void Update()
    {
        Debug.Log(transform.localEulerAngles.y); 
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
