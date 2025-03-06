using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OpenLevel(int levelId){
        string levelName = "Level " + levelId;
        StartCoroutine(NextScene(levelName));

    }
    IEnumerator NextScene(string levelName) {
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
