using System.Collections;
using UnityEngine;

public abstract class ChangeColorOverTimeEEffectBase : MonoBehaviour
{
    protected Coroutine _transitionCoroutine;

    public void UpdateColor(Color targetColor, float timeToWait)
    {
        if (targetColor == null | timeToWait <= 0f)
        {
            Debug.LogError("Color is null or timeToWait is <= 0!");
            return;
        }

        if (_transitionCoroutine != null & this != null)
            StopCoroutine(_transitionCoroutine);

        if (this != null)
            _transitionCoroutine = StartCoroutine(SmoothTransition(targetColor, timeToWait));
    }

    protected abstract IEnumerator SmoothTransition(Color targetColor, float timeToWait);
}
