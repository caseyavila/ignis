using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VentController : MonoBehaviour
{
    public float forceStrength = 10f; // Adjustable force strength

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (other.gameObject.CompareTag("Lamp"))
        {
            rb.AddForce(Vector3.up * forceStrength);
            Debug.Log($"{other.gameObject.name} is being pushed upward by the vent.");
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
                Debug.Log($"{other.gameObject.name} is being pushed upward by the vent.");
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
