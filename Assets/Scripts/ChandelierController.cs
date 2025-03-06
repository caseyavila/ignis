using UnityEngine;
using UnityEngine.InputSystem;

public class ChandelierController : MonoBehaviour
{
    public float dampingFactor = 0.95f; // Reduce swinging over time
    public float torqueForce = 10f; // Force applied when lit
    public Transform wickPosition; // Wick position inside the chandelier
    
    private InputAction moveAction;
    private Rigidbody rb;
    public bool lit;

    MusicManager audioManager;

    void Awake()
    {
    
       audioManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<MusicManager>();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (lit)
        {
            ApplySwingForce();
        }
        else
        {
            // Apply damping to slow down the swing
            rb.angularVelocity *= dampingFactor;
        }
    }

    void ApplySwingForce()
    {
        
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Apply torque for a back-and-forth swinging motion
        float torque = torqueForce * moveInput.x;
        rb.AddTorque(Vector3.forward * torque);
        
        if (moveInput.x != 0){
            //audioManager.PlayQuietSFX(audioManager.squeak);
        }


    }

  
}
