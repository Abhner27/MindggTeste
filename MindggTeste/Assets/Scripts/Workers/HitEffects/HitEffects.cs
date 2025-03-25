using UnityEngine;

public class HitEffects : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _smokePS;

    public virtual void PlayEffects()
    {
        _smokePS.Play();
    }
}
