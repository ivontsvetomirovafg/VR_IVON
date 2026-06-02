using UnityEngine;

public class MiniPuzzle : MonoBehaviour
{
    [Header("MiniPuzzle")]
    [SerializeField]
    private Animator puerta;
    [SerializeField]
    private GameObject ball;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ball)
        {
            puerta.SetTrigger("Open");     
        }
    }
}
