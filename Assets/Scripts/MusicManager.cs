using UnityEngine;
using System.Collections;


public class MusicManager : MonoBehaviour
{
    public static MusicManager instance = null;

    [Header("Audio Source")]
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource rainSource;
    [SerializeField] AudioSource fireSource;
    [SerializeField] AudioSource windSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource QuietSFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip rain;
    public AudioClip fireBackground;
    public AudioClip wind;
    public AudioClip ignite;
    public AudioClip sizzle;
    public AudioClip whoosh;
    public AudioClip squeak;
    public AudioClip drop;
    public AudioClip lightning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
        backgroundSource.clip = background;
        backgroundSource.loop = true;
        StartCoroutine(DelayedMusic());
        

        rainSource.clip = rain;
        rainSource.loop = true;
        rainSource.Play();

        fireSource.clip = fireBackground;
        fireSource.loop = true;
        
        windSource.clip = wind;
        windSource.loop = true;
        
    }

    
    public void PlaySFX(AudioClip clip)
    {

        SFXSource.PlayOneShot(clip);

    }

    public void PlayQuietSFX(AudioClip clip)
    {

        QuietSFXSource.PlayOneShot(clip);

    }

    public void PlayWind(bool toggle)
    {

        if (toggle){

            windSource.Play();

        }else{

            windSource.Stop();

        }

    }

    IEnumerator DelayedMusic()
    {
        yield return new WaitForSeconds(23f);
        
        backgroundSource.Play();
        fireSource.Play();

        
    }






 
}
