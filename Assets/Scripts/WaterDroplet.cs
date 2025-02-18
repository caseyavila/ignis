using UnityEngine;
using System.Collections;

public class WaterDroplet : MonoBehaviour
{
    public float speed;
    public float yBoundary;
    public float flickerSpeed = 2.0f;
    public float triggerDelay = 0.2f;

    private Vector3 originalScale;
    private Collider dropletCollider;

    private void Start()
    {
        originalScale = transform.localScale;
        dropletCollider = GetComponent<Collider>();

        StartCoroutine(EnableTriggerAfterDelay());
    }

    private void Update()
    {
        Move();
        Flicker();
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down);

        if (transform.position.y < yBoundary)
        {
            Destroy(gameObject);
        }
    }

    private void Flicker()
    {
        transform.localScale = originalScale +
            (Vector3.one * 0.25f * (Mathf.PerlinNoise(0, Time.time * flickerSpeed) - 0.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    private IEnumerator EnableTriggerAfterDelay()
    {
        if (dropletCollider != null)
        {
            dropletCollider.isTrigger = false;
            yield return new WaitForSeconds(triggerDelay);
            dropletCollider.isTrigger = true;
        }
    }
}
