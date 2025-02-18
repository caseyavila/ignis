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
