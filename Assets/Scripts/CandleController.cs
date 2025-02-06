using UnityEngine;
using UnityEngine.InputSystem;

public class CandleController : MonoBehaviour
{
    public float speed = 5;
    public bool lit;

    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (lit) {
            Move();
            Jump();
        }
    }

    private void Move() {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * speed;

        rb.linearVelocity = velocity;
    }

    private void Jump() {
        float jumpInput = jumpAction.ReadValue<float>();

        if (jumpAction.triggered) {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = 10;
            rb.linearVelocity = velocity;
        }
    }
}

