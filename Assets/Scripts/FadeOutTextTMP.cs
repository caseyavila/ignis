using UnityEngine;
using TMPro;
using System.Collections;

public class FadeOutTextTMP : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public float fadeOutDuration = 2.0f;

    void Start()
    {
        if (textElement == null)
            textElement = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color textColor = textElement.color;
        float elapsedTime = 0;

        while (elapsedTime < fadeOutDuration)
        {
            textColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            textElement.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 0;
        textElement.color = textColor;
    }
}
