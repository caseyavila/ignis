using UnityEngine;
using UnityEngine.InputSystem;

public class CandleController : MonoBehaviour
{
    public float speed = 5;
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
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * speed;

        rb.linearVelocity = velocity;
    }
}

