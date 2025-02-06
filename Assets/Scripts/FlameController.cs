using UnityEngine;
using UnityEngine.InputSystem;

public class FlameController : MonoBehaviour
{
    public float flickerSpeed;
    /* If null, flame is an "ember" */
    public GameObject candle;
    public float speed;

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
        if (other.gameObject.tag == "Candle") {
            candle = other.gameObject;
            other.gameObject.GetComponent<CandleController>().lit = true;
            currentScale = defaultScale;
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
                Vector3 velocity = rb.linearVelocity;
                velocity.y = 5;
                rb.linearVelocity = velocity;

                candle.GetComponent<CandleController>().lit = false;

                candle = null;
            }
        }
    }

    private void Move()
    {
        if (candle != null) {
            transform.position = candle.transform.position + new Vector3(0f, 0.33f, 0f);
        } else {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            Vector3 velocity = rb.linearVelocity;
            velocity.x = moveInput.x * speed;

            rb.linearVelocity = velocity;

            Shrink();
        }
    }

    private void Shrink()
    {
        Vector3 newScale = currentScale * 0.997f;

        if (newScale.x < 0) {
            Destroy(gameObject);
        }

        currentScale = newScale;
        transform.localScale = newScale;
    }
}
