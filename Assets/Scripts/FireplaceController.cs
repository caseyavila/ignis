using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireplaceController : MonoBehaviour
{

    public Transform logPosition; // log position
    [SerializeField] string sceneName;
    public bool lit;

    void Update()
    {
         if (lit)
        {
            StartCoroutine(NextScene());
        }
    }

    IEnumerator NextScene() {
        yield return new WaitForSeconds(1);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
