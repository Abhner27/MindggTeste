using UnityEngine;

[RequireComponent(typeof(Rain))]
public abstract class RainEventsBase : MonoBehaviour
{
    private Rain _rain;

    private void Start()
    {
        _rain = GetComponent<Rain>();

        _rain.OnRainStarted += _rain_OnRainStarted;
        _rain.OnRainEnded += _rain_OnRainEnded;
    }

    protected abstract void _rain_OnRainStarted();

    protected abstract void _rain_OnRainEnded();

    private void OnDestroy()
    {
        _rain.OnRainStarted -= _rain_OnRainStarted;
        _rain.OnRainEnded -= _rain_OnRainEnded;

    }
}
