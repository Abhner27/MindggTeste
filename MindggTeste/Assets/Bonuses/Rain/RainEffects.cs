using UnityEngine;

public class RainEffects : RainEventsBase
{
    //Audio
    [SerializeField]
    private ParticleSystem _rainPS;

    protected override void _rain_OnRainStarted()
    {
        _rainPS.Play();
    }

    protected override void _rain_OnRainEnded()
    {
        _rainPS.Stop();
    }
}
