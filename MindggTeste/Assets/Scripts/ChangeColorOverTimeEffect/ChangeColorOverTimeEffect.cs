using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeColorOverTimeEffect : ChangeColorOverTimeEEffectBase
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override IEnumerator SmoothTransition(Color targetColor, float timeToWait)
    {
        Color startColor = _spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < timeToWait)
        {
            elapsedTime += CustomTimeManager.GetDeltaTime();
            _spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / timeToWait);
            yield return null;
        }

        _spriteRenderer.color = targetColor;
    }
}
