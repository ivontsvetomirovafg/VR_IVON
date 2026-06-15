using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel; 
    [SerializeField]
    private Animator puertaGaraje; 
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private AudioClip win; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.StopMusic();          
            AudioManager.instance.PlaySFX(win, transform.position);
            winPanel.SetActive(true);
            puertaGaraje.SetTrigger("Close");
            player.enabled = false;
        }    
    }
}
