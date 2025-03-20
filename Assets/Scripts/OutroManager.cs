using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroManager : MonoBehaviour
{
    [SerializeField] GameObject Scene2;
    [SerializeField] GameObject Scene3;
    
    MusicManager audioManager;



    void Start()
    {

        StartCoroutine(Outro());
        
    }

    IEnumerator Outro()
    {
        yield return new WaitForSeconds(5f);
        Scene2.SetActive(true);
        yield return new WaitForSeconds(8f);
        Scene3.SetActive(true);
        yield return new WaitForSeconds(10f);
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu");

    }




}
