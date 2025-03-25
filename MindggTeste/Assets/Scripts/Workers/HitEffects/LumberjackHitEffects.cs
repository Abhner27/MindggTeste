using UnityEngine;

public class LumberjackHitEffects : HitEffects
{
    [SerializeField]
    private ShakeEffect _treeShakeEffect;
    [SerializeField]
    private ParticleSystem _leafsPS;

    public override void PlayEffects()
    {
        base.PlayEffects();
        _treeShakeEffect.StartShake();
        _leafsPS.Play();
    }
}