using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangeColorOverTimeEffectUI : ChangeColorOverTimeEEffectBase
{
    private Image _image;
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    protected override IEnumerator SmoothTransition(Color targetColor, float timeToWait)
    {
        Color startColor = _image.color;
        float elapsedTime = 0f;

        while (elapsedTime < timeToWait)
        {
            elapsedTime += CustomTimeManager.GetDeltaTime();
            _image.color = Color.Lerp(startColor, targetColor, elapsedTime / timeToWait);
            yield return null;
        }

        _image.color = targetColor;
    }
}
