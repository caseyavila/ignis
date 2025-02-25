using UnityEngine;

public class MovingPlatformAttach : MonoBehaviour
{
    public GameObject candle;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == candle) {
            candle.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == candle) {
            candle.transform.parent = null;
        }
    }
}
