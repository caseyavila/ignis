using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class VentController : MonoBehaviour
{
    public float forceStrength = 10f; // Adjustable force strength

    public float onDuration = 1f;
    public float offDuration = 1f;

    public bool ventOn = true;

    public float timer = 0;

    public ParticleSystem smokeEffect;

    private Collider ventCollider;

    void Start(){

        ventCollider = GetComponent<Collider>();

    }

    void Update(){

        timer += Time.deltaTime;

        if (ventOn){

            if (timer > onDuration){

                timer = 0f;
                ventCollider.enabled = false;
                ToggleSmoke(false);
                ventOn = false;

            }

        }else if (!ventOn){

            if (timer > offDuration){
                timer = 0f;
                ventCollider.enabled = true;
                ToggleSmoke(true);
                ventOn = true;
                
            }

        }

        
    }

    void ToggleSmoke(bool state)
    {
        if (smokeEffect != null)
        {
            var emission = smokeEffect.emission;
            emission.enabled = state;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (other.gameObject.CompareTag("Lamp"))
        {
            rb.AddForce(Vector3.up * forceStrength);

        }
        else if (other.gameObject.CompareTag("Candle"))
        {
            if (other.gameObject.GetComponent<FlameController>() != null)
            {
                // If the candle is not a lamp, restart the game
                StartCoroutine(Restart());
            }
            else
            {
                rb.AddForce(Vector3.up * forceStrength);

            }
        }
    }

    private IEnumerator Restart()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
