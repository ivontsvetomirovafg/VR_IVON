using UnityEngine;

public class MiniPuzzle : MonoBehaviour
{
    [Header("MiniPuzzle")]
    [SerializeField]
    private Animator puerta;
    [SerializeField]
    private AudioClip button;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AudioManager.instance.PlaySFX(button, transform.position);
            puerta.SetTrigger("Open");     
        }
    }
}
