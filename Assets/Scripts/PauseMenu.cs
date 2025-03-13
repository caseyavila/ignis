using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] string homeScene;

    [SerializeField] GameObject pauseButton;
    MusicManager audioManager;

    void Awake()
    {
       audioManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
    }
    public void Pause()
    {

        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        audioManager.PlaySFX(audioManager.ignite);
        Time.timeScale = 0;
        
        
    }

    public void Home()
    {

        StartCoroutine(NextScene());
        audioManager.PlaySFX(audioManager.sizzle);
        Time.timeScale = 1;

        
    }

    public void Restart()
    {

        StartCoroutine(RestartEnum());
        audioManager.PlaySFX(audioManager.sizzle);
        Time.timeScale = 1;

        
    }



    public void Resume()
    {
        pauseMenu.SetActive(false); 
        pauseButton.SetActive(true);
        audioManager.PlaySFX(audioManager.ignite);
        Time.timeScale = 1;
        
    }


    IEnumerator NextScene() {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(homeScene);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    IEnumerator RestartEnum() {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while (!asyncLoad.isDone) {
            yield return null;
        }

    } 

}
