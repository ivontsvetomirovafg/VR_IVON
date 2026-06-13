using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource musicSource;
    private AudioSource ambientSource;
    [SerializeField]
    private GameObject SFXPrefab;
    private float musicVolume = 0.4f;
    private float sfxVolume = 1; 

    private AudioSource stepsSource;
    [SerializeField]
    private AudioClip sfxSteps;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        musicSource = gameObject.AddComponent<AudioSource>();
        ambientSource = gameObject.AddComponent<AudioSource>();
        stepsSource = gameObject.AddComponent<AudioSource>();  
        stepsSource.playOnAwake = false; 
    }
    public void PlayMusic(AudioClip _music)
    {
        if (musicSource.isPlaying) 
        {
            return; 
        }
        musicSource.clip = _music;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PlayAmbientSound(AudioClip _ambient)
    {
        ambientSource.clip = _ambient;
        ambientSource.volume = sfxVolume;
        ambientSource.Play();
    }
    public void StopAmbientSound()
    {
        ambientSource.Stop();
    }

    public void PlaySFX(AudioClip _sfx, Vector3 _position)
    {
        GameObject SFXClone = Instantiate(SFXPrefab, _position, Quaternion.identity);
        SFXClone.GetComponent<AudioSource>().clip = _sfx;
        SFXClone.GetComponent <AudioSource>().volume = sfxVolume;
        SFXClone.GetComponent <AudioSource>().Play(); 
        Destroy(SFXClone, _sfx.length);
    }
    
    public void SetMusicVolume(float _volume)
    {
        musicSource.volume = _volume;
    }

    public void PlaySteps(float _volume)
    {
        stepsSource.clip = sfxSteps;
        stepsSource.loop = true;
        stepsSource.volume = _volume;
        stepsSource.Play();
    }

    public void StopSteps()
    {
        stepsSource.Stop();
    }
}
