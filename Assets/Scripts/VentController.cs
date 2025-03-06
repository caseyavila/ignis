using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
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

    MusicManager audioManager;


    private HashSet<GameObject> triggeredObjects = new HashSet<GameObject>(); //used to make sure whoosh sound effect is only played once

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
    }

    void Start()
    {
        ventCollider = GetComponent<Collider>();
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if (ventOn)
        {
            if (timer > onDuration)
            {
                timer = 0f;
                ventCollider.enabled = false;
                ToggleSmoke(false);
                ventOn = false;
                audioManager.PlayWind(false); 
            }
        }
        else
        {
            if (timer > offDuration)
            {
                timer = 0f;
                ventCollider.enabled = true;
                ToggleSmoke(true);
                ventOn = true;
                audioManager.PlayWind(true); 
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

        if (rb != null) // Ensure the object has a Rigidbody
        {
            if (!triggeredObjects.Contains(other.gameObject))
            {
                audioManager.PlayQuietSFX(audioManager.whoosh);
                triggeredObjects.Add(other.gameObject);
            }

            Vector3 ventDirection = transform.up * forceStrength; // Use the vent's local up direction

            if (other.gameObject.CompareTag("Lamp"))
            {
                rb.AddForce(ventDirection);
                Debug.Log($"Applying force {ventDirection} to Lamp {other.gameObject.name}");
            }
            else if (other.gameObject.CompareTag("Candle"))
            {
                if (other.gameObject.GetComponent<FlameController>() != null)
                {
                    StartCoroutine(Restart());
                }
                else
                {
                    rb.AddForce(ventDirection);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove object from set when it exits the trigger so it can trigger the sound again next time
        triggeredObjects.Remove(other.gameObject);
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
