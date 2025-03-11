using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] candles;
    public Vector3[] targetPositions;
    private Vector3[] defaultPositions;
    public float moveToTargetDuration = 2f;
    public float moveToDefaultDuration = 2f;
    private bool isActivated = false;
    private int objectsOnPlate = 0;
    private Coroutine moveCandlesCoroutine;

    private void Start()
    {
        defaultPositions = new Vector3[candles.Length];
        for (int i = 0; i < candles.Length; i++)
        {
            defaultPositions[i] = candles[i].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsOnPlate++;

        if (!isActivated)
        {
            isActivated = true;
            if (moveCandlesCoroutine != null) StopCoroutine(moveCandlesCoroutine);
            moveCandlesCoroutine = StartCoroutine(MoveCandles(targetPositions, moveToTargetDuration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsOnPlate--;

        if (objectsOnPlate <= 0)
        {
            isActivated = false;
            if (moveCandlesCoroutine != null) StopCoroutine(moveCandlesCoroutine);
            moveCandlesCoroutine = StartCoroutine(MoveCandles(defaultPositions, moveToDefaultDuration));
        }
    }

    private IEnumerator MoveCandles(Vector3[] targetPositions, float duration)
    {
        float elapsedTime = 0f;
        Vector3[] startPositions = new Vector3[candles.Length];

        for (int i = 0; i < candles.Length; i++)
        {
            startPositions[i] = candles[i].transform.position;
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            for (int i = 0; i < candles.Length; i++)
            {
                candles[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            yield return null;
        }

        for (int i = 0; i < candles.Length; i++)
        {
            candles[i].transform.position = targetPositions[i];
        }
    }
}