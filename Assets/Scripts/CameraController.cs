using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform flameTransform;

    void Update()
    {
        Vector3 newPos = new Vector3(flameTransform.position.x,
                                     transform.position.y,
                                     transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 5 * Time.deltaTime);
    }
}
