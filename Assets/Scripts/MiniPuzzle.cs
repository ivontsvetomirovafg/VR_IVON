using UnityEngine;

public class MiniPuzzle : MonoBehaviour
{
    [Header("MiniPuzzle")]
    [SerializeField]
    private Animator puerta;
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private AudioClip button;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ball)
        {
            AudioManager.instance.PlaySFX(button, transform.position);
            puerta.SetTrigger("Open");     
        }
    }
}
