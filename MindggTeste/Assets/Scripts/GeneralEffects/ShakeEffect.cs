using System.Collections;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public float duration = 0.5f; // Tempo total do tremor
    public float magnitude = 0.1f; // Intensidade do tremor

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-magnitude, magnitude);
            float offsetY = Random.Range(-magnitude, magnitude);
            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsedTime += CustomTimeManager.GetDeltaTime();
            yield return null;
        }

        transform.position = originalPosition; // Retorna à posição original
    }
}
