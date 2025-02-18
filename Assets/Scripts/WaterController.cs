using System.Collections;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public GameObject waterPrefab;
    public float waterSpeed;
    public float spawnTime;
    public float startDelay = 2.0f;

    public Transform spawnPoint;

    private float _timeSinceLastSpawn = 0f;
    private bool _spawningStarted = false;


    void Start()
    {
        StartCoroutine(StartSpawningAfterDelay());
    }

    void Update()
    {
        if (!_spawningStarted) return;

        _timeSinceLastSpawn += Time.deltaTime;
        if (_timeSinceLastSpawn >= spawnTime)
        {
            _timeSinceLastSpawn = 0f;
            SpawnDroplet();
        }
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        _spawningStarted = true;
    }

    private void SpawnDroplet()
    {
        GameObject newDroplet = Instantiate(waterPrefab, spawnPoint.position, Quaternion.identity);
        WaterDroplet dropletScript = newDroplet.GetComponent<WaterDroplet>();

        if (dropletScript != null)
        {
            dropletScript.speed = waterSpeed;
        }
    }
}
