using UnityEngine;

public class WaterController : MonoBehaviour
{
    public GameObject waterPrefab;
    public float waterSpeed;
    public float spawnTime;

    public Transform spawnPoint;

    private float _timeSinceLastSpawn = 0f;

    void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;
        if (_timeSinceLastSpawn >= spawnTime)
        {
            _timeSinceLastSpawn = 0f;
            SpawnDroplet();
        }
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
