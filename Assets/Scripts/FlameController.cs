using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

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
        Flicker();
        Move();
        Ember();
    }

    void OnTriggerEnter(Collider other) {
        if (candle == null && other.gameObject.tag == "Candle") {
            candle = other.gameObject;
            other.gameObject.GetComponent<CandleController>().lit = true;
            currentScale = defaultScale;
        }else if (other.gameObject.tag == "Chandelier"){
            candle = other.gameObject;
            other.gameObject.GetComponent<ChandelierController>().lit = true;
            currentScale = defaultScale;

        }
        if (other.CompareTag("WaterDrop"))
        {
            Destroy(gameObject);
        }
    }

    private void Flicker()
    {
        transform.localScale = currentScale +
            (Vector3.one * 0.25f * (Mathf.PerlinNoise(0, Time.time * flickerSpeed) - 0.5f));
    }

    private void Ember()
    {
        
        if (emberAction.triggered) {
            if (candle != null) {
                
                Rigidbody candleRb = candle.GetComponent<Rigidbody>();

                if (candle.tag == "Candle"){
                    Vector3 velocity = rb.linearVelocity;
                    velocity.y = 5;
                    rb.linearVelocity = velocity;

                    candle.GetComponent<CandleController>().lit = false;

                }else if (candle.tag == "Chandelier"){
                    // Get the angular velocity (rotational velocity) of the candle
                    Vector3 angularVelocity = candleRb.angularVelocity;

                    // Get the position of the wick or flame in relation to the candle's position
                    Transform wickPosition = candle.GetComponent<ChandelierController>().wickPosition;
                    Vector3 wickOffset = wickPosition.position - candle.transform.position;
                    Debug.Log("Wick Offset: " + wickOffset);

                    // Calculate the velocity at the end of the rotating arm using angular velocity
                    Vector3 rotationalVelocity = Vector3.Cross(angularVelocity, wickOffset);
                    Debug.Log("Rotational Velocity: " + rotationalVelocity);

                    // Add a vertical velocity of 5 to simulate the ember effect
                    Vector3 flameVelocity = rotationalVelocity + new Vector3(0f, 5f, 0f);
                    Debug.Log("Flame Velocity: " + flameVelocity);

                    // Apply the resulting velocity to the flame's Rigidbody
                    rb.linearVelocity = flameVelocity;
                    Debug.Log("Final Velocity: " + rb.linearVelocity);
                    candle.GetComponent<ChandelierController>().lit = false;
                }

                candle = null;
            }
        }
    }

    private void Move()
    {
        if (candle != null) {

            if (candle.tag == "Candle"){
                transform.position = candle.transform.position + new Vector3(0f, 0.33f, 0f);
            }else if (candle.tag == "Chandelier"){
                Transform wickPosition = candle.GetComponent<ChandelierController>().wickPosition;
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

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, shrinkCurve.Evaluate(time / shrinkTime));
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
        yield break;
    }
}
