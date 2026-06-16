using System;
using TMPro; 
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    //cargador
    [SerializeField] 
    private Transform bulletSpawnPoint;
    [SerializeField]
    private float fireRate;

    private float timePass;
    [SerializeField]
    private float bulletSpeed;
    private VRMagazine cargador;
    private bool sliderCargado;

    [SerializeField]
    private Transform slider;
    [SerializeField] 
    private TextMeshProUGUI bulletText;  
    [SerializeField] 
    private int maxBullets = 15;
    [SerializeField]
    private AudioClip shoot;
    /*[SerializeField]
    private ParticleSystem shootFlash;*/

    [SerializeField]
    private AudioClip reload;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePass += Time.deltaTime;
        if (slider.localPosition.z <- 0.03)
        {
            Debug.Log("Hacer el slider");
            sliderCargado = true;
            //AudioManager.instance.PlaySFX(reload, transform.position);
        }

        if (cargador != null)
        {
            bulletText.text = cargador.bullets + "/" + maxBullets;
        }
        else
        {
            bulletText.text = "0/" + maxBullets;
        }
    }

    public void AddMagazine(SelectEnterEventArgs eventArgs)
    {
        Debug.Log("Meto el cargador");
        cargador = eventArgs.interactableObject.transform.GetComponent<VRMagazine>();
        sliderCargado = false;
    }
    public void RemoveMagazine(SelectExitEventArgs eventArgs)
    {
        Debug.Log("Saco el cargador");
        cargador = null;
        sliderCargado = false;
    }

    public void Shoot()
    {
        if (cargador != null && sliderCargado == true)
        {
            if (fireRate <= timePass && cargador.bullets >0)
            {
                Debug.Log("Bala se instancia");
                AudioManager.instance.PlaySFX(shoot, transform.position);
                //shootFlash.Play();   

                GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletClone.GetComponent<Rigidbody>().linearVelocity = bulletClone.transform.forward*bulletSpeed;
                cargador.bullets -= 1;
                timePass = 0;
            }
        }   
    }
}
