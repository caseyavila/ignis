using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject Scene2;
    [SerializeField] GameObject Scene3;
    [SerializeField] GameObject Scene4;
    [SerializeField] GameObject Scene5;
    MusicManager audioManager;

    void Awake()
    {
       audioManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
    }


    void Start()
    {

        StartCoroutine(Intro());
        
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(5f);
        Scene2.SetActive(true);
        audioManager.PlaySFX(audioManager.lightning);
        yield return new WaitForSeconds(1f);
        Scene3.SetActive(true);
        yield return new WaitForSeconds(5f);
        Scene4.SetActive(true);
        yield return new WaitForSeconds(5f);
        Scene5.SetActive(true);
        yield return new WaitForSeconds(5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");

    }

    IEnumerator MenuScene() {
        

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    IEnumerator Wait(float time) {
        yield return new WaitForSeconds(time);

    }


}
