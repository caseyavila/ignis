using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class WaterController : MonoBehaviour
{
    public float flickerSpeed;
    public float speed;
    private Vector3 currentScale;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flicker();
        Move();
    }
    private void Flicker()
    {
        transform.localScale = currentScale +
            (Vector3.one * 0.25f * (Mathf.PerlinNoise(0, Time.time * flickerSpeed) - 0.5f));
    }

    private void Move()
    {
        
    }
}

