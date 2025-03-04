using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform flameTransform;

    void Update()
    {
        Vector3 newPos = new Vector3(flameTransform.position.x,
                                     flameTransform.position.y,
                                     transform.position.z);
        transform.position = newPos;
    }
}
