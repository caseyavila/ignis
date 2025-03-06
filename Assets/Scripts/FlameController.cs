using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class FlameController : MonoBehaviour
{
    public float flickerSpeed;
    /* If null, flame is an "ember" */
    public GameObject candle;
    public float speed;
    public float shrinkTime;
    public AnimationCurve shrinkCurve;

    private Rigidbody rb;
    private Vector3 defaultScale;
    private Vector3 currentScale;
    private InputAction moveAction;
    private InputAction emberAction;

    MusicManager audioManager;

    void Awake()
    {
    
       audioManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
    }

    void Start()
    {
        defaultScale = transform.localScale;
        currentScale = transform.localScale;
        moveAction = InputSystem.actions.FindAction("Move");
        emberAction = InputSystem.actions.FindAction("Ember");
        rb = GetComponent<Rigidbody>();

        candle.GetComponent<CandleController>().lit = true;
    }

    void Update()
    {
        Move();
        Ember();
        Flicker();
    }


    void OnTriggerEnter(Collider other) {
        if (candle == null && other.gameObject.tag == "Candle") {
            candle = other.gameObject;
            other.gameObject.GetComponent<CandleController>().lit = true;
            currentScale = defaultScale;

            audioManager.PlaySFX(audioManager.ignite);

        }else if (candle == null && other.gameObject.tag == "Chandelier"){
            candle = other.gameObject;
            other.gameObject.GetComponent<ChandelierController>().lit = true;
            currentScale = defaultScale;
            audioManager.PlaySFX(audioManager.ignite);

        }else if (candle == null && other.gameObject.tag == "Fireplace"){
            candle = other.gameObject;
            other.gameObject.GetComponent<FireplaceController>().lit = true;
            currentScale = defaultScale;
            audioManager.PlaySFX(audioManager.ignite);

        }else if (candle == null && other.gameObject.tag == "Lamp"){
            candle = other.gameObject;
            other.gameObject.GetComponent<LampController>().lit = true;
            currentScale = defaultScale;
            audioManager.PlaySFX(audioManager.ignite);
        }else if (other.CompareTag("WaterDrop"))
        {
            StartCoroutine(Restart());
            
        }else if ((candle == null || !candle.CompareTag("Lamp")) && other.CompareTag("Wind")){

            StartCoroutine(Restart());

        }
    }

    private void Flicker()
    {
        transform.localScale = currentScale +
            (Vector3.up * 0.25f * (Mathf.PerlinNoise(0, Time.time * flickerSpeed) - 0.5f));
    }

    private void Ember()
    {
        if (emberAction.triggered) {
            audioManager.PlaySFX(audioManager.sizzle);
            if (candle != null) {
                Vector3 velocity = rb.linearVelocity;
                velocity.y = 5;
                rb.linearVelocity = velocity;

                if (candle.tag == "Candle"){

                    candle.GetComponent<CandleController>().lit = false;

                }else if (candle.tag == "Chandelier"){
 
                    candle.GetComponent<ChandelierController>().lit = false;
                }else if (candle.tag == "Lamp"){
 
                    candle.GetComponent<LampController>().lit = false;
                }

                candle = null;

            }
        }
    }

    private void Move()
    {
        if (candle != null) {
            rb.linearVelocity = Vector3.zero;

            if (candle.tag == "Candle"){
                transform.position = candle.transform.position + new Vector3(0f, 0.33f, 0f);
            }else if (candle.tag == "Chandelier"){
                Transform wickPosition = candle.GetComponent<ChandelierController>().wickPosition;
                transform.position = wickPosition.position + new Vector3(0f, 0.2f, 0f);
            }else if (candle.tag == "Fireplace"){
                Transform logPosition = candle.GetComponent<FireplaceController>().logPosition;
                transform.position = logPosition.position + new Vector3(0f, 0.25f, 0f);
            }else if (candle.tag == "Lamp"){
                Transform wickPosition = candle.GetComponent<LampController>().wickPosition;
                transform.position = wickPosition.position + new Vector3(0f, 0.2f, 0f);
            }

        } else {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            Vector3 velocity = rb.linearVelocity;
            velocity.x = moveInput.x * speed;

            rb.linearVelocity = velocity;

            StartCoroutine(Shrink());
        }
    }

    private IEnumerator Shrink()
    {
        float time = 0;
        Vector3 startScale = transform.localScale;

        while (time < shrinkTime) {
            if (candle != null) {
                yield break;
            }

            currentScale = Vector3.Lerp(startScale, Vector3.zero, shrinkCurve.Evaluate(time / shrinkTime));
            Flicker();
            time += Time.deltaTime;
            yield return null;
        }

        
        StartCoroutine(Restart());
        
        yield break;
    }


    IEnumerator Restart() {

        GetComponent<Renderer>().enabled = false;
        if (candle != null) {
            if (candle.tag == "Candle"){

                candle.GetComponent<CandleController>().lit = false;

            }else if (candle.tag == "Chandelier"){
    
                candle.GetComponent<ChandelierController>().lit = false;

            }else if (candle.tag == "Lamp"){
    
                candle.GetComponent<LampController>().lit = false;
                
            }
        }

        

        yield return new WaitForSeconds(1);


        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}

