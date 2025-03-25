using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeColorOverTimeEffectUIText : ChangeColorOverTimeEEffectBase
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    protected override IEnumerator SmoothTransition(Color targetColor, float timeToWait)
    {
        Color startColor = _text.color;
        float elapsedTime = 0f;

        while (elapsedTime < timeToWait)
        {
            elapsedTime += CustomTimeManager.GetDeltaTime();
            _text.color = Color.Lerp(startColor, targetColor, elapsedTime / timeToWait);
            yield return null;
        }

        _text.color = targetColor;
    }
}