using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInTextTMP : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    public float fadeInDuration = 1.0f;

    void Start()
    {
        if (textElement == null)
            textElement = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Color textColor = textElement.color;
        textColor.a = 0;
        textElement.color = textColor;

        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration)
        {
            textColor.a = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            textElement.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 1;
        textElement.color = textColor;
    }
}
