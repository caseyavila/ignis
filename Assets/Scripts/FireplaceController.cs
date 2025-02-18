using UnityEngine;
using UnityEngine.SceneManagement;

public class FireplaceController : MonoBehaviour
{

    public Transform logPosition; // log position
    [SerializeField] string sceneName;
    
  
    public bool lit;

    void Start()
    {
   

    }

    void Update()
    {

         if (lit)
        {
            Invoke("NextScene", 1f); // 1 second delay
        }

    }

    private void NextScene(){

        SceneManager.LoadSceneAsync(sceneName);

    }
  
}
