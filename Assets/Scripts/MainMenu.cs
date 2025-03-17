using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    [SerializeField] string sceneName;
    
    public void PlayGame()
    {
        StartCoroutine(NextScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HaungsMode()
    {
        PlayerPrefs.SetInt("ReachedIndex", 15);
        PlayerPrefs.SetInt("UnlockedLevel", 15);
        PlayerPrefs.Save();
    }

    IEnumerator NextScene() {
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
    

}
