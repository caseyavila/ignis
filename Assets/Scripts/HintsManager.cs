using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HintsManager : MonoBehaviour
{
    void Update() {
        bool showHints = FlameController.Deaths >= 5;

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(showHints);
        }
    }
}
