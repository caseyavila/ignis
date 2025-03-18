using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string sceneName;

    void Update() {
        if (GetComponent<Image>() == null) {
            return;
        }

        if (PlayerPrefs.GetInt("Haungs", 0) == 1) {
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
        int currUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (PlayerPrefs.GetInt("Haungs", 0) == 0) {
            PlayerPrefs.SetInt("UnlockedLevel", 15);
            PlayerPrefs.SetInt("HaungsScratch", currUnlocked);
            PlayerPrefs.SetInt("Haungs", 1);
        } else {
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("HaungsScratch", 1));
            PlayerPrefs.SetInt("Haungs", 0);
        }

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
