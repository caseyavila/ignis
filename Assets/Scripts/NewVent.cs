using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class AirflowController : MonoBehaviour
{
    public float airflowStrength = 10f; // Adjustable force strength

    public bool isActive = true;

    public ParticleSystem mistEffect;

    private Collider airflowCollider;

    public MusicManager soundManager;

    private HashSet<GameObject> affectedObjects = new HashSet<GameObject>(); //used to make sure whoosh sound effect is only played once

    void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
    }

    void Start()
    {
        airflowCollider = GetComponent<Collider>();
        airflowCollider.enabled = isActive;
        ToggleMist(isActive);
        soundManager.PlayWind(isActive);
    }

    public void ToggleMist(bool state)
    {
        if (mistEffect != null)
        {
            var emission = mistEffect.emission;
            emission.enabled = state;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null) // Ensure the object has a Rigidbody
        {
            if (!affectedObjects.Contains(other.gameObject))
            {
                soundManager.PlayQuietSFX(soundManager.whoosh);
                affectedObjects.Add(other.gameObject);
            }

            Vector3 airflowDirection = transform.up * airflowStrength; // Use the airflow's local up direction

            if (other.gameObject.CompareTag("Lamp"))
            {
                rb.AddForce(airflowDirection);
                Debug.Log($"Applying force {airflowDirection} to Lamp {other.gameObject.name}");
            }
            else if (other.gameObject.CompareTag("Candle"))
            {
                if (other.gameObject.GetComponent<FlameController>() != null)
                {
                    StartCoroutine(ReloadScene());
                }
                else
                {
                    rb.AddForce(airflowDirection);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove object from set when it exits the trigger so it can trigger the sound again next time
        affectedObjects.Remove(other.gameObject);
    }

    private IEnumerator ReloadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
