using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string sceneName;
    private static bool haungs;

    void Update() {
        if (GetComponent<Image>() == null) {
            return;
        }

        Debug.Log(haungs);

        if (haungs) {
            GetComponent<Image>().color = new Color32(130, 130, 130, 255);
        } else {
            GetComponent<Image>().color = new Color32(245, 245, 245, 255);
        }
    }

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
        Debug.Log("HI");
        int currUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (!haungs) {
            PlayerPrefs.SetInt("UnlockedLevel", 15);
            PlayerPrefs.SetInt("HaungsScratch", currUnlocked);
        } else {
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("HaungsScratch", 1));
        }

        PlayerPrefs.Save();
        haungs = !haungs;
    }

    IEnumerator NextScene() {
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
    

}
