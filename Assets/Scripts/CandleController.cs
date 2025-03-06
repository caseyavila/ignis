using UnityEngine;
using UnityEngine.InputSystem;

public class CandleController : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public bool lit;

    private InputAction moveAction;
    private Rigidbody rb;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (lit) {
            Move();
        }
    }

    private void Move() {
        //Vector2 moveInput = moveAction.ReadValue<Vector2>();

        //Vector3 velocity = rb.linearVelocity;
        //velocity.x = moveInput.x * speed;

        //rb.linearVelocity = velocity;
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.x == 0) {
            if (rb.linearVelocity.x > 0) {
                rb.AddForce(Vector3.left, ForceMode.Acceleration);
            } else if (rb.linearVelocity.x < 0) {
                rb.AddForce(Vector3.right, ForceMode.Acceleration);
            }
        } else {
            Vector3 moveVector = new Vector3(moveInput.x, 0, 0);
            rb.AddForce(moveVector * acceleration, ForceMode.Acceleration);
        }


        Vector3 velocity = rb.linearVelocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = velocity;
    }
}

