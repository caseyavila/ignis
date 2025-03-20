using UnityEngine;
using UnityEngine.Splines;

public class TrailController : MonoBehaviour
{
    private float duration;

    void Awake() {
        duration = GetComponent<SplineAnimate>().Duration;
    }

    void Update() {
        bool forward = GetComponent<SplineAnimate>().ElapsedTime % (duration * 2) < duration;
        GetComponent<TrailRenderer>().emitting = forward;
    }
}
